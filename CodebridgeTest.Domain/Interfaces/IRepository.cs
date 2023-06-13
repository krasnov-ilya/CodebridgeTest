using CodebridgeTest.Domain.Entities;

namespace CodebridgeTest.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity: BaseEntity
{
    Task<IEnumerable<TEntity>> Get();
    Task<TEntity?> Get(int id);
    Task Create(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(int id);
}