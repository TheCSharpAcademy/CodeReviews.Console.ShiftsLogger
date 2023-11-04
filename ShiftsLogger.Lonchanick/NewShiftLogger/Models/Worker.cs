using System.Text.Json.Serialization;

namespace ShiftsLogger.Lonchanick.Models;

public class Worker
{

    public int Id { get; set; }
    public string Name { get; set; }
    
    [JsonIgnore]
    public IEnumerable<Shift> Shifts { get; set; }
}
