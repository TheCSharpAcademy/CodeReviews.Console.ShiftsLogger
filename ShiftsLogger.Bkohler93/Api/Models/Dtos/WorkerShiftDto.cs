using Swashbuckle.AspNetCore.Annotations;

namespace Api.Models.Dtos;

# pragma warning disable CS1591
public class PostWorkerShiftDto {
    public required int WorkerId { get; set; }
    public required int ShiftId { get; set; }

    [SwaggerSchema("The date the shift was worked on", Format = "date")]
    public required DateOnly ShiftDate { get; set; }
}

public class PutWorkerShiftDto {
    public required int WorkerId { get; set; }
    public required int ShiftId { get; set; }

    [SwaggerSchema("The date the shift was worked on", Format = "date")]
    public required DateOnly ShiftDate { get; set; }
}

public class GetWorkerShiftDto {
    public required int Id { get; set; }
    public required int WorkerId { get; set; }
    public required int ShiftId { get; set; }
    public required DateOnly ShiftDate { get; set; }
    public required GetWorkerDto Worker { get; set; }
    public required GetShiftDto Shift { get; set; }
}
# pragma warning restore CS1591