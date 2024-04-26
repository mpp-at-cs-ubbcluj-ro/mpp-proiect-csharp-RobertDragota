using AppDomain.Domain;
using Lab3.Utility.Validation;

namespace AppPersistence.Utility.Validation;

public class ValidatorReservation : Validator<Reservation>
{
    public void validate(Reservation entity)
    {
        if (entity == null)
            throw new ValidException("Entity is null");
        if (string.IsNullOrEmpty(entity.Account.Username))
            throw new ValidException("Tourist name is empty");
        if (string.IsNullOrEmpty(entity.PhoneNumber))
            throw new ValidException("Phone number is empty");
        if (entity.Tickets <= 0)
            throw new ValidException("Number of tickets is invalid");
        if (entity.Account.Username.Length < 3)
            throw new ValidException("Tourist name is too short");
        if (entity.PhoneNumber.Length < 10)
            throw new ValidException("Phone number is too short");
    }
}
