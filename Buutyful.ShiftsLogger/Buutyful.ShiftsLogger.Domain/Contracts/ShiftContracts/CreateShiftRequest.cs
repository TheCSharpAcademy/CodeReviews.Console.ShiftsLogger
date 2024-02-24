namespace Buutyful.ShiftsLogger.Domain.Contracts.Shift;

public record CreateShiftRequest(
    Worker Worker,
    DateTime StartAt,
    DateTime EndAt);

