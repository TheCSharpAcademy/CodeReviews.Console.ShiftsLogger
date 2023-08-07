using System.Text.Json.Serialization;

namespace ShiftsLoggerUI.Models
{
	internal class Shift
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }
		[JsonPropertyName("employeeName")]
		public string EmployeeName { get; set; }
		[JsonPropertyName("startOfShift")]
		public DateTime StartOfShift { get; set; }
		[JsonPropertyName("endOfShift")]
		public DateTime EndOfShift { get; set; }
		[JsonPropertyName("duration")]
		public TimeSpan Duration => EndOfShift - StartOfShift;
	}
}
