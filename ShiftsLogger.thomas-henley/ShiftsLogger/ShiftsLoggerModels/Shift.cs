namespace ShiftsLoggerModels;

public class Shift
{
    public int Id { get; init; }
    public int EmployeeId { get; init; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public string Duration
    {
        get
        {
            var span = End - Start;
            return $"{span.TotalHours:F2} hours";
        }
    }

    public string TimeDisplay => $"{Start:MM/dd/yyyy hh:mm tt}".PadRight(18) + " --- " + $"{End:MM/dd/yyyy hh:mm tt}";
}