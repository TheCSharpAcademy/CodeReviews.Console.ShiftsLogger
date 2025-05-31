using System.Globalization;

namespace HKHemanthSharma.ShiftsLogger
{
    public class validations
    {
        public static bool validateTime(string time)
        {
            if (time.Split(":")[0].Length == 1)
            {
                time = "0" + time;
            }
            bool res = DateTime.TryParseExact(time, "HH:mm", null, DateTimeStyles.None, out DateTime UserTime);
            return res;
        }
        public static bool validateDate(string date)
        {
            bool res = DateTime.TryParseExact(date, "dd-MM-yyyy", null, DateTimeStyles.None, out DateTime UserTime);
            return res;
        }
    }
}
