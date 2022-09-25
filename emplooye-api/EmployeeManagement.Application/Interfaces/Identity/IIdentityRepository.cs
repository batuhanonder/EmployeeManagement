namespace EmployeeManagement.Application.Interfaces.Identity;
using  Domain.Entities;
public interface IIdentityRepository
{
    Task<List<Domain.Entities.Identity>> GetIdentities();
    
    Task<Identity> HasAnyIdentity(string username, string email);
    
    

    Task<Identity> CheckIdentity(string userName, string password);
    
    Task<Identity> GetIdentityById(string id);
    Task CreateIdentity(Identity identity);

    Task UpdateIdendity(Identity identity);

    Task DeleteIdentity(string id);
}