﻿using EmployeeManagement.Application.Interfaces;
using AutoFixture;
using EmployeeManagement.Application.Queries.GetEmployees;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Services;
using Moq;
using FluentAssertions;

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
        await service.GetAllEmployees();
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
        var response = await service.GetAllEmployees();
        
        response.First().Id.Should().Be(employeeList.First().Id);
        response.First().Name.Should().Be(employeeList.First().Name);
        response.First().Email.Should().Be(employeeList.First().Email);
        response.First().Phone.Should().Be(employeeList.First().Phone);
        response.First().ImageUrl.Should().Be(employeeList.First().ImageUrl);
        response.First().JobTitle.Should().Be(employeeList.First().JobTitle);
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
        var response = await service.GetAllEmployees();
        
        _repository.Verify(f => f.GetEmployees(), Times.Once);

        response.Count.Should().Be(employeeList.Count);
    }
}