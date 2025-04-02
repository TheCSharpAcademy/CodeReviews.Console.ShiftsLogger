namespace ShiftsLoggerAPI.Interfaces;

public interface ISeedRepository
{
    Task SeedEmployeesAsync();
    Task SeedShiftsAsync(int randRowNumber);
}
