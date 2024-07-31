using Swashbuckle.AspNetCore.Annotations;

namespace Api.Models.Dtos;

public class PostWorkerShiftDto {
    public int WorkerId { get; set; }
    public int ShiftId { get; set; }

    [SwaggerSchema("The date the shift was worked on", Format = "date")]
    public DateOnly ShiftDate { get; set; }
}

public class PutWorkerShiftDto {
    public int WorkerId { get; set; }
    public int ShiftId { get; set; }

    [SwaggerSchema("The date the shift was worked on", Format = "date")]
    public DateOnly ShiftDate { get; set; }
}

public class GetWorkerShiftDto {
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public int ShiftId { get; set; }
    public DateOnly ShiftDate { get; set; }
    public required GetWorkerDto Worker { get; set; }
    public required GetShiftDto Shift { get; set; }
}