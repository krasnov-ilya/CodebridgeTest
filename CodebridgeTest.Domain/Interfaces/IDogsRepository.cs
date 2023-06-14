using CodebridgeTest.Domain.Entities;

namespace CodebridgeTest.Domain.Interfaces;

public interface IDogsRepository : IRepository<Dog>
{
    Task<Dog?> Get(string name);
    new Task<bool> Create(Dog dog);
}