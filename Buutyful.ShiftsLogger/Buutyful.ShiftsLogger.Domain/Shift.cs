namespace Buutyful.ShiftsLogger.Domain;

public record Shift
{
    public Guid Id { get; private set; }
    public Worker Worker { get; private set; }
    public DateTime StartAt { get; private set; }
    public DateTime EndAt { get; private set; }
    public TimeSpan Duration { get; private set; }
    private Shift() { }
    public static Shift Create(Worker worker, DateTime startAt, DateTime endAt)
    {
        if (startAt > endAt) throw new ArgumentException("end time cant be lower than start time");
        return new()
        {
            Id = Guid.NewGuid(),
            Worker = worker,
            StartAt = startAt,
            EndAt = endAt,
            Duration = startAt - endAt
        };

    }
}
