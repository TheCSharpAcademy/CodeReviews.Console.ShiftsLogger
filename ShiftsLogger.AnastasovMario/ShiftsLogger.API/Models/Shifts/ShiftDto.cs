﻿namespace ShiftsLogger.API.Models.Shifts
{
  public class ShiftDto
  {
    public required string Worker { get; set; }

    public required string StartShift { get; set; }

    public required string EndShift { get; set; }

    public TimeSpan Duration => CalculateDuration();


    private TimeSpan CalculateDuration() => (DateTime.Parse(EndShift) - DateTime.Parse(StartShift));
  }
}
