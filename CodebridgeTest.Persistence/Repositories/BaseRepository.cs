using CodebridgeTest.Domain.Entities;
using CodebridgeTest.Domain.Interfaces;
using CodebridgeTest.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CodebridgeTest.Persistence.Repositories;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DogsContext _dogsContext;

    protected BaseRepository(DogsContext dogsContext)
    {
        _dogsContext = dogsContext;
    }

    public virtual async Task<IEnumerable<TEntity>> Get() =>
        await _dogsContext.Set<TEntity>().ToListAsync();

    public virtual async Task<TEntity?> Get(int id) =>
        await _dogsContext.Set<TEntity>().FindAsync(id);

    public virtual async Task Create(TEntity entity)
    {
        await _dogsContext.Set<TEntity>().AddAsync(entity);
        await _dogsContext.SaveChangesAsync();
    }

    public virtual async Task Update(TEntity entity)
    {
        _dogsContext.Entry(entity).State = EntityState.Modified;
        await _dogsContext.SaveChangesAsync();
    }

    public virtual async Task Delete(int id)
    {
        await _dogsContext.Set<TEntity>()
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
}