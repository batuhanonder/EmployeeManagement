namespace EmployeeManagement.Application.Queries.GetEmployees;
public class GetEmployeesQuery : IRequest<Tuple<List<GetEmployeesQueryResponse>,int,int>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public GetEmployeesQuery(int page, int pageSize)
    {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize > 50 ? 50 : pageSize;
    }
}