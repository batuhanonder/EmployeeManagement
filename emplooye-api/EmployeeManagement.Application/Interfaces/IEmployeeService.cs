namespace EmployeeManagement.Application.Interfaces;

public interface IEmployeeService
{
    Task<Tuple<List<GetEmployeesQueryResponse>,int,int>> GetAllEmployees(int pageNumber, int pageSize);
    Task<GetEmployeeByIdQueryResponse> GetEmployeeById(string id);
    Task UpdateEmployee(UpdateEmployeeCommand employee);
    Task DeleteEmployee(string id);
    Task<CreateEmployeeCommandResponse> AddEmployee(CreateEmployeeCommand employee);
}