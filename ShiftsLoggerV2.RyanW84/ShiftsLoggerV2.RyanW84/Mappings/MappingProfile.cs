using AutoMapper;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;

namespace ShiftsLoggerV2.RyanW84.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Shifts, ShiftApiRequestDto>();
        CreateMap<ShiftApiRequestDto, Shifts>();
        CreateMap<Workers, WorkerApiRequestDto>();
        CreateMap<WorkerApiRequestDto, Workers>();
        CreateMap<Locations, LocationApiRequestDto>();
        CreateMap<LocationApiRequestDto, Locations>();
    }
}
