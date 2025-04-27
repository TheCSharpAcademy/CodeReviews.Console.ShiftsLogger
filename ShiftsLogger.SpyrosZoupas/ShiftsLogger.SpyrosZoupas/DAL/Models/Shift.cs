namespace ShiftsLogger.SpyrosZoupas.DAL.Model
{
    public class Shift
    {
        public int ShiftId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int DurationSeconds
        {
            get => (EndDateTime - StartDateTime).Seconds;
        }
    }
}