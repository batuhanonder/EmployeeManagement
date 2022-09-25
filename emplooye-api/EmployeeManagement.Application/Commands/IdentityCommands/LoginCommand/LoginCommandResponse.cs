namespace EmployeeManagement.Application.Commands.IdentityCommands.LoginCommand;

public class LoginCommandResponse
{
    public string Token { get; set; }
    public Status Status { get; set; }
}

public class Status
{
    public int Type { get; set; }
    public string Message { get; set; }
}