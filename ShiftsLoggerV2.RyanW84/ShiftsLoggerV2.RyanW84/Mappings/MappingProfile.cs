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
		CreateMap<ShiftsDto , Shifts>()
		.ForMember(dest => dest.ShiftId , opt => opt.Ignore()) // Ignore unmapped properties
		.ForMember(dest => dest.Location , opt => opt.Ignore())
		.ForMember(dest => dest.Worker , opt => opt.Ignore());
	}
}
