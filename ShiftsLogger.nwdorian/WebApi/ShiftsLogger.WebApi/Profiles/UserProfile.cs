using AutoMapper;
using ShiftsLogger.Model;
using ShiftsLogger.WebApi.RestModels;

namespace ShiftsLogger.WebApi.Profiles;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<User, UserRead>().ReverseMap();
		CreateMap<UserCreate, User>();
		CreateMap<UserUpdate, User>();
	}
}
