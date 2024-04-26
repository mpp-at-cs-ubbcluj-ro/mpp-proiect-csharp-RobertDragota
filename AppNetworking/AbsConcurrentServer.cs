using System;
using System.Net.Sockets;
using System.Threading;

namespace AppNetworking
{
    public abstract class AbsConcurrentServer : AbstractServer
    {
        public AbsConcurrentServer(int port) : base(port)
        {
            Console.WriteLine("Concurrent AbstractServer");
        }

        protected override void ProcessRequest(Socket client)
        {
            Thread worker = CreateWorker(client);
            worker.Start();
        }

        protected abstract Thread CreateWorker(Socket client);
    }
}