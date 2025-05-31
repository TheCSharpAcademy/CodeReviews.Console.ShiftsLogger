using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HKHemanthSharma.ShiftsLogger.Model
{
    public class Shift
    {
        [JsonPropertyName("shiftId")]
        public int shiftId { get; set; }

        [JsonPropertyName("workerId")]
        public int workerId { get; set; }

        [JsonPropertyName("shiftStartTime")]
        public string shiftStartTime { get; set; } // Treat as string

        [JsonPropertyName("shiftEndTime")]
        public string shiftEndTime { get; set; } // Treat as string

        [JsonPropertyName("shiftDuration")]
        public double? shiftDuration { get; set; }

        [JsonPropertyName("shiftDate")]
        public string shiftDate { get; set; } // Treat as string
    }
}
