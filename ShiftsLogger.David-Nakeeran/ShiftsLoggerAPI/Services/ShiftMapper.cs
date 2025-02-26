using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Services;

public class ShiftMapper : IShiftMapper
{
    public ShiftDTO ShiftToDTO(Shift shift) =>
        new ShiftDTO
        {
            ShiftId = shift.ShiftId,
            EmployeeId = shift.EmployeeId,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            EmployeeName = shift.Employee.Name,
        };
}