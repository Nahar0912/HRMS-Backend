using AutoMapper;
using HRMS.Backend.Models;
using HRMS.Backend.DTOs;

namespace HRMS.Backend.Mappings
{
    public static class MappingExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreNull<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> map)
        {
            map.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            return map;
        }
    }
    
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<EmployeeCreateDTO, Employee>();
            CreateMap<EmployeeUpdateDTO, Employee>().IgnoreNull();
            CreateMap<Employee, EmployeeDTO>();

            CreateMap<SalaryCreateDTO, Salary>();
            CreateMap<SalaryUpdateDTO, Salary>().IgnoreNull();
            CreateMap<Salary, SalaryDTO>();

            CreateMap<PayrollCreateDTO, Payroll>();
            CreateMap<PayrollUpdateDTO, Payroll>().IgnoreNull();
            CreateMap<Payroll, PayrollDTO>();

            CreateMap<RegisterDTO, User>();
            CreateMap<UpdateUserDTO, User>().IgnoreNull();
        }
    }
}
