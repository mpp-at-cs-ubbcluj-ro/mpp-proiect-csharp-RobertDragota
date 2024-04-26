using System.Collections.Generic;
using AppDomain.Domain;

namespace AppServices.Service;

public interface AppObserverInterface
{
    void UpdateTrips(IEnumerable<Trip> list);
}