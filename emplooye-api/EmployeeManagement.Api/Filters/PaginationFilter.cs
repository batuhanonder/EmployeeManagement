namespace EmployeeManagement.Api.Filters;

public class PaginationFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    
    public PaginationFilter()
    {
        PageNumber = 1;
        PageSize = 20;
    }
  
}