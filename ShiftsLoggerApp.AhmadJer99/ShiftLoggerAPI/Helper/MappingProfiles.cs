using AutoMapper;
using ShiftsLoggerAPI.Dto;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Shift,ShiftDto>();
        CreateMap<Employee,EmployeeDto>();
    }
}
