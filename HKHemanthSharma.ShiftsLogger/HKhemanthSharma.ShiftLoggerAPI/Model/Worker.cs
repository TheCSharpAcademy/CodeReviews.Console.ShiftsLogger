namespace HKhemanthSharma.ShiftLoggerAPI.Model
{
    public class Worker
    {
        public int WorkerId { get; set; }
        public string Name { get; set; }
        public List<Shift> Shifts { get; set; }
    }
}
