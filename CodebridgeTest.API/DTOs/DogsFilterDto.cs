using CodebridgeTest.Domain.Filters;

namespace CodebridgeTest.API.DTOs;

public class DogsFilterDto
{
    public string Attribute { get; set; } = string.Empty;
    public string Order { get; set; } = string.Empty;
    
    public bool IsEmpty => 
        string.IsNullOrWhiteSpace(Attribute) && string.IsNullOrWhiteSpace(Order);

    public static DogsFilterDto ToDto(DogsFilter dogsFilter)
    {
        return new DogsFilterDto
        {
            Attribute = dogsFilter.Attribute,
            Order = dogsFilter.Order
        };
    }
    
    public static DogsFilter ToDomain(DogsFilterDto dogsFilterDto)
    {
        return new DogsFilter
        {
            Attribute = dogsFilterDto.Attribute,
            Order = dogsFilterDto.Order
        };
    }
}