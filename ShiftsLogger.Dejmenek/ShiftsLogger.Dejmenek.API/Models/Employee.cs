namespace ShiftsLogger.Dejmenek.API.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}

public class EmployeeReadDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public List<ShiftReadDTO> Shifts { get; set; } = new List<ShiftReadDTO>();
}

public class EmployeeUpdateDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}

public class EmployeeCreateDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
