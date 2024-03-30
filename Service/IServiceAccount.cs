using Lab3.Domain;

namespace Lab3.Service;

public interface IServiceAccount : IService<long,Account>
{
    Account? findByUsername(string username);
}