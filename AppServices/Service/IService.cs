#nullable enable
using System.Collections.Generic;
using AppDomain.Domain;

namespace AppServices.Service;

public interface IService<TId,TE > where TE: Entity<TId>
{
    TE? FindOne(TId id);
    TE? Save(TE entity);
    TE? Delete(TE entity);
    TE? Update(TE entity);
    List<TE> GetAll();
}