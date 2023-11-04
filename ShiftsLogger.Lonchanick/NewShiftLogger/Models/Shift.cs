using System.Text.Json.Serialization;

namespace ShiftsLogger.Lonchanick.Models;

public class Shift
{
    public int Id { get; set; }
    public DateTime Check { get; set; }
    public CheckType CheckTypeField {get; set;} 
    public int WorkerId { get; set; }

    [JsonIgnore]
    public virtual Worker Worker { get; set; }
}

public enum CheckType{
    CheckIn,
    CheckOut
}
