using CodebridgeTest.Domain.Entities;
using CodebridgeTest.Domain.Filters;

namespace CodebridgeTest.Infrastructure.Intefaces;

public interface IDogsService
{
    Task<Dog?> Get(int id);
    Task<Dog?> Get(string name);
    Task<IEnumerable<Dog>> Get();
    Task<IEnumerable<Dog>> Get(DogsFilter dogsFilter, PaginationFilter paginationFilter);
    Task<bool> Create(Dog dog);
}