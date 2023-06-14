using CodebridgeTest.Domain.Entities;
using CodebridgeTest.Domain.Filters;

namespace CodebridgeTest.Infrastructure.Interfaces;

public interface IDogsService
{
    Task<IEnumerable<Dog>> Get();
    Task<IEnumerable<Dog>> Get(DogsFilter dogsFilter, PaginationFilter paginationFilter);
    Task<bool> Create(Dog dog);
}