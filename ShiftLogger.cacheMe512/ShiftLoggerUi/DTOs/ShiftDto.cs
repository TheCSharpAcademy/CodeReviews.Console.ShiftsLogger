namespace ShiftLoggerUi.DTOs;

internal class ShiftDto
{
    public int ShiftId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int WorkerId { get; set; }
}
