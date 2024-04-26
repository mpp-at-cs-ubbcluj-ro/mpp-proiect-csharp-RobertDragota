#nullable enable
using System;
using System.Configuration;
using System.IO;
using log4net;
using log4net.Config;
using Npgsql;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace AppPersistence.Utility;

public class DbUtils
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(DbUtils));
    private NpgsqlConnection? _instance = null;
    private readonly string _connectionString;

    static string GetConnectionString(string name)
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
        if (settings == null)
            throw new Exception($"Connection string {name} not found");
        return settings.ConnectionString;
    }

    public DbUtils()
    {
        try
        {
            _connectionString = GetConnectionString("travel_agency");
            XmlConfigurator.Configure(new FileInfo("log4net.xml"));
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error getting connection string {e}");
            _connectionString= "";
        }
    }

    private NpgsqlConnection? GetNewConnection()
    {
        Logger.Info($"Trying to connect to Postgres database ... {_connectionString}");

        NpgsqlConnection? con = null;
        try
        {
            con = new NpgsqlConnection(_connectionString);
            con.Open();
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine($"Error getting connection {e}");
        }

        return con;
    }

    public NpgsqlConnection? GetConnection()
    {
        try
        {
            if (_instance == null || _instance.State == System.Data.ConnectionState.Closed)
                _instance = GetNewConnection();
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine($"Error DB {e}");
        }

        return _instance;
    }
}