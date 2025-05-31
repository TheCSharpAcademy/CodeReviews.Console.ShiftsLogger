namespace HKHemanthSharma.ShiftsLogger.Model
{
    public class ShiftDto
    {
        public int WorkerId { get; set; }
        public string ShiftStartTime { get; set; } // Treat as string
        public string ShiftEndTime { get; set; } // Treat as string
        public string ShiftDate { get; set; }
    }
}
