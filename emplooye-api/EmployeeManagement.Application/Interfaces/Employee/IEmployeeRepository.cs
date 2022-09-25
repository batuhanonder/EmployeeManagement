namespace EmployeeManagement.Application.Interfaces.Employee;

public interface IEmployeeRepository
{

    Task<List<Domain.Entities.Employee>> GetEmployees();
    Task<Domain.Entities.Employee?> GetEmployee(string id);
    Task CreateEmployee(Domain.Entities.Employee employee);
    Task UpdateEmployee(Domain.Entities.Employee employee);
    Task DeleteEmployee(string id);
}