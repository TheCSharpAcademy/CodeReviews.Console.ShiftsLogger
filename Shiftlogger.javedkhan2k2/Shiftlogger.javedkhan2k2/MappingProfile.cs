using AutoMapper;
using Shiftlogger.Entities;
using Shiftlogger.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Worker, WorkerRequestDto>();
        //CreateMap<Shift, ShiftRequestDto>();
        CreateMap<Shift, ShiftRequestDto>()
            .ForMember(dest => dest.Worker, opt => opt.MapFrom(src => src.Worker)); // Ensure Worker is mapped
    }
}
