namespace CodebridgeTest.Domain.Filters;

public class PaginationFilter
{
    private const int MaxPageSize = 10;
    private const int MinPageNumber = 1;
    
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    
    public PaginationFilter(int pageNumber = MinPageNumber, int pageSize = MaxPageSize)
    {
        PageNumber = pageNumber < MinPageNumber 
            ? MinPageNumber 
            : pageNumber;

        PageSize = pageSize > MaxPageSize 
            ? MaxPageSize 
            : pageSize;
    }
}