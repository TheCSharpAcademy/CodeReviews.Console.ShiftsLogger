public record class Shift(int id, int workerId, DateTime startDateTime, DateTime endDateTime);
public record class ShiftDto(int WorkerId, DateTime StartDateTime, DateTime EndDateTime);