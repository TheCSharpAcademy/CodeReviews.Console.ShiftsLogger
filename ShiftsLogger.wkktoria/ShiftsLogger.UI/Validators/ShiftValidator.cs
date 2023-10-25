namespace ShiftsLogger.UI.Validators;

public static class ShiftValidator
{
    public static bool IsStartDateBeforeFinishDate(DateTime startDate, DateTime finishDate)
    {
        return startDate < finishDate;
    }
}