#nullable enable
using System;
using System.Collections.Generic;
using Lab3.Domain;
using Lab3.Utility;
using log4net;
using Npgsql;

namespace Lab3.Repository;

public class RepoReservation : IRepositoryReservation
{
    private readonly DbUtils _dbConnection;
    private static readonly ILog Logger = LogManager.GetLogger(typeof(RepoAccount));

    public RepoReservation(DbUtils dbConnection)
    {
        _dbConnection = dbConnection;
    }


    public Reservation? FindOne(long id)
    {
        const string sqlFind = @"
     SELECT r.*, a.id AS account_id, a.name AS account_name,a.password,
                           t.id AS trip_id, t.destination, t.transport_company, t.price, t.available_seats, t.date, t.start_hour, t.finish_hour
                    FROM reservations r
                    INNER JOIN account a ON r.account_id = a.id
                    INNER JOIN trips t ON r.trip_id = t.id
                    WHERE r.id = ?;";

        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlFind, conn);
            cmd.Parameters.AddWithValue("@ReservationId", id);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");

            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var account = new Account(reader.GetString(reader.GetOrdinal("client_name")),
                    reader.GetString(reader.GetOrdinal("phone_number")))
                {
                    Id = reader.GetInt64(reader.GetOrdinal("account_id"))
                };

                var trip = new Trip(reader.GetString(reader.GetOrdinal("destination")),
                    reader.GetString(reader.GetOrdinal("transport_company")),
                    reader.GetDouble(reader.GetOrdinal("price")), reader.GetInt32(reader.GetOrdinal("available_seats")),
                    reader.GetDateTime(reader.GetOrdinal("date")), reader.GetDateTime(reader.GetOrdinal("start_hour")),
                    reader.GetDateTime(reader.GetOrdinal("finish_hour")))
                {
                    Id = reader.GetInt64(reader.GetOrdinal("trip_id"))
                };

                var reservation = new Reservation(account, reader.GetString(reader.GetOrdinal("phone_number")),
                    reader.GetInt32(reader.GetOrdinal("tickets")), trip,
                    reader.GetString(reader.GetOrdinal("client_name")))
                    {
                        Id = id
                    };


                Logger.Info($"Reservation found for Id: {reservation.Id}");
                return reservation;
            }

            Logger.Info($"No reservation found for Id: {id}");
            return null;
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error finding reservation for Id: {id}", ex);
            return null;
        }
    }


    public Reservation? Save(Reservation entity)
    {
        const string sqlInsert =
            "INSERT INTO reservations (client_name,phone_number, tickets, trip_id, account_id) VALUES (@ClientName,@PhoneNumber, @Tickets, @TripId, @AccountId) RETURNING id;";
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlInsert, conn);
            cmd.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
            cmd.Parameters.AddWithValue("@ClientName", entity.ClientName);
            cmd.Parameters.AddWithValue("@Tickets", entity.Tickets);
            cmd.Parameters.AddWithValue("@TripId", entity.Trip.Id);
            cmd.Parameters.AddWithValue("@AccountId", entity.Account.Id);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            var id = cmd.ExecuteScalar();
            if (id != null)
            {
                entity.Id = Int64.Parse(id.ToString());
                Logger.Info($"Reservation added successfully for Id: {entity.Id}");
                return entity;
            }

            throw new NpgsqlException("Error adding reservation");
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error adding reservation for Id: {entity.Id}", ex);
            return null!;
        }
    }

    public Reservation? Delete(Reservation entity)
    {
        const string sqlDelete = "DELETE FROM reservations WHERE id = @Id;";
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlDelete, conn);
            cmd.Parameters.AddWithValue("@Id", entity.Id);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            var affectedRows = cmd.ExecuteNonQuery();
            if (affectedRows > 0)
            {
                Logger.Info($"Reservation with ID {entity.Id} deleted successfully.");
                return entity;
            }
            else
            {
                Logger.Info($"No reservation was deleted with ID: {entity.Id}");
            }
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error deleting reservation with ID: {entity.Id}", ex);
        }

        return null;
    }

    public Reservation? Update(Reservation entity)
    {
        const string sqlUpdate =
            "UPDATE reservations SET phone_number = @PhoneNumber, tickets = @Tickets, trip_id = @TripId ,client_name=@ClientName WHERE id = @Id;";
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlUpdate, conn);
            cmd.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
            cmd.Parameters.AddWithValue("@ClientName", entity.ClientName);
            cmd.Parameters.AddWithValue("@Tickets", entity.Tickets);
            cmd.Parameters.AddWithValue("@TripId", entity.Trip.Id);
            cmd.Parameters.AddWithValue("@Id", entity.Id);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            var affectedRows = cmd.ExecuteNonQuery();
            if (affectedRows > 0)
            {
                Logger.Info($"Reservation with ID {entity.Id} updated successfully.");
                return entity;
            }
            else
            {
                Logger.Info($"No reservation was updated with ID: {entity.Id}");
            }
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error updating reservation with ID: {entity.Id}", ex);
        }

        return null;
    }

    public List<Reservation> GetAll()
    {
        const string sqlSelect = @"
     SELECT r.*, a.id AS account_id, a.name AS account_name,a.password,
                           t.id AS trip_id, t.destination, t.transport_company, t.price, t.available_seats, t.date, t.start_hour, t.finish_hour
                    FROM reservations r
                    INNER JOIN account a ON r.account_id = a.id
                    INNER JOIN trips t ON r.trip_id = t.id;";

        var reservations = new List<Reservation>();
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlSelect, conn);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var account = new Account(reader.GetString(reader.GetOrdinal("account_name")),
                    reader.GetString(reader.GetOrdinal("phone_number")))
                {
                    Id = reader.GetInt64(reader.GetOrdinal("account_id"))
                };

                var trip = new Trip(reader.GetString(reader.GetOrdinal("destination")),
                    reader.GetString(reader.GetOrdinal("transport_company")),
                    reader.GetDouble(reader.GetOrdinal("price")), reader.GetInt32(reader.GetOrdinal("available_seats")),
                    reader.GetDateTime(reader.GetOrdinal("date")), reader.GetDateTime(reader.GetOrdinal("start_hour")),
                    reader.GetDateTime(reader.GetOrdinal("finish_hour")))
                {
                    Id = reader.GetInt64(reader.GetOrdinal("trip_id"))
                };

                var reservation = new Reservation(account, reader.GetString(reader.GetOrdinal("phone_number")),
                    reader.GetInt32(reader.GetOrdinal("tickets")), trip,
                    reader.GetString(reader.GetOrdinal("client_name")));
                
                reservation.Id = reader.GetInt64(reader.GetOrdinal("id"));
                    
                
                reservations.Add(reservation);
            }

            Logger.Info("Reservations retrieved successfully.");
            return reservations;
        }
        catch (NpgsqlException ex)
        {
            Logger.Error("Error retrieving reservations.", ex);
            return new List<Reservation>();
        }
    }
}