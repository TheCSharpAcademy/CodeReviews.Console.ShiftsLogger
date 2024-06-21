namespace Shiftlogger.Entities;

public class Worker
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<Shift> Shifts { get; set; } = new ();
}