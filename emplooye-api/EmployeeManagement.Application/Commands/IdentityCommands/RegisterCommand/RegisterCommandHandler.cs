using EmployeeManagement.Application.Interfaces.Identity;

namespace EmployeeManagement.Application.Commands.IdentityCommands.RegisterCommand;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResponse>
{
    private readonly IIdentityService _service;

    public RegisterCommandHandler(IIdentityService service)
    {
        _service = service;
    }

    public async Task<RegisterCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _service.AddIdentity(request);
    }
}