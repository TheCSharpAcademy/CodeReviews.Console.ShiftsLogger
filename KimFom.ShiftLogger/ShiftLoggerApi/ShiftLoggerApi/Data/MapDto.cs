using ShiftLoggerApi.Dtos;
using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.Data;

public static class MapDto
{
    public static ShiftReadDto MapToReadDto(Shift shift)
    {
        var shiftReadDto = new ShiftReadDto
        {
            Id = shift.Id,
            Name = shift.Name,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            Duration = shift.Duration
        };

        return shiftReadDto;
    }

    public static List<ShiftReadDto> MapToReadDtoList(List<Shift> shifts)
    {
        var shiftReadDtos = new List<ShiftReadDto>();
        foreach (var shift in shifts)
        {
            var temp = new ShiftReadDto
            {
                Id = shift.Id,
                Name = shift.Name,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                Duration = shift.Duration
            };
            shiftReadDtos.Add(temp);
        }

        return shiftReadDtos;
    }

    public static Shift MapFromWriteDto(ShiftWriteDto shiftDto)
    {
        var shift = new Shift { Name = shiftDto.Name, StartTime = shiftDto.StartTime };

        return shift;
    }

    public static void MapFromUpdateDto(Shift oldShift, ShiftUpdateDto shiftDto)
    {
        oldShift.EndTime = shiftDto.EndTime;
        oldShift.Duration = shiftDto.Duration;
    }
}