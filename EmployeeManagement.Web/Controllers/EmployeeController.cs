﻿using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Models;
using EmployeeManagement.Web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var list = _employeeRepository.GetEmployees();
        return Ok(new { data = list });
    }

    [HttpGet("GetDepartments")]
    public IActionResult GetDepartments()
    {
        var list = _employeeRepository.GetDepartments();
        return Ok(new { data = list });
    }

    [HttpGet("GetEmployeeById")]
    public IActionResult GetEmployeeById(int id)
    {
        var employee = _employeeRepository.GetEmployeeById(id);
        return Ok(employee);
    }

    [HttpPost("CreateEmployee")]
    public IActionResult CreateEmployee(EmployeeCreateRequest employeeCreateRequest)
    {
        _employeeRepository.CreateEmployee(employeeCreateRequest);
        return Ok(new { message = "Employee created" });
    }

    [HttpPost("UpdateEmployee")]
    public IActionResult UpdateEmployee(EmployeeUpdateRequest employeeUpdateRequest)
    {
        _employeeRepository.UpdateEmployee(employeeUpdateRequest.EmployeeId, employeeUpdateRequest);
        return Ok(new { message = "Employee updated" });
    }

    [HttpDelete("DeleteEmployee")]
    public IActionResult DeleteEmployee(int id)
    {
        _employeeRepository.DeleteEmployee(id);
        return Ok(new { message = "Employee deleted", data = true });
    }
}
