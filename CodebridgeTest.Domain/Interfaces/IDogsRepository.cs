using CodebridgeTest.Domain.Entities;

namespace CodebridgeTest.Domain.Interfaces;

public interface IDogsRepository : IRepository<Dog>
{
    Task<Dog?> Get(string name);
    Task<IEnumerable<Dog>> Get(Func<Dog, bool> predicate); 
    new Task<bool> Create(Dog dog);
}