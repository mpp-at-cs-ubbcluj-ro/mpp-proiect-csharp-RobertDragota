using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AppDomain.Domain;
using AppServices.Service;

public class ServiceDispatcher : ServiceAppInterface
{
    private readonly IServiceAccount _accountService;
    private readonly IServiceReservation _reservationService;
    private readonly IServiceTrip _tripService;
    private readonly ConcurrentDictionary<long, AppObserverInterface> _loggedUsers;
    private const int DefaultThreadsNo = 5;

    public ServiceDispatcher(IServiceAccount accountService, IServiceReservation reservationService,
        IServiceTrip tripService)
    {
        this._accountService = accountService;
        this._reservationService = reservationService;
        this._tripService = tripService;
        _loggedUsers = new ConcurrentDictionary<long, AppObserverInterface>();
    }

    private void NotifyClients(IEnumerable<Trip> trips)
    {
        if (_loggedUsers == null || trips == null)
        {
            throw new Exception("_loggedUsers or trips is null.");
        }

        Console.WriteLine("Notify");

        ThreadPool.SetMaxThreads(DefaultThreadsNo, DefaultThreadsNo);
        foreach (var username in _loggedUsers.Keys)
        {
            if (_loggedUsers.TryGetValue(username, out var client))
            {
                Task.Run(() =>
                {
                    try
                    {
                        Console.WriteLine($"Notifying [{username}]");
                        client.UpdateTrips(trips);
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine("Error notifying " + e.Message);
                    }
                });
            }
        }
    }

    public IEnumerable<Trip> Get_All_Trips()
    {
        return _tripService.GetAll();
    }

    public IEnumerable<Trip> Get_All_Trip_By_Destination_From_To(string destination, int startDate, int finishDate)
    {
        return _tripService.filterTrips(destination, startDate, finishDate);
    }

    public Account MakeAccount(string username, string password)
    {
        var account = _accountService.createAccount(username, password);
        if (_accountService.findByUsername(username) != null)
        {
            throw new Exception("Account already exists.");
        }

        _accountService.Save(account);
        return account;
    }

    public Account FindAccount(string username, string password)
    {
        return _accountService.findByUsernameAndPassword(username, password);
    }

    public virtual bool Login(Account account, AppObserverInterface observer)
    {
        var accountOpt = _accountService.findByUsernameAndPassword(account.Username, account.Password);
        if (accountOpt != null)
        {
            if (_loggedUsers.ContainsKey(accountOpt.Id))
                throw new Exception("User already logged in.");
            _loggedUsers.TryAdd(accountOpt.Id, observer);
        }
        else
        {
            throw new Exception("Authentication failed.");

        }
        return true;
    }

    public void Register(string username, string password)
    {
        var account = new Account(username, password);
        var accountExists = _accountService.FindOne(account.Id);

        if (accountExists != null)
        {
            throw new Exception("Account already exists.");
        }
        else
        {
            _accountService.Save(account);
        }
    }

    public virtual void Logout(Account account, AppObserverInterface observer)
    {
        var accountExists = _accountService.FindOne(account.Id);

        if (accountExists != null)
        {
            if (!_loggedUsers.ContainsKey(account.Id))
                throw new Exception("User is not logged in.");
            _loggedUsers.TryRemove(account.Id, out _);
        }
        else
        {
            throw new Exception("Account does not exist.");
        }
    }

    public virtual void MakeReservation(Account account, string name, string phone, long tickets, Trip trip)
    {
        if (trip == null)
        {
            throw new Exception("Trip is null.");
        }

        var reservation = _reservationService.createReservation(account, name, phone, tickets, trip);

        if (reservation == null)
        {
            throw new Exception("Reservation is null.");
        }

        trip.AvailableSeats -= (int)reservation.Tickets;
        var updateResult = _tripService.Update(trip);
        if (updateResult == null)
            throw new Exception("Trip not found.");

       
        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        NotifyClients(_tripService.GetAll());
    }
}