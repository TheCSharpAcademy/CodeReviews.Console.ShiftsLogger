using AutoMapper;
using ShiftsLogger.DAL.Entities;
using ShiftsLogger.Model;

namespace ShiftsLogger.Repository.Profiles;
public class ShiftProfile : Profile
{
	public ShiftProfile()
	{
		CreateMap<ShiftEntity, Shift>().ReverseMap();
	}
}
