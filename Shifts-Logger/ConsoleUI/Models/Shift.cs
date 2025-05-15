﻿namespace ConsoleUI.Models;

internal class Shift
{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Worker Worker { get; set; } = null!;
    public TimeSpan Duration
    {
        get
        {
            return EndTime - StartTime;
        }
    }
}