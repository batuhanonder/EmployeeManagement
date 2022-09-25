using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.Commands.IdentityCommands.LoginCommand;

public class LoginCommand : IRequest<LoginCommandResponse>
{
    [Required]
    [StringLength(20, MinimumLength=5)]
    public string Username { get; set; }
    
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }
}