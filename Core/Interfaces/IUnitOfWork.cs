using Core.Entities;
using Core.Entities.OrderAggregate;
using System.Formats.Tar;

namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> Complete();

}
