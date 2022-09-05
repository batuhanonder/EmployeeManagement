namespace EmployeeManagement.Api.Requests;

public record UpdateEmployeeRequest
{
    public string Name { get; set; }
    public string Email{ get; set; }
    public string JobTitle{ get; set; }
    public string Phone{ get; set; }
    public string ImageUrl{ get; set; }
}