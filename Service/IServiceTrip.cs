using System.Collections.Generic;
using Lab3.Domain;

namespace Lab3.Service;

public interface IServiceTrip: IService<long, Trip>
{
    Trip? findByDestination(string destination);
    List<Trip> filterTrips(string destination, int startHour, int finishHour);
    
}