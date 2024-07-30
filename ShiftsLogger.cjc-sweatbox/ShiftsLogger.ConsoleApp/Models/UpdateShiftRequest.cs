namespace ShiftsLogger.ConsoleApp.Models;

/// <summary>
/// The required data to perform an update request for the Shift object.
/// </summary>
public class UpdateShiftRequest
{
    #region Properties

    public Guid Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    #endregion
}
