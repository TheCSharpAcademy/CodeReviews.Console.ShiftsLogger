namespace ShiftsLogger.ConsoleApp.Models;

/// <summary>
/// The required data to perform a create request for the Shift object.
/// </summary>
public class CreateShiftRequest
{
    #region Properties

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    #endregion
}
