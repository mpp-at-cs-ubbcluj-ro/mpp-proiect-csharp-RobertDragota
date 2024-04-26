using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using AppDomain.Domain;
using AppServices.Service;

namespace AppNetworking.RcpProtocol
{
    public class AppServicesRpcProxy : ServiceAppInterface
    {
        private readonly string _host;
        private readonly int _port;

        private AppObserverInterface _client;


        private NetworkStream _stream;
        private readonly BinaryFormatter _formatter = new BinaryFormatter();
        private Socket _connection;

        private readonly BlockingCollection<Response> _responses = new BlockingCollection<Response>();
        private volatile bool _finished;

        public AppServicesRpcProxy(string host, int port)
        {
            this._host = host;
            this._port = port;
        }

        private void CloseConnection()
        {
            _finished = true;
            try
            {
                _stream?.Close();
                _connection?.Close();
                _client = null;
            }
            catch (IOException e)
            {
                Console.WriteLine("Error closing connection: " + e.Message);
            }
        }

        private void SendRequest(Request request)
        {
            try
            {
                _formatter.Serialize(_stream, request);
                _stream.Flush();
            }
            catch (IOException e)
            {
                throw new AppException("Error sending object: " + e.Message);
            }
        }

        private Response ReadResponse()
        {
            try
            {
                return _responses.Take();
            }
            catch (InvalidOperationException e)
            {
                throw new AppException("Error reading response: " + e.Message);
            }
        }

        private void InitializeConnection()
        {
            try
            {
                _connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _connection.Connect(_host, _port);
                _stream = new NetworkStream(_connection);
                _finished = false;
                StartReader();
            }
            catch (IOException e)
            {
                throw new AppException("Error establishing connection: " + e.Message);
            }
        }

        private void StartReader()
        {
            Thread readerThread = new Thread(() =>
            {
                while (!_finished)
                {
                    try
                    {
                        object data = _formatter.Deserialize(_stream);
                        Console.WriteLine("flag " + data);
                        if (data is Response response)
                        {
                            Console.WriteLine("!!!!!!!!Response received: " + response);
                            if (IsUpdate(response))
                            {
                                HandleUpdate(response);
                            }
                            else
                            {
                                _responses.Add(response);
                            }
                        }
                        else
                            Console.WriteLine("!!!!!!!!Received object: " + data);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("!!!!!!!!Reading error: " + e.StackTrace + e.Message);
                        if (e is IOException || e is SocketException)
                        {
                           
                            CloseConnection();
                            break;
                        }
                    }
                }
            });
            readerThread.Start();
        }

        private void HandleUpdate(Response response)
        {
            if (response.Type == ResponseType.UPDATE)
            {
                var list = (IEnumerable<Trip>)response.Data;
                Console.WriteLine("Trips list: " + list.ToList());
                try
                {
                    _client.UpdateTrips(list);
                }
                catch (AppException e)
                {
                    Console.WriteLine("Error updating trips: " + e.Message);
                }
            }
        }

        private bool IsUpdate(Response response)
        {
            return response.Type == ResponseType.UPDATE;
        }

        public virtual bool Login(Account account, AppObserverInterface observer)
        {
            if (_connection == null)
                InitializeConnection();
            Request request = new Request.Builder().Type(RequestType.LOGIN).Data(account).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == ResponseType.OK)
            {
                this._client = observer;
                return true;
            }

            if (response.Type == ResponseType.ERROR)
            {
                CloseConnection();
                throw new AppException("Login error");
            }

            return false;
        }

        public void Register(string username, string password)
        {
            Request request = new Request.Builder().Type(RequestType.REGISTER).Data(new Account(username, password))
                .Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == ResponseType.OK)
            {
                return;
            }

            if (response.Type == ResponseType.ERROR)
            {
                CloseConnection();
                throw new AppException("Register error");
            }
        }

        public void Logout(Account account, AppObserverInterface observer)
        {
            Request request = new Request.Builder().Type(RequestType.LOGOUT).Data(account).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == ResponseType.OK)
            {
                CloseConnection();
                return;
            }

            if (response.Type == ResponseType.ERROR)
            {
                throw new AppException("Logout error");
            }
        }

        public virtual void MakeReservation(Account account, string name, string phone, long tickets, Trip trip)
        {
            Request request = new Request.Builder().Type(RequestType.MAKE_RESERVATION)
                .Data(new Reservation(account, phone, tickets, trip, name)).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == ResponseType.OK)
            {
                return;
            }

            if (response.Type == ResponseType.ERROR)
            {
                throw new AppException("Make reservation error");
            }
        }

        public IEnumerable<Trip> Get_All_Trips()
        {
            Request request = new Request.Builder().Type(RequestType.GET_ALL_TRIP).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == ResponseType.GET_ALL_TRIP)
            {
                Console.WriteLine("Trips list: " + response.Data);
                return (IEnumerable<Trip>)response.Data;
            }

            return null;
        }

        public IEnumerable<Trip> Get_All_Trip_By_Destination_From_To(string destination, int startDate, int finishDate)
        {
            DateTime start = new DateTime(2024, 1, 1, startDate, 1, 0);
            DateTime finish = new DateTime(2024, 1, 1, finishDate, 1, 0);
            DateTime now = DateTime.Now;
            Trip trip = new Trip(destination, ".", 1, 1, now, start, finish);
            Request request = new Request.Builder().Type(RequestType.GET_ALL_TRIP_BY_DESTINATION_FROM_TO).Data(trip)
                .Build();
            try
            {
                SendRequest(request);
                Response response = ReadResponse();
                if (response.Type == ResponseType.GET_ALL_TRIP_BY_DESTINATION_FROM_TO)
                {
                    Console.WriteLine("Trips list " + response.Data);
                    return (IEnumerable<Trip>)response.Data;
                }
            }
            catch (AppException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            return null;
        }

        public Account MakeAccount(string username, string password)
        {
            Request request = new Request.Builder().Type(RequestType.MAKE_ACCOUNT).Data(new Account(username, password))
                .Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == ResponseType.OK)
            {
                return (Account)response.Data;
            }

            if (response.Type == ResponseType.ERROR)
            {
                throw new AppException("Make account error");
            }

            return null;
        }

        public Account FindAccount(string username, string password)
        {
            if (_connection == null)
                InitializeConnection();
            Request request = new Request.Builder().Type(RequestType.FIND_ACCOUNT)
                .Data(new Account(username, password)).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == ResponseType.FIND_ACCOUNT)
            {
                return (Account)response.Data;
            }

            if (response.Type == ResponseType.ERROR)
            {
                throw new AppException("Find account error");
            }

            return null;
        }
    }
}