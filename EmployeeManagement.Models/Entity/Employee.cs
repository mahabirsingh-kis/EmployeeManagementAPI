using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models.Entity;

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTimeOffset DOB { get; set; }
    [ForeignKey("DepartmentId")]
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

}
