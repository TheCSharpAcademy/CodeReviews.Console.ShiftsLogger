using System.ComponentModel.DataAnnotations.Schema;
using ShiftsLoggerApi.Employees;

namespace ShiftsLoggerApi.Shifts.Models;

public record class ShiftCreateDto
(
    int EmployeeId,
    DateTime StartTime,
    DateTime EndTime
);