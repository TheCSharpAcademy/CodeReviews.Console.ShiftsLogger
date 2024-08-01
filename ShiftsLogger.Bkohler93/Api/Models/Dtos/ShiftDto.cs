namespace Api.Models.Dtos;

# pragma warning disable CS1591
public class PostShiftDto {
    public required string Name { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}

public class PutShiftDto {
    public required string Name { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}

public class GetShiftDto {
    public int Id { get; set; }
    public required string Name { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
# pragma warning restore CS1591