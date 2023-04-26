using AutoMapper;

namespace sadklouds.ShiftLogger.Profiles;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Shift, GetShiftDto>();
        CreateMap<AddShiftDto, Shift>();
        CreateMap<UpdateShiftDto, Shift>();
    }
}
