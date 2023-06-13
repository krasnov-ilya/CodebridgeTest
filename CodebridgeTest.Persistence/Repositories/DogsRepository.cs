using CodebridgeTest.Domain.Entities;
using CodebridgeTest.Domain.Interfaces;
using CodebridgeTest.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CodebridgeTest.Persistence.Repositories;

public class DogsRepository : BaseRepository<Dog>, IDogsRepository
{
    public DogsRepository(DogsContext dogsContext) 
        : base(dogsContext)
    {
    }

    public async Task<Dog?> Get(string name) => 
        await _dogsContext.Dogs.FirstOrDefaultAsync(x => x.Name == name);

    public async Task<IEnumerable<Dog>> Get(Func<Dog, bool> predicate)
    {
        var dogs = _dogsContext.Dogs
            .AsQueryable()
            .Where(predicate)
            .ToList();
        
        return await Task.FromResult<IEnumerable<Dog>>(dogs);
    }

    public override async Task<bool> Create(Dog dog)
    {
        var existingDog = await Get(dog.Name);
        if (existingDog is null)
            return false;
        
        await base.Create(dog);
        return true;
    }
}