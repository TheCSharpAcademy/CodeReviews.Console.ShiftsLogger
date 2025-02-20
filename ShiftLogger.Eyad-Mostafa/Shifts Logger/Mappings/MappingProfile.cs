using AutoMapper;
using Shifts_Logger.DTOs;
using Shifts_Logger.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Worker, WorkerDto>();
        CreateMap<CreateWorkerDto, Worker>();

        CreateMap<Shift, ShiftDto>()
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.EndTime - src.StartTime));

        CreateMap<CreateShiftDto, Shift>();
    }
}
