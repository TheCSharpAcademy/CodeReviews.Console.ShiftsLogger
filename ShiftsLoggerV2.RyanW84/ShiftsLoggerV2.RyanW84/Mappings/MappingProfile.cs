using AutoMapper;

using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;

namespace ShiftsLoggerV2.RyanW84.Mappings;

public class MappingProfile: Profile
	{
	public MappingProfile( )
		{
		CreateMap<Shift , ShiftApiRequestDto>();
		CreateMap<ShiftApiRequestDto , Shift>();
		}
	}
	
