namespace EmployeeManagement.Application.Commands.IdentityCommands.RegisterCommand;

public class RegisterCommandResponse
{
  
    public string Username { get; set; }
    
    public string email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string[] HasRoles { get; set; }
    
}