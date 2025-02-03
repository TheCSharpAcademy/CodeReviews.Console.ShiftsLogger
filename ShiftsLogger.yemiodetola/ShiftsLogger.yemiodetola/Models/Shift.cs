
namespace ShiftsLogger.yemiodetola.Models;
public class Shift
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public DateTime StartedAt { get; set; }
  public DateTime FinishedAt { get; set; }
  public double Duration { get; set; }
}
