using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Models;
using EmployeeManagement.Web.Controllers;
using EmployeeManagement.Web.Repository;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Test.Controllers;

public class EmployeeControllerTest
{
    private readonly EmployeeController _employeeController;
    private readonly Mock<IEmployeeRepository> _service;

    public EmployeeControllerTest()
    {
        _service = new Mock<IEmployeeRepository>();
        _employeeController = new EmployeeController(_service.Object);
    }

    [Fact]
    public void Get_WhenCalled_ReturnsOKResult()
    {
        var okResult = _employeeController.GetAll();
        Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
    }

    [Fact]
    public void Get_WhenCalled_ReturnsAllItems()
    {
        var empList = GetEmployeeData();
        _service.Setup(x => x.GetEmployees())
            .Returns(empList);
        var employeeResult = _employeeController.GetAll() as OkObjectResult;

        Assert.NotNull(employeeResult);
        Assert.True(empList.Equals(employeeResult.Value));
    }


    [Fact]
    public void GetById_ExistingIdPassed_ReturnsOKResult()
    {
        var okResult = _employeeController.GetEmployeeById(1);
        Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
    }

    [Fact]
    public void GetById_ExistingIdPasses_ReturnsRightItem()
    {
        var emp = GetEmployee();
        _service.Setup(x => x.GetEmployeeById(1))
            .Returns(emp);

        var employeeResult = _employeeController.GetEmployeeById(1) as OkObjectResult;

        Assert.NotNull(employeeResult);
        Assert.True(emp.Equals(employeeResult.Value));
    }

   

    private List<EmployeesResponse> GetEmployeeData()
    {
        List<EmployeesResponse> empData = new List<EmployeesResponse>
        {
            new EmployeesResponse
            {
                EmployeeId= 1,
                Name= "Emp1",
                Email="emp1@mailinator.com",
                DOB=DateTime.Now.AddYears(-20),
                DepartmentId=1,
                DepartmentName="Accounts"
            }
        };
        return empData;
    }

    private Employee GetEmployee()
    {

        var emp = new Employee
        {
            EmployeeId = 1,
            Name = "Emp1",
            Email = "emp1@mailinator.com",
            DOB = DateTime.Now.AddYears(-20),
            DepartmentId = 1
        };
        return emp;
    }
}
