using AutoMapper;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Models;

namespace EmployeeManagement.Web.Helpers;

public class AutoMapperProfile:Profile
{
    public AutoMapperProfile()
    {
        CreateMap<EmployeeCreateRequest, Employee>();
        CreateMap<EmployeeUpdateRequest, Employee>();
        CreateMap<Employee, EmployeesResponse>()
            .ForMember(x => x.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName));
    }
}
