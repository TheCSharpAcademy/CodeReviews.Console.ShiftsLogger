using System.Text.Json.Serialization;

namespace ShiftWebApi.Models;


public class Shift
{
    public int Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int UserId { get; set; }

    [JsonIgnore]
    internal User? User { get; set; }
}

public class ShiftDTO
{
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
}