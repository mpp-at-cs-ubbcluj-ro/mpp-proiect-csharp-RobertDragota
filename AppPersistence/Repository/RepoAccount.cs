#nullable enable

using System;
using System.Collections.Generic;
using AppDomain.Domain;
using AppPersistence.Utility;
using Lab3.Utility;
using log4net;
using Npgsql;

namespace AppPersistence.Repository;

public class RepoAccount : IRepositoryAccount
{
    private readonly DbUtils _dbConnection;
    private static readonly ILog Logger = LogManager.GetLogger(typeof(RepoAccount));

    public RepoAccount(DbUtils dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Account? FindOne(long id)
    {
        // Query adjusted to remove unused parameter
        const string sqlFind = @"
    SELECT account.* 
    FROM account 
    WHERE account.id = @ClientId;";

        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlFind, conn);
            // Only one parameter is used here, corresponding to the WHERE clause
            cmd.Parameters.AddWithValue("@ClientId", id);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var account = new Account(reader.GetString(reader.GetOrdinal("name")),
                    reader.GetString(reader.GetOrdinal("password")))
                {
                    Id = id
                };

                Logger.Info($"Account found for ClientId: {account.Id}");
                return account;
            }

            Logger.Info($"No account found for ClientId: {id}");
            return null;
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error finding account for ClientId: {id}", ex);
            return null;
        }
    }

    public Account? Save(Account account)
    {
        const string sqlInsert =
            "INSERT INTO account (name, password) VALUES (@Username, @Password) RETURNING id;";
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlInsert, conn); // Changed to SQLiteCommand
            cmd.Parameters.AddWithValue("@Username", account.Username);
            cmd.Parameters.AddWithValue("@Password", account.Password);


            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                // Assuming id is of type long
                account.Id = Int64.Parse(result.ToString());
                Logger.Info($"Account added successfully for Username: {account.Username}, Id: {account.Id}");
                return account;
            }

           throw   new NpgsqlException("Error adding account");
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error adding account for Username: {account.Username}", ex);
            return null!;
        }
    }

    public Account? Delete(Account account)
    {
        const string sqlDelete = "DELETE FROM account WHERE id = @ClientId";
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlDelete, conn); // Changed to SQLiteCommand
            cmd.Parameters.AddWithValue("@ClientId", account.Id);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            int affectedRows = cmd.ExecuteNonQuery();
            if (affectedRows > 0)
            {
                Logger.Info($"Account deleted successfully for AccountId: {account.Id}");
                return account;
            }

            Logger.Warn($"No account found to delete for AccountId: {account.Id}");
            return null!;
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error deleting account for AccountId: {account.Id}", ex);
            return null!;
        }
    }

    public Account? Update(Account entity)
    {
        const string sqlUpdate =
            "UPDATE account SET name = @Username, password = @Password WHERE id = @ClientId;";
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlUpdate, conn); // Changed to SQLiteCommand
            cmd.Parameters.AddWithValue("@Username", entity.Username);
            cmd.Parameters.AddWithValue("@Password", entity.Password);
            cmd.Parameters.AddWithValue("@ClientId", entity.Id);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            int affectedRows = cmd.ExecuteNonQuery();
            if (affectedRows > 0)
            {
                Logger.Info($"Account updated successfully for AccountId: {entity.Id}");
                return entity;
            }

            Logger.Warn($"No account found to update for AccountId: {entity.Id}");
            return null!;
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error updating account for AccountId: {entity.Id}", ex);
            return null!;
        }
    }

    public List<Account> GetAll()
    {
        const string sqlGetAll = @"
    SELECT account.*
    FROM account";

        var accounts = new List<Account>();

        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlGetAll, conn);

            if (conn != null) conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var account = new Account(reader.GetString(reader.GetOrdinal("name")),
                    reader.GetString(reader.GetOrdinal("password")))
                {
                    Id = reader.GetInt64(reader.GetOrdinal("id"))
                };
                accounts.Add(account);
            }

            Logger.Info($"Retrieved {accounts.Count} accounts");
        }
        catch (Exception ex)
        {
            Logger.Error("Error retrieving all accounts", ex);
        }

        return accounts;
    }

    public Account? findByUsername(string username)
    {
        const string sqlFind = @"
    SELECT account.* 
    FROM account 
    WHERE account.name = @AccountName;";
        
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlFind, conn);
            cmd.Parameters.AddWithValue("@AccountName", username);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var account = new Account(reader.GetString(reader.GetOrdinal("name")),
                    reader.GetString(reader.GetOrdinal("password")))
                {
                    Id = reader.GetInt64(reader.GetOrdinal("id"))
                };

                Logger.Info($"Account found for Username: {account.Username}");
                return account;
            }

            Logger.Info($"No account found for Username: {username}");
            return null;
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error finding account for Username: {username}", ex);
            return null;
        }
    }

    public Account findByUsernameAndPassword(string username, string password)
    {
        const string sqlFind = @"
    SELECT account.* 
    FROM account 
    WHERE account.name = @AccountName And account.password=@Passwd;";
        
        try
        {
            using var conn = _dbConnection.GetConnection();
            using var cmd = new NpgsqlCommand(sqlFind, conn);
            cmd.Parameters.AddWithValue("@AccountName", username);
            cmd.Parameters.AddWithValue("@Passwd", password);

            if (conn == null)
                throw new NpgsqlException("Connection object is null.");
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var account = new Account(reader.GetString(reader.GetOrdinal("name")),
                    reader.GetString(reader.GetOrdinal("password")))
                {
                    Id = reader.GetInt64(reader.GetOrdinal("id"))
                };

                Logger.Info($"Account found for Username: {account.Username}");
                return account;
            }

            Logger.Info($"No account found for Username: {username}");
            return null;
        }
        catch (NpgsqlException ex)
        {
            Logger.Error($"Error finding account for Username: {username}", ex);
            return null;
        }
    }
}