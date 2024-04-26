using System.Collections.Generic;
using AppDomain.Domain;

namespace AppServices.Service;

public interface ServiceAppInterface
{
    bool Login(Account account, AppObserverInterface observer);
    void Register(string usernanme, string password);
    void Logout(Account account, AppObserverInterface observer);
    void MakeReservation(Account account, string name, string phone, long tickets, Trip trip);
    IEnumerable<Trip> Get_All_Trips();
    IEnumerable<Trip> Get_All_Trip_By_Destination_From_To(string destination, int startDate, int finishDate);

    Account MakeAccount(string username, string password);
    Account FindAccount(string username, string password);
}