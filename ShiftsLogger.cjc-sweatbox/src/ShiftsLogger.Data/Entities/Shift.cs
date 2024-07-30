namespace ShiftsLogger.Data.Entities;

/// <summary>
/// Database version of the Shift model.
/// </summary>
public class Shift
{
    #region Properties

    public Guid Id { get; set; }

    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }

    #endregion
}
