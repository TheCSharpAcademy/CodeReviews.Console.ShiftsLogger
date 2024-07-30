using ShiftsLogger.ConsoleApp.Constants;

namespace ShiftsLogger.ConsoleApp.Models;

/// <summary>
/// UI display version of the Shift model.
/// </summary>
internal class ShiftDto
{
    #region Constructors

    public ShiftDto(Guid id, DateTime startTime, DateTime endTime)
    {
        Id = id;
        StartTime = startTime;
        EndTime = endTime;
    }

    #endregion
    #region Properties

    internal Guid Id { get; set; }

    internal DateTime StartTime { get; set; }

    internal DateTime EndTime { get; set; }

    internal double DurationInHours => (EndTime - StartTime).TotalHours;

    #endregion
    #region Methods

    internal string ToSelectionChoice()
    {
        return $"{StartTime.ToString(StringFormat.DateTime)} - {EndTime.ToString(StringFormat.DateTime)} ({DurationInHours:F2} Hours)";
    }

    #endregion
}
