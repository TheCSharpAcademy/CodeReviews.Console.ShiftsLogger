using Swashbuckle.AspNetCore.Annotations;

namespace Models;

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