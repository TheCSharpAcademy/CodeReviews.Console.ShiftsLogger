using AutoMapper;
using ShiftsLogger.DAL.Entities;
using ShiftsLogger.Model;

namespace ShiftsLogger.Repository.Profiles;
public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<UserEntity, User>().ReverseMap();
	}
}
