using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EmployeeManagement.Application.Commands.IdentityCommands.LoginCommand;
using EmployeeManagement.Application.Commands.IdentityCommands.RegisterCommand;
using EmployeeManagement.Application.Interfaces.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly IIdentityRepository _repository;
    private readonly IConfiguration _configuration;

    public IdentityService(IIdentityRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<Identity> GetIdentityById(string id) => await _repository.GetIdentityById(id);

    public async Task<LoginCommandResponse> GetIdentityByUserName(string userName, string password)
    {
        var user = await _repository.CheckIdentity(userName, password);
        if (user == null)
        {
            return new LoginCommandResponse()
            {
                Token = null,
                Status = new Status()
                {
                    Type = 401,
                    Message = "Wrong identity"
                }
            };
        }

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username),
        };

        foreach (var role in user.HasRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var securityKey =
            new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Secret").Value));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        return new LoginCommandResponse()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            )),
            Status = new Status()
            {
                Type = 200,
                Message = "Success"
            }
        };
    }

    public async Task UpdateIdentity(Identity identity) => await _repository.UpdateIdendity(identity);

    public async Task DeleteIdentity(string id) => await _repository.DeleteIdentity(id);

    public async Task<RegisterCommandResponse> AddIdentity(RegisterCommand request)
    {
        var user = await _repository.HasAnyIdentity(request.Username, request.email);
        
        if (user != null)
        {
            return null;
        }
        
        await _repository.CreateIdentity(new Identity
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Username = request.Username,
            Password = request.Password,
            HasRoles = new string[]
            {
                "User"
            },
            email = request.email,
            PhoneNumber = request.PhoneNumber
        });
        
        return new RegisterCommandResponse
        {
            Username = request.Username,
            
            email = request.email,
            PhoneNumber = request.PhoneNumber,
            HasRoles = new string[]
            {
                "User"
            }
        };
    } 
}