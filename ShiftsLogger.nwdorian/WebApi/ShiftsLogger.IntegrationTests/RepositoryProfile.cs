using AutoMapper;
using ShiftsLogger.DAL.Entities;

namespace ShiftsLogger.IntegrationTests;
public class RepositoryProfile : Profile
{
	public RepositoryProfile()
	{
		CreateMap<UserEntity, User>().ReverseMap();
		CreateMap<ShiftEntity, Shift>().ReverseMap();
	}
}
