﻿using AutoMapper;
using EmployeeManagement.DAL;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Models;
using EmployeeManagement.Web.Helpers;
using EmployeeManagement.Web.Repository;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Web.Services;

public class EmployeeRepository : IEmployeeRepository
{
    private ApiContext _apiContext;
    private readonly IMapper _mapper;
    public EmployeeRepository(ApiContext apiContext, IMapper mapper)
    {
        _apiContext = apiContext;
        _mapper = mapper;
        SeedDepartments();
    }

    public void CreateEmployee(EmployeeCreateRequest employeeCreateRequest)
    {
        // Validate
        if(_apiContext.Employees.Any(x=>x.Email == employeeCreateRequest.Email))
            throw new AppException("User with the email '" + employeeCreateRequest.Email + "' already exists");

        var employee = _mapper.Map<Employee>(employeeCreateRequest);
        _apiContext.Employees.Add(employee);
        _apiContext.SaveChanges();
    }

    public List<EmployeesResponse> GetEmployees()
    {
        var employeeList = _apiContext.Employees.Include(x=>x.Department).ToList();
        var responseList = _mapper.Map<List<EmployeesResponse>>(employeeList);
        return responseList;
    }

    public Employee GetEmployeeById(int employeeId)
    {
       return getEmployee(employeeId);
    }

    public void UpdateEmployee(int employeeId, EmployeeUpdateRequest employeeUpdateRequest)
    {
        var employee = getEmployee(employeeId);

        // Validate
        if (employeeUpdateRequest.Email != employee.Email && _apiContext.Employees.Any(x => x.Email == employeeUpdateRequest.Email))
            throw new AppException("User with the email '" + employeeUpdateRequest.Email + "' already exists");

        _mapper.Map(employeeUpdateRequest, employee);
        _apiContext.Employees.Update(employee);
        _apiContext.SaveChanges();

    }

    public void DeleteEmployee(int employeeId)
    {
        var employee = getEmployee(employeeId);
        _apiContext.Employees.Remove(employee);
        _apiContext.SaveChanges();
    }

    #region Methods
    private Employee getEmployee(int employeeId)
    {
        var employee = _apiContext.Employees.Find(employeeId);
        if(employee == null) throw new KeyNotFoundException("Employee not found");
        return employee;
    }

    private void SeedDepartments()
    {
        if (_apiContext.Departments.Count() == 0)
        {
            var departmentsList = new List<Department>
        {
            new Department
            {
                DepartmentName="Management"
            },
            new Department
            {
                DepartmentName="Accounts"
            }
        };
            _apiContext.Departments.AddRange(departmentsList);
            _apiContext.SaveChanges();
        }
       
    }
    #endregion
}
