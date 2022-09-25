namespace EmployeeManagement.Application.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
{
    private readonly IEmployeeService _service;

    public DeleteEmployeeCommandHandler(IEmployeeService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteEmployee(request.Id);
        return Unit.Value;
    }
}