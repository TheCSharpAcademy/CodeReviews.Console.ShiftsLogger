using System;

namespace ShiftsLogger.Ryanw84.Models;

public class Shift
	{
	public int Id { get; set; }
	public string ShiftName { get; set; }
	public DateTimeOffset Date { get; set; }
	public DateTimeOffset StartTime { get; set; }
	public DateTimeOffset EndTime { get; set; }
	public TimeSpan Duration { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }

	// Foreign Key for Worker
	public int WorkerId { get; set; }

	// Navigation Property for Worker
	public virtual Worker Worker { get; set; }
	}
