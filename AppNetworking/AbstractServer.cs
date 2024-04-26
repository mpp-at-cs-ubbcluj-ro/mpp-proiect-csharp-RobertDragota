using System;
using System.Net;
using System.Net.Sockets;

namespace AppNetworking
{
    public abstract class AbstractServer
    {
        private readonly int _port;
        private TcpListener _server = null;

        public AbstractServer(int port)
        {
            this._port = port;
        }

        public void Start()
        {
            try
            {
                _server = new TcpListener(IPAddress.Any, _port);
                _server.Start();
                Console.WriteLine("Server started. Waiting for users...");

                while (true)
                {
                    Console.WriteLine("Waiting for users...");
                    Socket client = _server.AcceptSocket();
                    Console.WriteLine("User connected...");
                    ProcessRequest(client);
                }
            }
            catch (SocketException e)
            {
                throw new ServerException("Starting server error", e);
            }
            finally
            {
                Stop();
            }
        }

        protected abstract void ProcessRequest(Socket client);

        public virtual void Stop()
        {
            try
            {
                _server?.Stop();
            }
            catch (SocketException e)
            {
                throw new ServerException("Closing server error", e);
            }
        }
    }

    public class ServerException : Exception
    {
        public ServerException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}