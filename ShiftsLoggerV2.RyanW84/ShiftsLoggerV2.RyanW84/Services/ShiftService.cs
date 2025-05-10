using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Shift>> GetAllShifts()
    {
        return await _dbContext.Shifts.ToListAsync();
    }

    public async Task<Shift?> GetShiftById(int id)
    {
        var result = await _dbContext.Shifts.FindAsync(id);
        if (result is null)
        {
            AnsiConsole.MarkupLine($"\n[red]Shift with ID: {id} not found.[/]");
            return null;
        }
        AnsiConsole.MarkupLine($"\n[green]Successfully retrieved shift with ID: {id}[/]");
        return result;
    }

    public async Task<Shift> CreateShift(Shift shift)
    {
        var savedShift = await _dbContext.Shifts.AddAsync(shift);
        await _dbContext.SaveChangesAsync();

        AnsiConsole.MarkupLine(
            $"\n[green]Successfully created shift with ID: {savedShift.Entity.ShiftId}[/]"
        );
        return savedShift.Entity;
    }

    public async Task <Shift?> UpdateShift(int id, Shift updatedShift)
    {
        Shift? savedShift = await _dbContext.Shifts.FindAsync(id);

        if (savedShift == null)
        {
            AnsiConsole.MarkupLine($"\n[red]Shift with ID: {id} not found.[/]");
            return null;
        }

        savedShift.ShiftId = updatedShift.ShiftId;
        savedShift.workerId = updatedShift.workerId;
        savedShift.StartTime = updatedShift.StartTime;
        savedShift.EndTime = updatedShift.EndTime;
        savedShift.LocationId = updatedShift.LocationId;

        await _dbContext.SaveChangesAsync();

        AnsiConsole.MarkupLine($"\n[green]Successfully updated shift with ID: {id}[/]");

        return savedShift;
    }

    public async Task<string> DeleteShift(int id)
    {
        string result = "";
        Shift? savedShift = await _dbContext.Shifts.FindAsync(id);

        if (savedShift == null)
        {
            AnsiConsole.MarkupLine($"[red]Shift with ID: {id} not found.[/]");
            return result;
        }

        _dbContext.Shifts.Remove(savedShift);
        await _dbContext.SaveChangesAsync();

        AnsiConsole.MarkupLine($"[green]Successfully deleted shift with ID: {id}[/]");

        return result;
    }
}
