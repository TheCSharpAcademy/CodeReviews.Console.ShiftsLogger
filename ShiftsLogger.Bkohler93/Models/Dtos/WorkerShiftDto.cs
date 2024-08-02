using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace Models;

public class PostWorkerShiftDto {

    [JsonPropertyName("workerId")]
    public required int WorkerId { get; set; }

    [JsonPropertyName("shiftId")]
    public required int ShiftId { get; set; }

    [JsonPropertyName("shiftDate")]

    [SwaggerSchema("The date the shift was worked on", Format = "date")]
    public required DateOnly ShiftDate { get; set; }
}

public class PutWorkerShiftDto {

    [JsonPropertyName("workerId")]
    public required int WorkerId { get; set; }

    [JsonPropertyName("shiftId")]
    public required int ShiftId { get; set; }

    [JsonPropertyName("shiftDate")]

    [SwaggerSchema("The date the shift was worked on", Format = "date")]
    public required DateOnly ShiftDate { get; set; }
}

public class GetWorkerShiftDto {

   [JsonPropertyName("id")] 
    public required int Id { get; set; }

    [JsonPropertyName("workerId")]
    public required int WorkerId { get; set; }

    [JsonPropertyName("shiftId")]
    public required int ShiftId { get; set; }

    [JsonPropertyName("shiftDate")]
    public required DateOnly ShiftDate { get; set; }

    [JsonPropertyName("worker")]
    public required GetWorkerDto Worker { get; set; }

    [JsonPropertyName("shift")]
    public required GetShiftDto Shift { get; set; }
}