namespace EmployeeManagement.Application.Queries.IdentityQueries;

public class GetAllIdentity : IRequest<List<GetAllIdentityResponse>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public GetAllIdentity(int page, int pageSize)
    {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize > 50 ? 50 : pageSize;
    }
}