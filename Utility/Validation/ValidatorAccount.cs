using Lab3.Domain;

namespace Lab3.Utility.Validation;

public class ValidatorAccount : Validator<Account>
{
    public void validate(Account entity)
    {
        if(entity == null)
            throw new ValidException("Entity is null");
        if(string.IsNullOrEmpty(entity.Username))
            throw new ValidException("Username is empty");
        if(string.IsNullOrEmpty(entity.Password))
            throw new ValidException("Password is empty");
    }
}
