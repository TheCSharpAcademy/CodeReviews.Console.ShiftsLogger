namespace Shared;

public class SharedUtils
{
  private static readonly Random _random = new();
  public static DateTime GetRandomTime(DateTime shiftTime)
  {
    int minutesOffset = _random.Next(-60, 61);
    return shiftTime.AddMinutes(minutesOffset);
  }
}
