using System.Text.Json.Serialization;

namespace Models;

public class PostShiftDto {
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("startTime")]
    public required TimeOnly StartTime { get; set; }
    [JsonPropertyName("endTime")]
    public required TimeOnly EndTime { get; set; }
}

public class PutShiftDto {
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("startTime")]
    public required TimeOnly StartTime { get; set; }
    [JsonPropertyName("endTime")]
    public required TimeOnly EndTime { get; set; }
}

public class GetShiftDto {
    [JsonPropertyName("id")] 
    public required int Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("startTime")]
    public required TimeOnly StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public required TimeOnly EndTime { get; set; }
}