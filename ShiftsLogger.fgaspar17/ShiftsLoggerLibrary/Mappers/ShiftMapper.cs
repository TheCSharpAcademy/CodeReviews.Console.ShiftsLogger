using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLoggerLibrary;

public class ShiftMapper
{
    public static ShiftDto MapToDto(Shift shift)
    {
        return new ShiftDto
        {
            Id = shift.ShiftId,
            Start = shift.Start,
            End = shift.End,
            DurationMinutes = shift.DurationMinutes,
            EmployeeName = shift.EmployeeName,
        };
    }
}