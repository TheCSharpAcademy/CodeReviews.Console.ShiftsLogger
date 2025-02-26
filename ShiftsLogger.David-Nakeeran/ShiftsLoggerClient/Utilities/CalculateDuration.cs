namespace ShiftsLoggerClient.Utilities;

class CalculateDuration
{
    internal TimeSpan CalcDuration(DateTime start, DateTime end)
    {
        return end - start;
    }
}