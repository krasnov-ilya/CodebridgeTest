using CodebridgeTest.Domain.Filters;

namespace CodebridgeTest.API.DTOs;

public class PaginationFilterDto
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public bool IsEmpty => PageNumber == 0 && PageSize == 0;
    
    public static PaginationFilterDto ToDto(PaginationFilter paginationFilter)
    {
        return new PaginationFilterDto
        {
            PageNumber = paginationFilter.PageNumber,
            PageSize = paginationFilter.PageSize
        };
    }
    
    public static PaginationFilter ToDomain(PaginationFilterDto paginationFilterDto)
    {
        return new PaginationFilter(
            paginationFilterDto.PageNumber,
            paginationFilterDto.PageSize
        );
    }
}