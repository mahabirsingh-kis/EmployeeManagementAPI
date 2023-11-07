using EmployeeManagement.DAL;
using EmployeeManagement.Web.Repository;
using EmployeeManagement.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApiContext>();
string[] departments = new string[] { "HR", "DevOps", "Development" };
for (int i = 0; i < departments.Length; i++)
{
    if (!dbContext.Departments.Any(x => x.DepartmentName == departments[i]))
    {
        dbContext.Departments.Add(new EmployeeManagement.Models.Entity.Department
        {
            DepartmentName = departments[i],
            DepartmentId = i + 1
        });
    }
}
dbContext.SaveChanges();
app.Run();
