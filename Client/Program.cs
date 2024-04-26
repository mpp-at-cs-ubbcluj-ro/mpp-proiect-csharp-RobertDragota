using System;
using System.Configuration;
using System.Globalization;
using System.Windows.Forms;
using AppNetworking.RcpProtocol;
using AppServices.Service;

namespace Client
{
    static class Program
    {
        private static readonly int DefaultChatPort = 55555;
        private static readonly string DefaultServer = "localhost";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///
        
        /*
         * app.server.host=localhost
app.server.port=55556
         */
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Load client properties from App.config or a similar configuration file
            var serverIp = ConfigurationManager.AppSettings["app.server.host"] ?? DefaultServer;
            int serverPort = DefaultChatPort;
            try
            { 
                serverPort =ConfigurationManager.AppSettings["app.server.port"] != null ? int.Parse(ConfigurationManager.AppSettings["app.server.port"], CultureInfo.InvariantCulture) : DefaultChatPort;
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Wrong port number " + ex.Message);
                Console.WriteLine("Using default port: " + DefaultChatPort);
            }

            Console.WriteLine("Using server IP " + serverIp);
            Console.WriteLine("Using server port " + serverPort);

            // Initialize the service proxy with the server IP and port
            ServiceAppInterface appServicesRpcProxy = new AppServicesRpcProxy(serverIp, serverPort);

            // Assuming LoginForm is your login form, and it accepts a ServiceAppInterface object
            var loginForm = new Form1(appServicesRpcProxy);
            Application.Run(loginForm);
        }
    }
}