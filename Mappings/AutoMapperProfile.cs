using AutoMapper;
using HRMS.Backend.Models;
using HRMS.Backend.DTOs;

namespace HRMS.Backend.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Employee mappings
            CreateMap<EmployeeCreateDTO, Employee>();
            CreateMap<EmployeeUpdateDTO, Employee>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Employee, EmployeeDTO>();


            // Salary mappings
            CreateMap<SalaryCreateDTO, Salary>();
            CreateMap<SalaryUpdateDTO, Salary>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Salary, SalaryDTO>();


            // Payroll mappings
            CreateMap<PayrollCreateDTO, Payroll>();
            CreateMap<PayrollUpdateDTO, Payroll>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Payroll, PayrollDTO>()
                .ForMember(dest => dest.PayrollMonth, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.PayrollMonth, DateTimeKind.Utc)))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreatedAt, DateTimeKind.Utc)))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.UpdatedAt, DateTimeKind.Utc)));


            // User mappings
            CreateMap<RegisterDTO, User>();
            CreateMap<UpdateUserDTO, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }

}
