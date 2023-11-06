using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.Models;

public class EmployeeCreateRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string DOB { get; set; }
    public int DepartmentId { get; set; }
}
