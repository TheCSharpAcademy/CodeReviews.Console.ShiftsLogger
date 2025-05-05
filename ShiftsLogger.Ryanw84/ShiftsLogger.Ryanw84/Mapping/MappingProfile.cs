using AutoMapper;

using ShiftsLogger.Ryanw84.Dtos;
using ShiftsLogger.Ryanw84.Models;

namespace ShiftsLogger.Ryanw84.Mapping;

public class MappingProfile: Profile
    {
    public MappingProfile( )
        {
        CreateMap<Shift , ShiftApiRequestDto>();
        CreateMap<ShiftApiRequestDto , Shift>();
        }
    }
