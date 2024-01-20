namespace ShiftsLoggerWebApi.Models;

public class Employee
{
    public int EmployeeId {get; set;}
    public string Name {get; set;} = "";
    public List<Shift>? Shifts {get; set;}
}