using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShiftsLoggerCLI.Models;

public record ShiftRead
(
    [property:JsonPropertyName("id")]
     int Id ,
     
   [property:JsonPropertyName("workerId")]
     int WorkerId,
     
    [property:JsonPropertyName("start")]
    DateTime Start ,
     
   [property:JsonPropertyName("end")]
     DateTime End )
{
    [JsonIgnore]
    public TimeSpan Duration => End.Subtract(Start);

    public override string ToString() => $"Id: {Id}, WorkerId: {WorkerId}, Start: {Start}, End: {End}";

}
public record ShiftUpdate(int Id,DateTime? Start,DateTime? End);

public record ShiftCreate(int WorkerId,DateTime Start,DateTime End);