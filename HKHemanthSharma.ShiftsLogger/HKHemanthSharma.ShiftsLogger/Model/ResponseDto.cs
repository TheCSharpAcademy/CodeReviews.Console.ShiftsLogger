using System.Text.Json.Serialization;

namespace HKHemanthSharma.ShiftsLogger.Model
{
    public class ResponseDto<T>
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; } = new List<string>();

        [JsonPropertyName("responseTime")]
        public string ResponseTime { get; set; } // Treat as string
    }
}
