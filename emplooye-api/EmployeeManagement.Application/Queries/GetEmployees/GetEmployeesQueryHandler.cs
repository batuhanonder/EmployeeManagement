namespace EmployeeManagement.Application.Queries.GetEmployees;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, Tuple<List<GetEmployeesQueryResponse>,int,int>>
{
    private readonly IEmployeeService _service;
    private readonly int _pageSize = 20;
    public GetEmployeesQueryHandler(IEmployeeService service)
    {
        _service = service;
    }

    public async Task<Tuple<List<GetEmployeesQueryResponse>,int,int>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllEmployees(request.Page,request.PageSize);
    }
}