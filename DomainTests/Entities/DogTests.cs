using CodebridgeTest.Domain.Entities;
using FluentAssertions;

namespace DomainTests.Entities;

public class DogTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCase("", "red", 32, 10)]
    [TestCase("Doggy", "", 32, 10)]
    [TestCase("James", "black", -32, 10)]
    [TestCase("Rob", "white", 32, -10)]
    public void CreatingDog_ShouldThrowArgumentException_WithUnexpectedParameters(string name, string color, int tailLength, int weight)
    {
        Assert.Throws(typeof(ArgumentException),() =>
        {
            var dog = new Dog(name, color, tailLength, weight);
        });
    }
    
    [Test]
    [TestCase("Jessy", "red", 32, 10)]
    [TestCase("Doggy", "gray", 11, 24)]
    [TestCase("James", "black", 8, 20)]
    [TestCase("Rob", "white", 5, 6)]
    public void CreatingDog_ShouldPass_WithCorrectParameters(string name, string color, int tailLength, int weight)
    {
        var dog = new Dog(name, color, tailLength, weight);
        dog.Should().NotBeNull();
    }
}