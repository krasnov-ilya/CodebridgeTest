using CodebridgeTest.Domain.Filters;
using FluentAssertions;

namespace DomainTests.Filters;

public class PaginationFilterTests
{
    private const int MaxPageSize = 10;
    private const int MinPageNumber = 1;
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCase(-1, -1)]
    [TestCase(0, 0)]
    [TestCase(-100, 2000)]
    public void PaginationFilter_ShouldContainDefaultValues_WhenUnexpectedParameterPassed(int pageNumber, int pageSize)
    {
        var paginationFilter = new PaginationFilter(pageNumber, pageSize);
        
        paginationFilter.Should().NotBeNull();
        paginationFilter.PageNumber.Should().Be(MinPageNumber);
        paginationFilter.PageSize.Should().Be(MaxPageSize);
    }
}