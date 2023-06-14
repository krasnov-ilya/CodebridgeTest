using CodebridgeTest.Domain.Entities;
using CodebridgeTest.Domain.Filters;
using CodebridgeTest.Domain.Interfaces;
using CodebridgeTest.Infrastructure.Extensions;
using CodebridgeTest.Infrastructure.Intefaces;

namespace CodebridgeTest.Infrastructure.Services;

public class DogsService : IDogsService
{
    private readonly IDogsRepository _dogsRepository;
    
    public DogsService(IDogsRepository dogsRepository)
    {
        _dogsRepository = dogsRepository;
    }

    public async Task<Dog?> Get(int id) => 
        await _dogsRepository.Get(id);

    public async Task<Dog?> Get(string name) => 
        await _dogsRepository.Get(name);

    public async Task<IEnumerable<Dog>> Get() => 
        await _dogsRepository.Get();

    public async Task<IEnumerable<Dog>> Get(DogsFilter dogsFilter, PaginationFilter paginationFilter)
    {
        var dogs = await Get();
        
        var isDescending = dogsFilter.Order == "desc";
        var orderedDogs = dogs.AsQueryable()
            .OrderBy(dogsFilter.Attribute, isDescending);
        
        return orderedDogs.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToList();
    }

    public async Task<bool> Create(Dog dog)
    {
        return await _dogsRepository.Create(dog);
    }
}