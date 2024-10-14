using System;
using ShiftLogger.Api.Models;

namespace ShiftLogger.Api;

public static class ShiftData
{
    public static List<Shift> Shifts { get; set; } = new List<Shift>()
    {
        new Shift() 
        {
            Id = 1,
            EmployeeName = "Adam",
            Start = new DateTime(2024, 09, 26).AddHours(12).AddMinutes(30),
            End = new DateTime(2024, 09, 26).AddHours(15)
        },

        new Shift()
        {
            Id = 2,
            EmployeeName = "Eve",
            Start = new DateTime(2024, 09, 26).Add(new TimeSpan(0, 12, 00, 00)),
            End = new DateTime(2024, 09, 26).AddHours(16)
        }
    };
}
