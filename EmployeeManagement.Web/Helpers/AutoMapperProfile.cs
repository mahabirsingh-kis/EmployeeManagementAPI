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
        CreateMap<DepartmentResponse, Department>();
        CreateMap<Department, DepartmentResponse>();
        CreateMap<Employee, EmployeesResponse>()
            .ForMember(x => x.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.DepartmentName : string.Empty));
    }
}
