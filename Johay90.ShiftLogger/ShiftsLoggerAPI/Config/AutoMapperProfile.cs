using AutoMapper;
using SharedLibrary.DTOs;
using SharedLibrary.Extensions;
using SharedLibrary.Models;

namespace ShiftsLoggerAPI.Config
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ConfigureEmployeeMappings();
            ConfigureShiftMappings();
        }

        private void ConfigureEmployeeMappings()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Employee, CreateEmployeeDto>();
            CreateMap<Employee, UpdateEmployeeDto>();

            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();

            CreateMap<CreateEmployeeDto, EmployeeDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<UpdateEmployeeDto, EmployeeDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
        }

        private void ConfigureShiftMappings()
        {
            CreateMap<Shift, ShiftDto>();
            CreateMap<Shift, CreateShiftDto>();
            CreateMap<Shift, UpdateShiftDto>();  

            CreateMap<CreateShiftDto, Shift>();
            CreateMap<UpdateShiftDto, Shift>();
            CreateMap<ShiftDto, Shift>();
        }
    }
}
