#nullable enable
using Lab3.Domain;

namespace Lab3.Repository;

public interface IRepositoryAccount: IRepository<long, Account>
{
    Account? findByUsername(string username);
}