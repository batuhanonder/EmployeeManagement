using EmployeeManagement.Application.Commands.CreateEmployee;
using EmployeeManagement.Application.Commands.DeleteEmployee;
using EmployeeManagement.Application.Commands.UpdateEmployee;
using EmployeeManagement.Application.Queries.GetEmployeeById;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("/employee")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
       var response =  await _mediator.Send(new GetEmployeesQuery());
       
       if (!response.Any())
       {
           return NotFound();
       }
       
       return Ok(response);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> Get(string id)
    {
        var response = await _mediator.Send(new GetEmployeeByIdQuery(id));
        if (response.Id is null)
        {
            return NotFound();
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateEmployeeCommand createEmployeeCommand)
    {
        await _mediator.Send(createEmployeeCommand);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string id)
    {
        await _mediator.Send(new DeleteEmployeeCommand(id));
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommand updateEmployeeCommand)
    {
        await _mediator.Send(updateEmployeeCommand);
        return Ok();
    }

}