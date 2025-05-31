using System.Text.Json.Serialization;

namespace HKHemanthSharma.ShiftsLogger.Model
{
    public class Shift
    {
        [JsonPropertyName("shiftId")]
        public int ShiftId { get; set; }

        [JsonPropertyName("workerId")]
        public int WorkerId { get; set; }

        [JsonPropertyName("shiftStartTime")]
        public string ShiftStartTime { get; set; } // Treat as string

        [JsonPropertyName("shiftEndTime")]
        public string ShiftEndTime { get; set; } // Treat as string

        [JsonPropertyName("shiftDuration")]
        public double? ShiftDuration { get; set; }

        [JsonPropertyName("shiftDate")]
        public string ShiftDate { get; set; } // Treat as string
    }
}
