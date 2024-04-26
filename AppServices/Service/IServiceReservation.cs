using System;
using AppDomain.Domain;

namespace AppServices.Service;

public interface IServiceReservation: IService<long, Reservation>
{
    Reservation createReservation(Account account, String clientName, String phoneNumber, long tickets, Trip trip);
}