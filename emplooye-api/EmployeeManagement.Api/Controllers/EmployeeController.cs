using EmployeeManagement.Api.Filters;
using EmployeeManagement.Application.Commands.CreateEmployee;
using EmployeeManagement.Application.Commands.DeleteEmployee;
using EmployeeManagement.Application.Commands.UpdateEmployee;
using EmployeeManagement.Application.Queries.GetEmployeeById;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("v1/employees")]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
    {

        var (employees, totalCount, pageLimit) = await _mediator.Send(new GetEmployeesQuery(filter.PageNumber, filter.PageSize));

        if (!employees.Any())
        {
            return NotFound();
        }

        return Ok(new
        {
            TotalCount = totalCount,
            PageLimit = pageLimit,
            Employees = employees,
        });
    }
    
    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var response = await _mediator.Send(new GetEmployeeByIdQuery(id));
        
        if (response.Id is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
    
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateEmployeeCommand createEmployeeCommand)
    {
        var employee = await _mediator.Send(createEmployeeCommand);
        return Created($"/v1/employees/{employee.Id}", employee);
    }
    
    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        await _mediator.Send(new DeleteEmployeeCommand(id));
        return NoContent();
    }
    
    [Authorize(Roles = "Administrator")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateEmployeeRequest request)
    {
        await _mediator.Send(new UpdateEmployeeCommand(id, request.Name, request.Email, request.JobTitle, request.Phone,
            request.ImageUrl));
        return NoContent();
    }
}