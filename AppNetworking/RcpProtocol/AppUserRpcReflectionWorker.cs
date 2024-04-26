using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using AppDomain.Domain;
using AppServices.Service;

namespace AppNetworking.RcpProtocol
{
    public class AppUserRpcReflectionWorker: AppObserverInterface{
        
        private readonly ServiceAppInterface _server;
        private readonly Socket _connection;
        private readonly NetworkStream _stream;
        private readonly BinaryFormatter _formatter = new BinaryFormatter();
        private volatile bool _connected;

        public AppUserRpcReflectionWorker(ServiceAppInterface server, Socket connection)
        {
            this._server = server;
            this._connection = connection;
            try
            {
                _stream = new NetworkStream(connection);
                _formatter.Serialize(_stream, new object()); // Sending a dummy object as a handshake or initial flush.
                _connected = true;
            }
            catch (IOException e)
            {
                Console.WriteLine("Error initializing network stream: " + e.Message);
            }
        }
       
        public void Run()
        {
            while (_connected)
            {
                try
                {
                    Console.WriteLine("Waiting for request...");
                    Request request = (Request)_formatter.Deserialize(_stream);
                    Response response = HandleRequest(request);
                    if (response != null)
                    {
                        SendResponse(response);
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Communication error: " + e.Message);
                    _connected = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error processing request: " + e.Message);
                }

                Thread.Sleep(1000); // Sleep to prevent a tight loop
            }

            CloseConnection();
        }

        private void CloseConnection()
        {
            try
            {
                _stream.Close();
                _connection.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine("Error closing connection: " + e.Message);
            }
        }

        private Response HandleRequest(Request request)
        {
            string handlerName = "Handle" + request.Type.ToString();
            Console.WriteLine("Handler Name: " + handlerName);

            try
            {
                MethodInfo method = GetType().GetMethod(handlerName, BindingFlags.NonPublic | BindingFlags.Instance);
                if (method != null)
                {
                    return (Response)method.Invoke(this, new object[] { request });
                }
                else
                {
                    return new Response.Builder().Type(ResponseType.ERROR).Data("No handler method found").Build();
                }
            }
            catch (TargetInvocationException e)
            {
               
                    Console.WriteLine("Invocation error: " + e.InnerException.Message);
                    return new Response.Builder().Type(ResponseType.ERROR).Data(e.InnerException.Message).Build();
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Reflection error: " + e.Message);
                return new Response.Builder().Type(ResponseType.ERROR).Data("Method invocation failed").Build();
            }
        }

        private Response OkResponse => new Response.Builder().Type(ResponseType.OK).Build();

        private Response HandleLOGIN(Request request)
        {
            Console.WriteLine("Login request: " + request.Type);
            var account = (Account)request.Data;
            try
            {
                _server.Login(account, this);
                return OkResponse;
            }
            catch (AppException e)
            {
                _connected = false;
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }

        private Response HandleLOGOUT(Request request)
        {
            Console.WriteLine("Logout request...");
            var account = (Account)request.Data;
            try
            {
                _server.Logout(account, this);
                _connected = false;
                return OkResponse;
            }
            catch (AppException e)
            {
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }

        private Response HandleREGISTER(Request request)
        {
            Console.WriteLine("Register request...");
            var account = (Account)request.Data;
            try
            {
                _server.Register(account.Username, account.Password);
                return OkResponse;
            }
            catch (AppException e)
            {
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }

        private Response HandleMAKE_RESERVATION(Request request)
        {
            Console.WriteLine("Make Reservation request...");
            var reservation = (Reservation)request.Data;
            var account = reservation.Account;
            try
            {
                _server.MakeReservation(account, account.Username, reservation.PhoneNumber, reservation.Tickets,
                    reservation.Trip);
                return OkResponse;
            }
            catch (AppException e)
            {
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }

        private Response HandleGET_ALL_TRIP_BY_DESTINATION_FROM_TO(Request request)
        {
            Console.WriteLine("Get All request...");
            var destination = (Trip)request.Data;
            try
            {
                var list = _server.Get_All_Trip_By_Destination_From_To(destination.Destination,
                    destination.StartHour.Hour, destination.FinishHour.Hour);
                return new Response.Builder().Type(ResponseType.GET_ALL_TRIP_BY_DESTINATION_FROM_TO).Data(list).Build();
            }
            catch (AppException e)
            {
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }

        private Response HandleGET_ALL_TRIP(Request request)
        {
            Console.WriteLine("Get All request...");
            try
            {
                var list = _server.Get_All_Trips();
                return new Response.Builder().Type(ResponseType.GET_ALL_TRIP).Data(list).Build();
            }
            catch (AppException e)
            {
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }
        
        private Response HandleFIND_ACCOUNT(Request request)
        {
            Console.WriteLine("Find Account request...");
            var account = (Account)request.Data;
            try
            {
                var foundAccount = _server.FindAccount(account.Username, account.Password);
                return new Response.Builder().Type(ResponseType.FIND_ACCOUNT).Data(foundAccount).Build();
            }
            catch (AppException e)
            {
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }

        private void SendResponse(Response response)
        {
            Console.WriteLine("Sending response: " + response);
            lock (_stream)
            {
                _formatter.Serialize(_stream, response);
                _stream.Flush();
            }
        }

        public virtual void UpdateTrips(IEnumerable<Trip> list)
        {
            Console.WriteLine("AICI TRIMIT LISTA CA RASPUS LA UPDATE");

            if (list == null)
            {
                throw new Exception("List is null.");
            }

            var response = new Response.Builder().Type(ResponseType.UPDATE).Data(list).Build();

            if (response == null)
            {
                throw new Exception("Response is null.");
            }

            try
            {
                SendResponse(response);
            }
            catch (IOException e)
            {
                Console.WriteLine("Error sending update: " + e.Message);
            }
        }
    }
}