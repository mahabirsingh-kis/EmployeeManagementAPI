using EmployeeManagement.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DAL;

public class ApiContext:DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "EmployeeDb");
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
}
