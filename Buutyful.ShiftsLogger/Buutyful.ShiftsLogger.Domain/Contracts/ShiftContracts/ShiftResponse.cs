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