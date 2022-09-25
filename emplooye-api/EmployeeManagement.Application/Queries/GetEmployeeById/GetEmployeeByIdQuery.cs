namespace EmployeeManagement.Application.Queries.GetEmployeeById;

public class GetEmployeeByIdQuery : IRequest<GetEmployeeByIdQueryResponse>
{
    public string Id { get; set; }

    public GetEmployeeByIdQuery(string id)
    {
        Id = id;
    }
}