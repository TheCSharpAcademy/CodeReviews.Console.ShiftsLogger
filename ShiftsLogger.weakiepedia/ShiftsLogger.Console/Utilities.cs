using ShiftsLogger.Console.Models;

namespace ShiftsLogger.Console;

public class Utilities
{
    public Employee CreateEmployeeEntity(string firstName, string lastName)
    {
        return new Employee(firstName, lastName);
    }
    
    public Shift CreateShiftEntity(int employeeId, DateTime startTime, DateTime endTime)
    {
        return new Shift(employeeId, startTime, endTime);
    }

    public TimeSpan CalculateDuration(long duration)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(duration);
        
        return timeSpan;
    }
}