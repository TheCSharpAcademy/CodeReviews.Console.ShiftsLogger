using ShiftsLogger.Ryanw84.Models;

namespace ShiftsLogger.Ryanw84.Data.Helpers;

public class TimezoneConverter
{
	public void AdjustTimeZoneByLocation(Shift shift)
		{
		// Ensure the LocationId property is not null and retrieve the first location
		var location = shift.Location;

		if(location == null || string.IsNullOrEmpty(location.Name))
			{
			Console.WriteLine("Invalid LocationId, Please try again!");
			AdjustTimeZoneByLocation(shift);
			return;
			}

		if(location.Name == "Colchester General Hospital")
			{
			var englishTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
			shift.StartTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now , englishTimeZone);
			}
		else if(location.Name == "Royal Adelaide Hospital")
			{
			var australianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
			shift.StartTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now , australianTimeZone);
			}
		else if(location.Name == "New York Presbyterian Hospital")
			{
			var newYorkTimeZone = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
			shift.StartTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now , newYorkTimeZone);
			}
		else if(location.Name == "Toronto Western Hospital")
			{
			var torontoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
			shift.StartTime = TimeZoneInfo.ConvertTime(DateTimeOffset.Now , torontoTimeZone);
			}
		else
			{
			Console.WriteLine("Invalid LocationId, Please try again!");
			AdjustTimeZoneByLocation(shift);
			}
		}
}
