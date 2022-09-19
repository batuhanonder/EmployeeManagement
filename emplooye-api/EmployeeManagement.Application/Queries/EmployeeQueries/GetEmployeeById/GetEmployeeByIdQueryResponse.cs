namespace EmployeeManagement.Application.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email{ get; set; }
    public string JobTitle{ get; set; }
    public string Phone{ get; set; }
    public string ImageUrl{ get; set; }
}