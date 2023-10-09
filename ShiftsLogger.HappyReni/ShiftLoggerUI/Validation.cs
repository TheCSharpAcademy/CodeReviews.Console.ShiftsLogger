using System.Globalization;

namespace ShiftLoggerUI
{
    public class Validation
    {
        public static DateTime CheckDateTime(string date)
        {
            string format = "yyyy-MM-dd HH:mm:ss";

            if (DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return DateTime.Parse(date);
            }

            throw new Exception("Invalid DateTime format.");
        }
    }
}
