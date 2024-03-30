#nullable enable
using System.Collections.Generic;
using Lab3.Domain;

namespace Lab3.Repository;

public interface IRepositoryTrip : IRepository<long, Trip>
{
    Trip? findByDestination(string destination);
    List<Trip> filterTrips(string destination, int startHour, int finishHour);
}