#nullable enable
using AppDomain.Domain;

namespace AppServices.Service;

public interface IServiceAccount : IService<long,Account>
{
    Account? findByUsername(string username);
    Account? findByUsernameAndPassword(string username, string password);
    Account createAccount(string username, string password);
}