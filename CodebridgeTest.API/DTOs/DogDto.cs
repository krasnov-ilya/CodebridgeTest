using CodebridgeTest.Domain.Entities;

namespace CodebridgeTest.API.DTOs;

public class DogDto
{
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int TailLength { get; set; }
    public int Weight { get; set; }

    public static DogDto ToDto(Dog dog)
    {
        return new DogDto
        {
            Name = dog.Name,
            Color = dog.Color,
            TailLength = dog.TailLength,
            Weight = dog.Weight
        };
    }

    public static Dog ToDomain(DogDto dogDto) => 
        new(dogDto.Name, dogDto.Color, dogDto.TailLength, dogDto.Weight);
}