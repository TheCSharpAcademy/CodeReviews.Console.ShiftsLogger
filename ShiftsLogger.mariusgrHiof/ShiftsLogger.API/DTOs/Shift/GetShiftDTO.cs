using System.Text.Json.Serialization;

namespace ShiftsLogger.API.DTOs.Shift
{
    public class GetShiftDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime End { get; set; }

        public TimeSpan Duration => End - Start;

        [JsonPropertyName("workerId")]
        public int WorkerId { get; set; }
    }
}
