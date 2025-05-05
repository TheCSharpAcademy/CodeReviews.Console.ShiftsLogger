using ShiftsLogger.Ryanw84.Models;

namespace ShiftsLogger.Ryanw84.Data.Helpers;

public class TimezoneConverter
{
    public void AdjustTimeZoneByLocation(Shift shift)
    {
        if (shift.Location.Name == "Colchester General Hospital")
        {
            var englishTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            shift.StartTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, englishTimeZone);
        }
        else if (shift.Location.Name == "Royal Adelaide Hospital")
        {
            var australianTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
                "AUS Eastern Standard Time"
            );
            shift.StartTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, australianTimeZone);
        }
        else if (shift.Location.Name == "New York Presbyterian Hospital")
        {
            var newYorkTimeZone = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            shift.StartTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, newYorkTimeZone);
        }
        else if (shift.Location.Name == "Toronto Western Hospital")
        {
            var newYorkTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            shift.StartTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, newYorkTimeZone);
        }
        else
        {
            Console.WriteLine("Invalid Location, Please try again!");
            AdjustTimeZoneByLocation(shift);
        }
    }
}
