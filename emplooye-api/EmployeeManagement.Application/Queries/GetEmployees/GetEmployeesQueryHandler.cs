namespace EmployeeManagement.Application.Queries.GetEmployees;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<GetEmployeesQueryResponse>>
{
    private readonly IEmployeeService _service;

    public GetEmployeesQueryHandler(IEmployeeService service)
    {
        _service = service;
    }

    public async Task<List<GetEmployeesQueryResponse>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllEmployees();
    }
}