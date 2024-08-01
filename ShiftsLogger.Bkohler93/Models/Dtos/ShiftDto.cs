namespace Models;

public class PostShiftDto {
    public required string Name { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }
}

public class PutShiftDto {
    public required string Name { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }
}

public class GetShiftDto {
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }
}