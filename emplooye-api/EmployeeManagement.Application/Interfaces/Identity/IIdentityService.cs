using EmployeeManagement.Application.Commands.IdentityCommands.LoginCommand;
using EmployeeManagement.Application.Commands.IdentityCommands.RegisterCommand;

namespace EmployeeManagement.Application.Interfaces.Identity;
using Domain.Entities;
public interface IIdentityService
{

    Task<Identity> GetIdentityById(string id);

    Task<LoginCommandResponse> GetIdentityByUserName(string userName, string password);

    Task UpdateIdentity(Identity identity);
    
    Task DeleteIdentity(string id);
    
    Task<RegisterCommandResponse> AddIdentity(RegisterCommand identity);
}