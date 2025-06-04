using AutoMapper;
using ShiftsLogger.WebApi.RestModels;

namespace ShiftsLogger.UnitTests;
public class ControllerProfile : Profile
{
	public ControllerProfile()
	{
		CreateMap<User, UserRead>().ReverseMap();
		CreateMap<UserCreate, User>();
		CreateMap<UserUpdate, User>();
		CreateMap<Shift, ShiftRead>().ReverseMap();
		CreateMap<ShiftCreate, Shift>();
		CreateMap<ShiftUpdate, Shift>();
	}
}
