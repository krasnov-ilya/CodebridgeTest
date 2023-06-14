using CodebridgeTest.Domain.Entities;
using CodebridgeTest.Domain.Filters;
using CodebridgeTest.Domain.Interfaces;
using CodebridgeTest.Infrastructure.Interfaces;
using CodebridgeTest.Infrastructure.Services;
using FluentAssertions;
using Moq;

namespace InfrastructureTests;

public class Tests
{
    private IDogsRepository _dogsRepository = null!;
    private IDogsService _dogsService = null!;

    [SetUp]
    public void Setup()
    {
        var dogsRepositoryMock = new Mock<IDogsRepository>();
        var dogs = GetFakeDogs();
        dogsRepositoryMock.Setup(x => x.Get()).ReturnsAsync(dogs);
        _dogsRepository = dogsRepositoryMock.Object;
        _dogsService = new DogsService(_dogsRepository);
    }

    [Test]
    [TestCase("", "red", 32, 10)]
    [TestCase("Doggy", "", 32, 10)]
    [TestCase("James", "black", -32, 10)]
    [TestCase("Rob", "white", 32, -10)]
    public void CreatingDog_ShouldThrowArgumentException_WithUnexpectedParameters(string name, string color,
        int tailLength, int weight)
    {
        Assert.Throws(typeof(ArgumentException), () =>
        {
            _dogsService.Create(new Dog(
                name,
                color,
                tailLength,
                weight));
        });
    }

    [Test]
    [TestCase("Jessy", "red", 32, 10)]
    [TestCase("Doggy", "gray", 11, 24)]
    [TestCase("James", "black", 8, 20)]
    [TestCase("Rob", "white", 5, 6)]
    public void Dog_ShouldNotBeCreated_WhenDogWithSameNameAlreadyExists(string name, string color, int tailLength,
        int weight)
    {
        var createResult = _dogsService.Create(new Dog(
            name,
            color,
            tailLength,
            weight)).Result;

        createResult.Should().BeFalse();
    }

    [Test]
    [TestCase("")]
    [TestCase("asd")]
    [TestCase("asc")]
    public void Filtration_ShouldSortDogsInAscendingOrder_WhenOrderIsNotSpecified_OrNotDescending(string order)
    {
        var dogsFilter = new DogsFilter { Attribute = nameof(Dog.Name) };
        var paginationFilter = new PaginationFilter();

        var orderedDogs = _dogsService.Get(dogsFilter, paginationFilter).Result.ToList();

        orderedDogs.Should().NotBeEmpty();
        orderedDogs.Should().BeInAscendingOrder(x => x.Name);
    }

    [Test]
    public void Filtration_ShouldSortDogsInDescendingOrder_WhenOrderIsDescending()
    {
        var dogsFilter = new DogsFilter { Attribute = nameof(Dog.Name), Order = "desc" };
        var paginationFilter = new PaginationFilter();

        var orderedDogs = _dogsService.Get(dogsFilter, paginationFilter).Result.ToList();

        orderedDogs.Should().NotBeEmpty();
        orderedDogs.Should().BeInDescendingOrder(x => x.Name);
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(10)]
    [TestCase(5)]
    public void PaginatedResult_ShouldContain_SpecifiedNumberOfDogs(int pageSize)
    {
        var dogsFilter = new DogsFilter();
        var paginationFilter = new PaginationFilter(pageSize: pageSize);

        var orderedDogs = _dogsService.Get(dogsFilter, paginationFilter).Result.ToList();

        orderedDogs.Should().NotBeEmpty();
        orderedDogs.Should().HaveCount(pageSize);
    }

    [Test]
    public void PaginationAndFiltration_ShouldWorkTogether()
    {
        const int pageSize = 6;
        const int pageNumber = 2;
        
        var dogsFilter = new DogsFilter { Attribute = nameof(Dog.Name), Order = "desc"};
        var paginationFilter = new PaginationFilter(pageNumber, pageSize);

        var orderedDogs = _dogsService.Get(dogsFilter, paginationFilter).Result.ToList();

        orderedDogs.Should().NotBeEmpty();
        orderedDogs.Should().HaveCount(pageSize);
        orderedDogs.Should().BeInDescendingOrder(x => x.Name);
    }

    private static IEnumerable<Dog> GetFakeDogs()
    {
        return new List<Dog>
        {
            new("Doggy1", "red", 32, 10),
            new("Doggy2", "gray", 11, 24),
            new("Doggy3", "black", 8, 20),
            new("Doggy4", "white", 15, 26),
            new("Doggy5", "white", 25, 46),
            new("Doggy6", "white", 17, 56),
            new("Doggy7", "white", 14, 18),
            new("Doggy8", "white", 3, 13),
            new("Doggy9", "white", 1, 15),
            new("Doggy10", "white", 19, 23),
            new("Doggy11", "white", 18, 6),
            new("Doggy12", "white", 52, 16),
        };
    }
}