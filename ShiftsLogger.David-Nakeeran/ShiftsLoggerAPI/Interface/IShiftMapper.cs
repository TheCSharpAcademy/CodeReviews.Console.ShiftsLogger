

using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Interface;

public interface IShiftMapper
{
    ShiftDTO ShiftToDTO(Shift shift);
}