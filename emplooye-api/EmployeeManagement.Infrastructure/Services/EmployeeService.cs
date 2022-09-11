using EmployeeManagement.Application.Commands.CreateEmployee;
using EmployeeManagement.Application.Commands.UpdateEmployee;
using EmployeeManagement.Application.Queries.GetEmployeeById;
using MongoDB.Bson;

namespace EmployeeManagement.Infrastructure.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }
    public async Task<Tuple<List<GetEmployeesQueryResponse>,int,int>> GetAllEmployees(int pageNumber, int pageSize)
    {
        var employees =  await _repository.GetEmployees();
        
        var filteredEmployee = employees.Skip((pageNumber - 1) * pageSize).Take(pageSize); 
        
        var employeesResponses = filteredEmployee.Select(employee => new GetEmployeesQueryResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                JobTitle = employee.JobTitle,
                Phone = employee.Phone,
                ImageUrl = employee.ImageUrl
            }).ToList();
        
        return new Tuple<List<GetEmployeesQueryResponse>,int,int>(employeesResponses, employees.Count, (employees.Count/pageSize)+1);
    }

    public async Task<GetEmployeeByIdQueryResponse> GetEmployeeById(string id) 
    {
            var employee =  await _repository.GetEmployee(id);
            
            if (employee is null)
            {
                return new GetEmployeeByIdQueryResponse();
            }
            
            return new GetEmployeeByIdQueryResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                JobTitle = employee.JobTitle,
                Phone = employee.Phone,
                ImageUrl = employee.ImageUrl
            };
    }

    public async Task UpdateEmployee( UpdateEmployeeCommand employee)
    {
        await _repository.UpdateEmployee(new Employee
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            JobTitle = employee.JobTitle,
            Phone = employee.Phone,
            ImageUrl = employee.ImageUrl
        });
    }
    
    public async Task DeleteEmployee(string id) => await _repository.DeleteEmployee(id);

    public async Task<CreateEmployeeCommandResponse> AddEmployee(CreateEmployeeCommand newEmployee)
    {
        var employee = new Employee
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = newEmployee.Name,
            Email = newEmployee.Email,
            JobTitle = newEmployee.JobTitle,
            Phone = newEmployee.Phone,
            ImageUrl = newEmployee.ImageUrl
        };
        await _repository.CreateEmployee(employee);
        return new CreateEmployeeCommandResponse
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            JobTitle = employee.JobTitle,
            Phone = employee.Phone,
            ImageUrl = employee.ImageUrl
        };
    }

}