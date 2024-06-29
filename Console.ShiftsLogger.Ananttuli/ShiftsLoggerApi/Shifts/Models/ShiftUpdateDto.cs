using System.ComponentModel.DataAnnotations.Schema;
using ShiftsLoggerApi.Employees;

namespace ShiftsLoggerApi.Shifts.Models;

public record class ShiftUpdateDto
(
    int ShiftId,
    DateTime StartTime,
    DateTime EndTime
);