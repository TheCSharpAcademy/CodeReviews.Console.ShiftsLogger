namespace Shiftlogger.UI.DTOs;

public class ShiftNewDto
{
    public int workerId { get; set; }
    public DateTime? startDateTime { get; set; }
    public DateTime? endDateTime { get; set; }
}

public class ShiftDto
{
    public int id { get; set; }
    public int workerId { get; set; }
    public DateTime? startDateTime { get; set; }
    public DateTime? endDateTime { get; set; }
}

public class ShiftReqestDto : ShiftDto
{
    public string duration { get; set; }
    //[JsonIgnore]
    public WorkerDto worker { get; set; }
}