﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShiftsLoggerUI.Model
{
    internal class ShiftLog
    {
         [property: JsonPropertyName("id")] public long Id { get; set; }
         [property: JsonPropertyName("employeeId")] public long EmployeeId { get; set; }
         [property: JsonPropertyName("startTime")] public DateTime StartTime { get; set; }
         [property: JsonPropertyName("endTime")] public DateTime EndTime { get; set; }
         [property: JsonPropertyName("duration")] public TimeSpan Duration { get; set; }
         [property: JsonPropertyName("comment")] public string Comment { get; set; } = string.Empty;
    }
}
