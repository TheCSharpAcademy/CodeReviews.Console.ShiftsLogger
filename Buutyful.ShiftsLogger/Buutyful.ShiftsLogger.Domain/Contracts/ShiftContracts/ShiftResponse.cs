namespace Buutyful.ShiftsLogger.Domain.Contracts.Shift;

public record ShiftResponse(
    Worker Worker,
    DateTime StartAt,
    DateTime EndAt,
    TimeSpan Duration);

