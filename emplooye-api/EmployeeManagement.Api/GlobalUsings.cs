// Global using directives

global using EmployeeManagement.Api.Requests;
global using EmployeeManagement.Application.Behaviours;
global using EmployeeManagement.Application.Commands.IdentityCommands.LoginCommand;
global using EmployeeManagement.Application.Commands.IdentityCommands.RegisterCommand;
global using Microsoft.AspNetCore.Mvc;
global using EmployeeManagement.Application.Interfaces;
global using EmployeeManagement.Application.Queries.GetEmployees;
global using EmployeeManagement.Application.Validations;
global using EmployeeManagement.Domain.Models;
global using EmployeeManagement.Infrastructure.Repositories;
global using EmployeeManagement.Infrastructure.Services;
global using FluentValidation;
global using MediatR;
global using Microsoft.AspNetCore.Rewrite;
global using MongoDB.Driver;