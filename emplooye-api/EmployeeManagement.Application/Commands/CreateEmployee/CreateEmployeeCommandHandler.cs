namespace EmployeeManagement.Application.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Unit>
{
    private readonly IEmployeeService _service;

    public CreateEmployeeCommandHandler(IEmployeeService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        await _service.AddEmployee(request);
        return Unit.Value;
    }
}