using System.Collections.Generic;
using AppDomain.Domain;

namespace AppServices.Service;

public interface IServiceTrip: IService<long, Trip>
{
    Trip? findByDestination(string destination);
    List<Trip> filterTrips(string destination, int startHour, int finishHour);
    
}