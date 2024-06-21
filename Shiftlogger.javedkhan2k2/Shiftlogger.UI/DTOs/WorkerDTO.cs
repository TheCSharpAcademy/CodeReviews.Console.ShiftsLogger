namespace Shiftlogger.UI.DTOs;

public class WorkerNewDto
{
    public string name { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
}

public class WorkerDto
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
}

public class WorkerRequestDto : WorkerDto
{
    public List<ShiftDto> shifts { get; set; } = new ();
}



