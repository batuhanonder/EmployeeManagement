using EmployeeManagement.Application.Commands.IdentityCommands.LoginCommand;
using EmployeeManagement.Application.Commands.IdentityCommands.RegisterCommand;
using EmployeeManagement.Application.Queries.IdentityQueries;

namespace EmployeeManagement.Application.Interfaces.Identity;
using Domain.Entities;
public interface IIdentityService
{
    Task<List<GetAllIdentityResponse>> GetAllIdentity(int pageNumber, int pageSize);
    
    Task<Identity> GetIdentityById(string id);

    Task<LoginCommandResponse> GetIdentityByUserName(string userName, string password);

    Task UpdateIdentity(Identity identity);
    
    Task DeleteIdentity(string id);
    
    Task<RegisterCommandResponse> AddIdentity(RegisterCommand identity);
}