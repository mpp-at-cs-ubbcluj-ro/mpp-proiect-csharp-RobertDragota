#nullable enable
using AppDomain.Domain;

namespace AppPersistence.Repository;

public interface IRepositoryAccount: IRepository<long, Account>
{
    Account? findByUsername(string username);
    Account findByUsernameAndPassword(string username, string password);
}