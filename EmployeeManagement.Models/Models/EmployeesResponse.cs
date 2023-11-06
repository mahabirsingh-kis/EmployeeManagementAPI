namespace EmployeeManagement.Models.Models;

public class EmployeesResponse
{
    public int EmployeeId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTimeOffset? DOB { get; set; }
    public int? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
}
