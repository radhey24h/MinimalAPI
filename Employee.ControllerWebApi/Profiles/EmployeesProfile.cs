using AutoMapper;
using Emp=Employee.Models.Entities;
using Employee.ControllerWebApi.DTO;

namespace Employee.ControllerWebApi.Profiles
{
    public class EmployeesProfile : Profile
    {
        public EmployeesProfile()
        {
            CreateMap<Emp.Employee, EmployeeDto>();
            CreateMap<NewEmployeeDto, Emp.Employee>();
            CreateMap<UpdateEmployeeDto, Emp.Employee>();
        }
    }
}
