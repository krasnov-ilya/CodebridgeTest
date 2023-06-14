using CodebridgeTest.API.DTOs;
using CodebridgeTest.Domain.Options;
using CodebridgeTest.Infrastructure.Intefaces;
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
    
    [HttpGet("/dogs")]
    public async Task<IActionResult> Index()
    {
        var dogs = await _dogsService.Get();
        return Ok(dogs);
    }
    
    // [HttpGet("/dogs")]
    // public Task<IActionResult> Get([FromQuery] DogsFilterDto dogsFilterDto, [FromQuery] PaginationFilterDto paginationFilterDto)
    // {
    //     
    // }
    
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