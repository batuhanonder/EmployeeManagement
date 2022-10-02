namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("v1/accounts")]
public class SecurityController : ControllerBase
{
    private readonly IMediator _mediator;

    public SecurityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand identityLogin)
    {
        var response = await _mediator.Send(identityLogin);
        return response.Status.Type switch
        {
            403 => Forbid(),
            401 => Unauthorized(response),
            _ => Ok(response)
        };
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
    {
        var response = await _mediator.Send(registerCommand);

        if (response == null)
        {
            return Conflict(new
            {
                errorCode = "409" ,
                error = "Conflict",
                message = $"An existing record with the id or email was already found."
            });
        }
        
        return Created($"/v1/employees/", response);
    }

   
    
}