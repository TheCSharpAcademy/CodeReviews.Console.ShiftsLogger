namespace ShiftsLogger.Api.Contracts.V1.Requests;

/// <summary>
/// The required data to perform a request for the Shift object.
/// </summary>
public class ShiftRequest
{
    #region Properties

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    #endregion
}
