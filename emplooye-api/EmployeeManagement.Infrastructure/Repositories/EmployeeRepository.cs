namespace EmployeeManagement.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IMongoCollection<Employee> _employeeCollection;
    
    public EmployeeRepository(IMongoDatabase mongoDatabase)
    {
        _employeeCollection = mongoDatabase.GetCollection<Employee>("Employees");
    }
    
    public async Task<List<Employee>> GetEmployees() => await _employeeCollection.Find(_ => true).ToListAsync();

    public async Task<Employee?> GetEmployee(string id) => await _employeeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateEmployee(Employee employee) => await _employeeCollection.InsertOneAsync(employee);
    
    public async Task UpdateEmployee(Employee employee) => await _employeeCollection.ReplaceOneAsync( i => i.Id == employee.Id, employee);

    public async Task DeleteEmployee(string id) => await _employeeCollection.DeleteOneAsync(i => i.Id == id);

}