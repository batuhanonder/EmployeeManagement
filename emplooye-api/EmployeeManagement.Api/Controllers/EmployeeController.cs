using EmployeeManagement.Api.Requests;
using EmployeeManagement.Application.Commands.CreateEmployee;
using EmployeeManagement.Application.Commands.DeleteEmployee;
using EmployeeManagement.Application.Commands.UpdateEmployee;
using EmployeeManagement.Application.Queries.GetEmployeeById;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("v1/employees")]
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
    public async Task<IActionResult> Get([FromRoute]string id)
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
        var employee = await _mediator.Send(createEmployeeCommand);
        return Created($"/v1/employees/{employee.Id}",employee);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        await _mediator.Send(new DeleteEmployeeCommand(id));
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute]string id, [FromBody] UpdateEmployeeRequest request)
    {
        await _mediator.Send(new UpdateEmployeeCommand(id, request.Name, request.Email, request.JobTitle, request.Phone, request.ImageUrl));
        return NoContent();
    }

}