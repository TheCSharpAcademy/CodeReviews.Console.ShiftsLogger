using ShiftsLogger.Dejmenek.UI.Models;

namespace ShiftsLogger.Dejmenek.UI.Helpers;
public static class Mapper
{
    public static ShiftReadDTO ToShiftReadDto(Shift shift)
    {
        return new ShiftReadDTO
        {
            EmployeeFirstName = shift.EmployeeFirstName,
            EmployeeLastName = shift.EmployeeLastName,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            Duration = shift.Duration,
        };
    }

    public static List<ShiftReadDTO> ToShiftReadDtoList(List<Shift> shifts)
    {
        List<ShiftReadDTO> shiftDtos = new List<ShiftReadDTO>();

        foreach (var shift in shifts)
        {
            shiftDtos.Add(ToShiftReadDto(shift));
        }

        return shiftDtos;
    }

    public static EmployeeDTO ToEmployeeDto(Employee employee)
    {
        return new EmployeeDTO
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
        };
    }

    public static EmployeeReadDTO ToEmployeeReadDto(Employee employee)
    {
        return new EmployeeReadDTO
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
        };
    }

    public static List<EmployeeReadDTO> ToEmployeeReadDtoList(List<Employee> employees)
    {
        List<EmployeeReadDTO> employeeDtos = new List<EmployeeReadDTO>();

        foreach (var employee in employees)
        {
            employeeDtos.Add(ToEmployeeReadDto(employee));
        }

        return employeeDtos;
    }
}
