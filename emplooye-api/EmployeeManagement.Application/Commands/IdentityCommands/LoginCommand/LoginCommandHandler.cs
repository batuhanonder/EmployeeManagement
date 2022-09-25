using System.Security.Claims;
using System.Text;
using EmployeeManagement.Application.Interfaces.Identity;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement.Application.Commands.IdentityCommands.LoginCommand;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    private readonly IIdentityService _service;


    public LoginCommandHandler(IIdentityService service)
    {
        _service = service;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await _service.GetIdentityByUserName(request.Username, request.Password);
        return response;
    }
}