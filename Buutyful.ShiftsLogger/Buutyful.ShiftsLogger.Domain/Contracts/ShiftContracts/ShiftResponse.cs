using System.Text.Json.Serialization;

namespace Buutyful.ShiftsLogger.Domain.Contracts.Shift;

public record ShiftResponse(
    Guid ShiftId,
    Guid WorkerId,
    DateTime ShiftDay,
    DateTime StartAt,
    DateTime EndAt,
    TimeSpan Duration)
{
    public static implicit operator ShiftResponse(Domain.Shift shift) =>
        new(shift.Id, shift.WorkerId,shift.ShiftDay, shift.StartAt, shift.EndAt, shift.Duration);
}

public class ShiftResponseJson
{
    [JsonPropertyName("shiftDay")]
    public DateTime ShiftDay { get; set; }
    [JsonPropertyName("startAt")]
    public DateTime StartAt { get; set; }

    [JsonPropertyName("endAt")]
    public DateTime EndAT { get; set; }

    [JsonPropertyName("Duration")]
    public TimeSpan Duration { get; set; }

    public override string ToString()
    {
        return $"Day: {ShiftDay},\n StartedAt: {StartAt},\n EndedAt: {EndAT},\n Duration: {Duration}\n";
    }
}