using CodebridgeTest.Domain.Entities;
using CodebridgeTest.Domain.Filters;
using CodebridgeTest.Domain.Interfaces;
using CodebridgeTest.Infrastructure.Extensions;
using CodebridgeTest.Infrastructure.Interfaces;

namespace CodebridgeTest.Infrastructure.Services;

public class DogsService : IDogsService
{
    private readonly IDogsRepository _dogsRepository;
    
    public DogsService(IDogsRepository dogsRepository)
    {
        _dogsRepository = dogsRepository;
    }

    public async Task<IEnumerable<Dog>> Get() => 
        await _dogsRepository.Get();

    public async Task<IEnumerable<Dog>> Get(DogsFilter dogsFilter, PaginationFilter paginationFilter)
    {
        var dogs = await Get();
        var isDescending = dogsFilter.Order == "desc";

        dogs = isDescending 
            ? dogs.OrderByDescending(x => x.Name) 
            : dogs.OrderBy(x => x.Name);
        
        if (!string.IsNullOrWhiteSpace(dogsFilter.Attribute))
        {
            dogs = dogs.AsQueryable()
                .OrderBy(dogsFilter.Attribute, isDescending)
                .ToList();   
        }

        return dogs.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize)
            .ToList();
    }

    public async Task<bool> Create(Dog dog) => 
        await _dogsRepository.Create(dog);
}