using EmployeeManagement.Application.Interfaces.Employee;

namespace EmployeeManagement.Application.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
{
    private readonly IEmployeeService _service;

    public UpdateEmployeeCommandHandler(IEmployeeService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateEmployee(request);
        return Unit.Value;
    }
}