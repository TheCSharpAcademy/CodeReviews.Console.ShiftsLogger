using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace ShiftTrackerUI.Models
{
    public class Shift
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("startTime")]
        public string StartTime { get; set; }
        [JsonProperty("endTime")]
        public string EndTime { get; set; }
        [JsonProperty("duration")]
        public string Duration { get; set; }

    }
}
