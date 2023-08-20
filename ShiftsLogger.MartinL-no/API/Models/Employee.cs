namespace API.Models;

internal class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public ICollection<Shift> Shifts { get; set; }
}
