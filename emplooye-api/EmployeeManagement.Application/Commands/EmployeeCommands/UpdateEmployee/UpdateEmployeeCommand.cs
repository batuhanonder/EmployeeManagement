namespace EmployeeManagement.Application.Commands.UpdateEmployee;

public class UpdateEmployeeCommand : IRequest
{
    public UpdateEmployeeCommand(string id, string name, string email, string jobTitle, string phone, string ımageUrl)
    {
        Id = id;
        Name = name;
        Email = email;
        JobTitle = jobTitle;
        Phone = phone;
        ImageUrl = ımageUrl;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Email{ get; set; }
    public string JobTitle{ get; set; }
    public string Phone{ get; set; }
    public string ImageUrl{ get; set; }
}