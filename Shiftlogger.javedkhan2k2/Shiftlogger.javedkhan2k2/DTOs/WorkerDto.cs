using Shiftlogger.Entities;

namespace Shiftlogger.DTOs;

public class WorkerAddDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

public class WorkerPutDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<Shift> Shifts { get; set; } = new ();
    public Worker ToWorker()
    {
        return new Worker
        {
            Id = this.Id,
            Name = this.Name,
            Email = this.Email,
            PhoneNumber = this.PhoneNumber
        };
    }
}

public class WorkerRequestDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
