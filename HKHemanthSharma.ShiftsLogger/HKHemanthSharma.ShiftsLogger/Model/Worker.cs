using System.Text.Json.Serialization;

namespace HKHemanthSharma.ShiftsLogger.Model
{
    public class Worker
    {
        [property: JsonPropertyName("workerId")]
        public int WorkerId { get; set; }
        [property: JsonPropertyName("name")]
        public string Name { get; set; }
        [property: JsonPropertyName("shifts")]
        public List<Shift> Shifts { get; set; }
    }
}
