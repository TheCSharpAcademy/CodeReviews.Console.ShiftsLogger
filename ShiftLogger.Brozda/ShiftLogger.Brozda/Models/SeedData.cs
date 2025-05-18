namespace ShiftLogger.Brozda.API.Models
{
    /// <summary>
    /// Represents SeedData model containing list of ShiftTypes, Workers and  AssignedShifts used for seeding the database during initial create
    /// </summary>
    public class SeedData
    {
        public List<ShiftType> ShiftTypes { get; set; } = null!;
        public List<Worker> Workers { get; set; } = null!;
        public List<AssignedShift> AssignedShifts { get; set; } = null!;
    }
}