#nullable enable
using System.Collections.Generic;
using AppDomain.Domain;

namespace AppPersistence.Repository;

public interface IRepositoryTrip : IRepository<long, Trip>
{
    Trip? findByDestination(string destination);
    List<Trip> filterTrips(string destination, int startHour, int finishHour);
}