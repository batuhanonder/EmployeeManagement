namespace EmployeeManagement.Application.Queries.IdentityQueries;

public class GetAllIdentityResponse
{
    public string[] HasRoles { get; set; }
    public string Username { get; set; }

    public string email { get; set; }
    
    public string PhoneNumber { get; set; }
}

