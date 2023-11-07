using AutoMapper;
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
            throw new Exception("User with the email '" + employeeCreateRequest.Email + "' already exists");

        var employee = _mapper.Map<Employee>(employeeCreateRequest);
        _apiContext.Employees.Add(employee);
        _apiContext.SaveChanges();
    }

    public List<EmployeesResponse> GetEmployees()
    {
        var department = _apiContext.Departments.ToList();
        var employeeList = _apiContext.Employees.ToList();
        foreach (var employee in employeeList)
        {
            employee.Department = department.FirstOrDefault(x => x.DepartmentId == employee.DepartmentId);
        }
        var responseList = _mapper.Map<List<EmployeesResponse>>(employeeList);
        return responseList;
    }

    public List<DepartmentResponse> GetDepartments()
    {
        var employeeList = _apiContext.Departments.ToList();
        var responseList = _mapper.Map<List<DepartmentResponse>>(employeeList);
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
            throw new Exception("User with the email '" + employeeUpdateRequest.Email + "' already exists");

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
        if(employee == null) throw new Exception("Employee not found");
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
                DepartmentName="HR"
            },
            new Department
            {
                DepartmentName="DevOps"
            },
            new Department
            {
                DepartmentName="Development"
            }
        };
            _apiContext.Departments.AddRange(departmentsList);
            _apiContext.SaveChanges();
        }
       
    }
    #endregion
}
