using System;

namespace ShiftLoggerApi.Dtos;

public class StartShiftDto
{
    public string? EmployeeName { get; set; }
    public DateTime Start { get; set; }
}
