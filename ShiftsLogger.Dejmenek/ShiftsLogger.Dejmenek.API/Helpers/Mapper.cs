using ShiftsLogger.Dejmenek.API.Models;

namespace ShiftsLogger.Dejmenek.API.Helpers;

public static class Mapper
{
    public static ShiftReadDTO ToShiftReadDto(Shift shift)
    {
        return new ShiftReadDTO
        {
            Id = shift.Id,
            EmployeeFirstName = shift.Employee.FirstName,
            EmployeeLastName = shift.Employee.LastName,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            Duration = shift.Duration
        };
    }

    public static List<ShiftReadDTO> ToShiftReadDtoList(List<Shift> shifts)
    {
        var shiftDtos = new List<ShiftReadDTO>();

        foreach (var shift in shifts)
        {
            shiftDtos.Add(ToShiftReadDto(shift));
        }

        return shiftDtos;
    }

    public static EmployeeReadDTO ToEmployeeReadDto(Employee employee)
    {
        return new EmployeeReadDTO
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Shifts = ToShiftReadDtoList(employee.Shifts.ToList()),
        };
    }

    public static List<EmployeeReadDTO> ToEmployeeReadDtoList(List<Employee> employees)
    {
        var employeeDtos = new List<EmployeeReadDTO>();

        foreach (var employee in employees)
        {
            employeeDtos.Add(ToEmployeeReadDto(employee));
        }

        return employeeDtos;
    }

    public static Shift FromShiftCreateDto(ShiftCreateDTO shiftDto)
    {
        return new Shift
        {
            EmployeeId = shiftDto.EmployeeId,
            StartTime = shiftDto.StartTime,
            EndTime = shiftDto.EndTime,
            Duration = shiftDto.Duration
        };
    }

    public static Employee FromEmployeeCreateDto(EmployeeCreateDTO employeeDto)
    {
        return new Employee
        {
            FirstName = employeeDto.FirstName,
            LastName = employeeDto.LastName
        };
    }
}
