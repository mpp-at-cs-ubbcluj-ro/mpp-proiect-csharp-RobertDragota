using AppDomain.Domain;
using Lab3.Utility.Validation;

namespace AppPersistence.Utility.Validation;

public class ValidatorTrip : Validator<Trip>
{
    public void validate(Trip entity)
    {
        if(entity == null)
            throw new ValidException("Entity is null");
        if(string.IsNullOrEmpty(entity.Destination))
            throw new ValidException("Destination is empty");
        if(string.IsNullOrEmpty(entity.TransportCompany))
            throw new ValidException("Transport Company is empty");
        if(entity.Price <= 0)
            throw new ValidException("Price cannot be negative or zero");
        if(entity.AvailableSeats < 0)
            throw new ValidException("Seats is negative");
    }
}
