using AutoMapper;
using ShiftsLoggerUI.Models;
using ShiftsLoggerUI.Dto;

namespace ShiftsLoggerUI.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Shift, ShiftDto>();
        CreateMap<Employee, EmployeeDto>();
    }
}
