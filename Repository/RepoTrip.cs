using System;
using System.Collections.Generic;
using Lab3.Domain;
using Lab3.Utility;
using log4net;
using Npgsql;

namespace Lab3.Repository;

public class RepoTrip : IRepositoryTrip
{
    private readonly DbUtils _dbConnection;
    private static readonly ILog Logger = LogManager.GetLogger(typeof(RepoAccount));

    public RepoTrip(DbUtils dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Trip FindOne(long id)
    {
        const string sqlFind = @"
     SELECT * 
     FROM trips 
     WHERE id = @TripId;";
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlFind, conn);
            cmd.Parameters.AddWithValue("@TripId", id);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var trip = new Trip(reader.GetString(reader.GetOrdinal("destination")),
                    reader.GetString(reader.GetOrdinal("transport_company")),
                    reader.GetDouble(reader.GetOrdinal("price")), reader.GetInt32(reader.GetOrdinal("available_seats")),
                    reader.GetDateTime(reader.GetOrdinal("date")), reader.GetDateTime(reader.GetOrdinal("start_hour")),
                    reader.GetDateTime(reader.GetOrdinal("finish_hour")))
                {
                    Id = id
                };
                Logger.Info($"Trip found for TripId: {trip.Id}");
                return trip;
            }

            Logger.Info($"No trip found for TripId: {id}");
            return null!;
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error finding trip for TripId: {id}", ex);
            return null!;
        }
    }

    public Trip Save(Trip entity)
    {
        const string sqlInsert =
            @"INSERT INTO trips (destination, transport_company, price, available_seats, date, start_hour, finish_hour) 
          VALUES (@Destination, @TransportCompany, @Price, @AvailableSeats, @Date, @StartHour, @FinishHour) 
          RETURNING id;";
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlInsert, conn);

            cmd.Parameters.AddWithValue("@Destination", entity.Destination);
            cmd.Parameters.AddWithValue("@TransportCompany", entity.TransportCompany);
            cmd.Parameters.AddWithValue("@Price", entity.Price);
            cmd.Parameters.AddWithValue("@AvailableSeats", entity.AvailableSeats);
            cmd.Parameters.AddWithValue("@Date", entity.Date);
            cmd.Parameters.AddWithValue("@StartHour", entity.StartHour);
            cmd.Parameters.AddWithValue("@FinishHour", entity.FinishHour);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                entity.Id = Convert.ToInt64(result);
                Logger.Info($"Trip added successfully with ID: {entity.Id}");
                return entity;
            }
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error adding trip: {entity}", ex);
        }

        return null!;
    }

    public Trip Delete(Trip entity)
    {
        const string sqlDelete = "DELETE FROM trips WHERE id = @TripId";
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlDelete, conn);
            cmd.Parameters.AddWithValue("@TripId", entity.Id);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            cmd.ExecuteNonQuery();
            Logger.Info($"Trip deleted successfully with ID: {entity.Id}");
            return entity;
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error deleting trip: {entity}", ex);
            return null!;
        }
    }

    public Trip Update(Trip entity)
    {
        const string sqlUpdate =
            @"UPDATE trips 
          SET destination = @Destination, transport_company = @TransportCompany, price = @Price, available_seats = @AvailableSeats, date = @Date, start_hour = @StartHour, finish_hour = @FinishHour
          WHERE id = @TripId;";
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlUpdate, conn);
            cmd.Parameters.AddWithValue("@Destination", entity.Destination);
            cmd.Parameters.AddWithValue("@TransportCompany", entity.TransportCompany);
            cmd.Parameters.AddWithValue("@Price", entity.Price);
            cmd.Parameters.AddWithValue("@AvailableSeats", entity.AvailableSeats);
            cmd.Parameters.AddWithValue("@Date", entity.Date);
            cmd.Parameters.AddWithValue("@StartHour", entity.StartHour);
            cmd.Parameters.AddWithValue("@FinishHour", entity.FinishHour);
            cmd.Parameters.AddWithValue("@TripId", entity.Id);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            cmd.ExecuteNonQuery();
            Logger.Info($"Trip updated successfully with ID: {entity.Id}");
            return entity;
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error updating trip: {entity}", ex);
            return null!;
        }
    }

    public List<Trip> GetAll()
    {
        const string sqlSelect = @"
     SELECT * 
     FROM trips;";
        var trips = new List<Trip>();
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
                var trip = new Trip(reader.GetString(reader.GetOrdinal("destination")),
                    reader.GetString(reader.GetOrdinal("transport_company")),
                    reader.GetDouble(reader.GetOrdinal("price")), reader.GetInt32(reader.GetOrdinal("available_seats")),
                    reader.GetDateTime(reader.GetOrdinal("date")), reader.GetDateTime(reader.GetOrdinal("start_hour")),
                    reader.GetDateTime(reader.GetOrdinal("finish_hour")))
                {
                    Id = reader.GetInt64(reader.GetOrdinal("id"))
                };
                trips.Add(trip);
            }

            Logger.Info("All trips retrieved successfully.");
        }
        catch (NpgsqlException ex)
        {
            Logger.Error("Error retrieving all trips.", ex);
        }

        return trips;
    }

    public Trip findByDestination(string destination)
    {
        
const string sqlFind = @"
     SELECT * 
     FROM trips 
     WHERE destination = @Destination;";
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlFind, conn);
            cmd.Parameters.AddWithValue("@Destination", destination);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var trip = new Trip(reader.GetString(reader.GetOrdinal("destination")),
                    reader.GetString(reader.GetOrdinal("transport_company")),
                    reader.GetDouble(reader.GetOrdinal("price")), reader.GetInt32(reader.GetOrdinal("available_seats")),
                    reader.GetDateTime(reader.GetOrdinal("date")), reader.GetDateTime(reader.GetOrdinal("start_hour")),
                    reader.GetDateTime(reader.GetOrdinal("finish_hour")))
                {
                    Id = reader.GetInt64(reader.GetOrdinal("id"))
                };
                Logger.Info($"Trip found for destination: {trip.Destination}");
                return trip;
            }

            Logger.Info($"No trip found for destination: {destination}");
            return null!;
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error finding trip for destination: {destination}", ex);
            return null!;
        }
        
    }

    public List<Trip> filterTrips(string destination, int startHour, int finishHour)
    {

        const string sqlFilter = @"
    SELECT * FROM trips WHERE destination =@Destination AND EXTRACT(HOUR FROM start_hour) >= @StartHour AND EXTRACT(HOUR FROM finish_hour) <= @FinishHour;";
        var trips = new List<Trip>();
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlFilter, conn);
            cmd.Parameters.AddWithValue("@Destination", destination);
            cmd.Parameters.AddWithValue("@StartHour", startHour);
            cmd.Parameters.AddWithValue("@FinishHour", finishHour);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var trip = new Trip(reader.GetString(reader.GetOrdinal("destination")),
                    reader.GetString(reader.GetOrdinal("transport_company")),
                    reader.GetDouble(reader.GetOrdinal("price")), reader.GetInt32(reader.GetOrdinal("available_seats")),
                    reader.GetDateTime(reader.GetOrdinal("date")), reader.GetDateTime(reader.GetOrdinal("start_hour")),
                    reader.GetDateTime(reader.GetOrdinal("finish_hour")))
                {
                    Id = reader.GetInt64(reader.GetOrdinal("id"))
                };
                trips.Add(trip);
            }

            Logger.Info("Trips filtered successfully.");
        }
        catch (NpgsqlException ex)
        {
            Logger.Error("Error filtering trips.", ex);
        }

        return trips;
    }
}