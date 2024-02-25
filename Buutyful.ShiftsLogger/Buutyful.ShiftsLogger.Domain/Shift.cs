namespace Buutyful.ShiftsLogger.Domain;

public class Shift
{
    public Guid Id { get; private set; }
    public Guid WorkerId { get; private set; }
    public DateTime ShiftDay { get; private set; }
    public DateTime StartAt { get; private set; }
    public DateTime EndAt { get; private set; }
    public TimeSpan Duration { get; private set; }
    private Shift() { }
    public static Shift Create(Guid workerId,DateTime shiftDay, DateTime startAt, DateTime endAt)
    {
        if (startAt > endAt) throw new ArgumentException("end time cant be lower than start time");
        return new()
        {
            Id = Guid.NewGuid(),
            ShiftDay = shiftDay,
            WorkerId = workerId,
            StartAt = startAt,
            EndAt = endAt,
            Duration = startAt - endAt
        };
    }
}
