using WebApplication1.Models;

namespace WebApplication1
{
    public class Services
    {
        public static TimeSpan CalculateTime(Worker worker)
        {
            TimeSpan workedHours = worker.End - worker.Begin;
            return workedHours.Duration();
        }
    }
}
