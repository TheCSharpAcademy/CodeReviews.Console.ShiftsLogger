namespace ShiftsLoggerUI;

public class ShiftModel
{
    public long id { get; set; }
    public DateTime date { get; set; }
    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }
    public TimeSpan duration { get; set; }
}

enum Menu
{
    ViewShifts,
    EnterShift,
    UpdateShift,
    DeleteShift,
    Quit

}