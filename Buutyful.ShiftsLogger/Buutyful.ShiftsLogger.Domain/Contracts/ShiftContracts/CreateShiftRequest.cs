namespace Buutyful.ShiftsLogger.Domain.Contracts.Shift;

public record CreateShiftRequest(
    Guid WorkerId,
    DateTime ShiftDay,
    DateTime StartAt,
    DateTime EndAt);

