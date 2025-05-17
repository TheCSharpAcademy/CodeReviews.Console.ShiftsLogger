using Newtonsoft.Json;
namespace ShiftUI.UIModels
{
    class UIShift
    {
        public int id { get; set; }

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }

        public int userId { get; set; }

        [JsonProperty("user")]
        public UIUser? user { get; set; }
    }

    class UIUser
    {
        public int userId { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public bool isActive { get; set; } = true;
        public List<UIShift>? Shifts { get; set; }
    }
}
