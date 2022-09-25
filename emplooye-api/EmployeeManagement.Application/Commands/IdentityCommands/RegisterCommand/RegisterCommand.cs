using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.Commands.IdentityCommands.RegisterCommand;

public class RegisterCommand : IRequest<RegisterCommandResponse>
{

    [Required]
    [StringLength(20, MinimumLength=5)]
    public string Username { get; set; }
    
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    public string email { get; set; }
    
    [Phone]
    public string PhoneNumber { get; set; }
}