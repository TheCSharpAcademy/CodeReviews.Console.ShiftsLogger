using System.Text.Json.Serialization;

namespace ShiftLogger.Models
{
    public record class Shift(
       [property: JsonPropertyName("id")] int Id,
       [property: JsonPropertyName("firstName")] string FirstName,
       [property: JsonPropertyName("lastName")] string LastName,
       [property: JsonPropertyName("date")] string Date,
       [property: JsonPropertyName("shiftStartTime")] string ShiftStartTime,
       [property: JsonPropertyName("shiftEndTime")] string ShiftEndTime,
       [property: JsonPropertyName("duration")] string Duration); 
}
