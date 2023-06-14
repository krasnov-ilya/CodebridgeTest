using CodebridgeTest.API.DTOs;
using CodebridgeTest.Domain.Options;
using CodebridgeTest.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CodebridgeTest.API.Controllers;

[ApiController]
public class DogsController : ControllerBase
{
    private readonly IDogsService _dogsService;
    private readonly ServiceInfo _serviceInfoOptions;

    public DogsController(IDogsService dogsService, IOptions<ServiceInfo> serviceInfoOptions)
    {
        _dogsService = dogsService;
        _serviceInfoOptions = serviceInfoOptions.Value;
    }
    
    // [HttpGet("/dogs")]
    // public async Task<IActionResult> Index()
    // {
    //     var dogs = await _dogsService.Get();
    //     return Ok(dogs);
    // }
    
    [HttpGet("/dogs")]
    public async Task<IActionResult> Get([FromQuery] DogsFilterDto dogsFilterDto, [FromQuery] PaginationFilterDto paginationFilterDto)
    {
        try
        {
            if (dogsFilterDto.IsEmpty && paginationFilterDto.IsEmpty)
            {
                var dogs = await _dogsService.Get();
                return Ok(dogs);
            }
            
            var dogsFilter = DogsFilterDto.ToDomain(dogsFilterDto);
            var paginationFilter = PaginationFilterDto.ToDomain(paginationFilterDto);
            var orderedDogs = await _dogsService.Get(dogsFilter, paginationFilter);

            return Ok(orderedDogs);
        }
        catch (Exception e)
        {
            return UnprocessableEntity(
                $"Dog does not contain attribute! ({nameof(dogsFilterDto.Attribute)} '{dogsFilterDto.Attribute}')");
        }
    }
    
    [HttpPost("/dog")]
    public async Task<IActionResult> Create([FromBody] DogDto dogDto)
    {
        try
        {
            var dog = DogDto.ToDomain(dogDto);
            var isCreated = await _dogsService.Create(dog);
            if (isCreated)
                return Created(nameof(Create), dog);

            return UnprocessableEntity("Entity with same name already exists!");
        }
        catch (Exception e)
        {
            return UnprocessableEntity(e.Message);
        }
    }
    
    [HttpGet("/ping")]
    public IActionResult GetVersion()
    {
        return Ok(_serviceInfoOptions.Version);
    }
}