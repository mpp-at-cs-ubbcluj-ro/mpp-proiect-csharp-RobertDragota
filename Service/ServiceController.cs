using Lab3.Domain;

namespace Lab3.Service;

public class ServiceController
{
    private IServiceAccount _serviceAccount;
    private IServiceTrip _serviceTrip;
    private IServiceReservation _serviceReservation;
    
    public ServiceController(IServiceAccount serviceAccount, IServiceTrip serviceTrip, IServiceReservation serviceReservation)
    {
        _serviceAccount = serviceAccount;
        _serviceTrip = serviceTrip;
        _serviceReservation = serviceReservation;
    }
    public IServiceAccount getAccountService()
    {
        return _serviceAccount;
    }
    
    public IServiceTrip getTripService()
    {
        return _serviceTrip;
    }
    
    public  IServiceReservation getReservationService()
    {
        return _serviceReservation;
    }
    
}