using System;
using System.Net.Sockets;
using System.Threading;
using AppNetworking.RcpProtocol;
using AppServices.Service;

namespace AppNetworking
{
    public class AppRpcConcurrentServer : AbsConcurrentServer
    {
        private readonly ServiceAppInterface _appServer;

        public AppRpcConcurrentServer(int port, ServiceAppInterface chatServer) : base(port)
        {
            this._appServer = chatServer;
            Console.WriteLine("Chat- ChatRpcConcurrentServer");
        }

        protected override Thread CreateWorker(Socket client)
        {
            // Assume AppUserRpcReflectionWorker is a class that implements the Runnable interface in Java
            // and is adapted here as a class implementing the `IRunnable` interface for worker tasks.
            AppUserRpcReflectionWorker worker = new AppUserRpcReflectionWorker(_appServer, client);
            return new Thread(new ThreadStart(worker.Run));
        }

        public override void Stop()
        {
            Console.WriteLine("Stopping services...");
            base.Stop(); // Call base stop method to handle additional stopping mechanisms, if any defined.
        }
    }
}