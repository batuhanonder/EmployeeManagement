namespace EmployeeManagement.Application.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeCommandResponse>
{
    private readonly IEmployeeService _service;

    public CreateEmployeeCommandHandler(IEmployeeService service)
    {
        _service = service;
    }

    public async Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        return await _service.AddEmployee(request);;
    }
}