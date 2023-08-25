namespace ShiftLoggerCarDioLogicsAPI.Model;

public class Shift
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan Duration => (EndDate - StartDate);
}
