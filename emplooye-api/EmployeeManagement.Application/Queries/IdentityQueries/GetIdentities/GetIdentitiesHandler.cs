using EmployeeManagement.Application.Interfaces.Identity;

namespace EmployeeManagement.Application.Queries.IdentityQueries;

public class GetAllIdentityHandler:IRequestHandler<GetAllIdentity, List<GetAllIdentityResponse>>
{
    private readonly IIdentityService _service;

    public GetAllIdentityHandler(IIdentityService service)
    {
        _service = service;
    }

    public async  Task<List<GetAllIdentityResponse>> Handle(GetAllIdentity request, CancellationToken cancellationToken)
    {
        var users =   await _service.GetAllIdentity(request.Page,request.PageSize);
        

        return users;
    }
}