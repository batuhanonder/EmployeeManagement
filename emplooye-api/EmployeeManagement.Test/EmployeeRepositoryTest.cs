namespace EmployeeManagement.Test;

public class EmployeeRepositoryTest
{
    private readonly Mock<IEmployeeRepository> _repository;

    public EmployeeRepositoryTest()
    {
        _repository = new Mock<IEmployeeRepository>();
    }

    [Fact]
    public async Task GetAllEmployees_Verify()
    {
        var employeeList = new List<Employee>
        {
            new Employee
            {
                Id = "1",
                Name = "testuser",
                Email = "test@test.com",
                JobTitle = "test",
                Phone = "5555555555",
                ImageUrl = "image.jpg"
            }
        };
        _repository.Setup(r => r.GetEmployees()).ReturnsAsync(employeeList);
        var service = new EmployeeService(_repository.Object);
        await service.GetAllEmployees(1,10);
        _repository.Verify(f => f.GetEmployees(), Times.Once);
    }
    
    [Fact]
    public async Task GetAllEmployees_hasOneRecord()
    {
        var employeeList = new List<Employee>
        {
            new Employee
            {
                Id = "1",
                Name = "testuser",
                Email = "test@test.com",
                JobTitle = "test",
                Phone = "5555555555",
                ImageUrl = "image.jpg"
            }
        };
        _repository.Setup(r => r.GetEmployees()).ReturnsAsync(employeeList);
        
        var service = new EmployeeService(_repository.Object);
        var (employees, count, pageSize) = await service.GetAllEmployees(1,10);
        
        employees.First().Id.Should().Be(employeeList.First().Id);
        employees.First().Name.Should().Be(employeeList.First().Name);
        employees.First().Email.Should().Be(employeeList.First().Email);
        employees.First().Phone.Should().Be(employeeList.First().Phone);
        employees.First().ImageUrl.Should().Be(employeeList.First().ImageUrl);
        employees.First().JobTitle.Should().Be(employeeList.First().JobTitle);
    }
    [Fact]
    public async Task GetAllEmployees_thereIsMoreThanOneRecords()
    {
        var employeeList = new List<Employee>
        {
            new Employee
            {
                Id = "1",
                Name = "testuser",
                Email = "test@test.com",
                JobTitle = "test",
                Phone = "5555555555",
                ImageUrl = "image.jpg"
            },
            new Employee
            {
                Id = "2",
                Name = "test",
                Email = "test@test.com",
                JobTitle = "tester",
                Phone = "5444444444",
                ImageUrl = "test.jpg"
            }
        };
        _repository.Setup(r => r.GetEmployees()).ReturnsAsync(employeeList);
        
        var service = new EmployeeService(_repository.Object);
        var (response, count, pageSize) = await service.GetAllEmployees(1,10);
        
        _repository.Verify(f => f.GetEmployees(), Times.Once);

        response.Count.Should().Be(employeeList.Count);
    }
}