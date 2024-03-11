namespace ShiftLoggerConsole
{
    public class WorkersModel
    {
        public List<Worker> Workers { get; set; }
    }
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public TimeSpan WorkedHours { get; set; }
        public TimeSpan TotalHours { get; set; }

        public override string ToString()
        {
            return $"{Name} - Worked today: {WorkedHours} - Total: {TotalHours}";
        }
    }
}
