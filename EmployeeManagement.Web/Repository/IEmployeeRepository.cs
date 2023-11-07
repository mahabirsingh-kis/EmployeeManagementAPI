using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Models;

namespace EmployeeManagement.Web.Repository;

public interface IEmployeeRepository
{
    public List<EmployeesResponse> GetEmployees();
    public List<DepartmentResponse> GetDepartments();
    void CreateEmployee(EmployeeCreateRequest employeeCreateRequest);
    Employee GetEmployeeById(int employeeId);
    void UpdateEmployee(int employeeId, EmployeeUpdateRequest employeeUpdateRequest);
    void DeleteEmployee(int employeeId);

}
