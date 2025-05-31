using System.Text.Json.Serialization;

namespace HKhemanthSharma.ShiftLoggerAPI.Model
{
    public class Shift
    {
        public int ShiftId { get; set; }
        public int WorkerId { get; set; }
        public DateTime ShiftStartTime { get; set; }
        public DateTime ShiftEndTime { get; set; }
        public double ShiftDuration { get; set; }
        public DateTime? ShiftDate { get; set; }
        [JsonIgnore]
        public Worker Worker { get; set; }
        public void CalculateDuration()
        {
            ShiftDuration = (ShiftEndTime - ShiftStartTime).TotalHours;
        }
    }
}
