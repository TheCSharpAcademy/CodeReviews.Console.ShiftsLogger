using ShiftsLoggerV2.RyanW84.Data;
using ShiftsLoggerV2.RyanW84.Models;
using Spectre.Console;

namespace ShiftsLoggerV2.RyanW84.Services;

public class ShiftService : IShiftService
{
    private readonly ShiftsDbContext _dbContext;

    public ShiftService(ShiftsDbContext dbContext)
    {
        _dbContext = dbContext; // Injecting the context into the service so we can talk to the Database
    }

    public List<Shift> GetAllShifts()
    {
        return _dbContext.Shifts.ToList();
    }

    public Shift? GetShiftById(int id)
    {
        return _dbContext.Shifts.Find(id);
    }

    public Shift CreateShift(Shift shift)
    {
        var savedShift = _dbContext.Shifts.Add(shift);
        _dbContext.SaveChanges();

        AnsiConsole.MarkupLine(
            $"[green]Successfully created shift with ID: {savedShift.Entity.ShiftId}[/]"
        );
        return savedShift.Entity;
    }

    public Shift? UpdateShift(int id, Shift updatedShift)
    {
        Shift? savedShift = _dbContext.Shifts.Find(id);

        if (savedShift == null)
        {
            AnsiConsole.MarkupLine($"[red]Shift with ID: {id} not found.[/]");
            return null;
        }

        savedShift.ShiftId = updatedShift.ShiftId;
        savedShift.workerId = updatedShift.workerId;
        savedShift.StartTime = updatedShift.StartTime;
        savedShift.EndTime = updatedShift.EndTime;
        savedShift.LocationId = updatedShift.LocationId;

        _dbContext.SaveChanges();

        AnsiConsole.MarkupLine($"[green]Successfully updated shift with ID: {id}[/]");
        return savedShift;
    }

    public string DeleteShift(int id)
    {
        string result = "";
        Shift? savedShift = _dbContext.Shifts.Find(id);

        if (savedShift == null)
        {
            AnsiConsole.MarkupLine($"[red]Shift with ID: {id} not found.[/]");
            return result;
        }

        _dbContext.Shifts.Remove(savedShift);
        _dbContext.SaveChanges();

        AnsiConsole.MarkupLine($"[green]Successfully deleted shift with ID: {id}[/]");

        return result;
    }
}
