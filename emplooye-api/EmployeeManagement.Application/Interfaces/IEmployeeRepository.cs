namespace EmployeeManagement.Application.Interfaces;

public interface IEmployeeRepository
{

    Task<List<Employee>> GetEmployees();
    Task<Employee?> GetEmployee(string id);
    Task CreateEmployee(Employee employee);
    Task UpdateEmployee(Employee employee);
    Task DeleteEmployee(string id);
}