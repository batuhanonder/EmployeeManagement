namespace EmployeeManagement.Application.Commands.DeleteEmployee;

public class DeleteEmployeeCommand : IRequest
{
    public DeleteEmployeeCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }

    
}