using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using AppNetworking;
using AppPersistence.Repository;
using AppPersistence.Utility;
using AppPersistence.Utility.Validation;
using AppServer.app_server;
using AppServices.Service;

namespace AppServer
{
    public static class StartRpcServer
    {
        private const int DefaultPort = 55555;

        public static void Main(string[] args)
        {
            string serverPort = null;
            try
            {
                serverPort = ConfigurationManager.AppSettings["app.server.port"];

                Console.WriteLine("Server properties set.");
                Console.WriteLine("serverPort: " + serverPort);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Cannot find appserver.properties " + e.Message);
            }

            DbUtils dbUtils = new DbUtils();
            RepoAccount repoAccount = new RepoAccount(dbUtils);
            ValidatorAccount validatorAccount = new ValidatorAccount();
            RepoTrip repoTrip = new RepoTrip(dbUtils);
            RepoReservation repoReservation = new RepoReservation(dbUtils);
            IServiceAccount serviceAccount = new ServiceAccount(repoAccount, validatorAccount);
            IServiceTrip serviceTrip = new ServiceTrip(repoTrip, new ValidatorTrip());
            IServiceReservation serviceReservation =
                new ServiceReservation(repoReservation, new ValidatorReservation());
            ServiceAppInterface serviceController =
                new ServiceDispatcher(serviceAccount, serviceReservation, serviceTrip);
            int appServer = DefaultPort;
            try
            {
                if (serverPort != null)
                    appServer = int.Parse(serverPort);
                else
                    throw new FormatException("No port found in appserver.properties");
            }
            catch (FormatException fe)
            {
                Console.Error.WriteLine("Wrong Port Number" + fe.Message);
                Console.Error.WriteLine("Using default port " + DefaultPort);
            }

            Console.WriteLine("Starting server on port: " + appServer);
            AbstractServer server = new AppRpcConcurrentServer(appServer, serviceController);
            try
            {
                server.Start();
            }
            catch (ServerException e)
            {
                Console.Error.WriteLine("Error starting the server" + e.Message);
            }
            finally
            {
                try
                {
                    server.Stop();
                }
                catch (ServerException e)
                {
                    Console.Error.WriteLine("Error stopping server " + e.Message);
                }
            }
        }
    }
}