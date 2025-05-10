using Microsoft.EntityFrameworkCore;
using ShiftsLoggerV2.RyanW84.Data;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using Spectre.Console;
using AutoMapper;

namespace ShiftsLoggerV2.RyanW84.Services;

public class ShiftService(ShiftsDbContext dbContext , IMapper mapper): IShiftService
{
	public async Task<List<Shift>> GetAllShifts()
    {
        return await dbContext.Shifts.ToListAsync();
    }

    public async Task<Shift?> GetShiftById(int id)
    {
        var result = await dbContext.Shifts.FindAsync(id);
        if (result is null)
        {
            AnsiConsole.MarkupLine($"\n[red]Shift with ID: {id} not found.[/]");
            return null;
        }
        AnsiConsole.MarkupLine($"\n[green]Successfully retrieved shift with ID: {id}[/]");
        return result;
    }

    public async Task<Shift> CreateShift(ShiftApiRequestDTO shift)
    {
    Shift newShift = mapper.Map<Shift>(shift); // Map the DTO to the Shift entity
		var savedShift = await dbContext.Shifts.AddAsync(newShift);
        await dbContext.SaveChangesAsync();

        AnsiConsole.MarkupLine(
            $"\n[green]Successfully created shift with ID: {savedShift.Entity.ShiftId}[/]"
        );
        return savedShift.Entity;
    }

    public async Task<Shift?> UpdateShift(int id, ShiftApiRequestDTO updatedShift)
    {
        Shift? savedShift = await dbContext.Shifts.FindAsync(id);

        if (savedShift == null)
        {
            AnsiConsole.MarkupLine($"\n[red]Shift with ID: {id} not found.[/]");
            return null;
        }

		// Map the updated properties from the DTO to the existing entity
		mapper.Map(updatedShift , savedShift);
        savedShift.ShiftId = id; // Ensure the ID is set correctly
								
		dbContext.Shifts.Update(savedShift);

		await dbContext.SaveChangesAsync();

        AnsiConsole.MarkupLine($"\n[green]Successfully updated shift with ID: {id}[/]");

        return savedShift;
    }

    public async Task<string?> DeleteShift(int id)
    {
        string result = "";
        Shift? savedShift = await dbContext.Shifts.FindAsync(id);

        if (savedShift == null)
        {
            AnsiConsole.MarkupLine($"[red]Shift with ID: {id} not found.[/]");
            return result;
        }

        dbContext.Shifts.Remove(savedShift);
        await dbContext.SaveChangesAsync();

        AnsiConsole.MarkupLine($"[green]Successfully deleted shift with ID: {id}[/]");

        return result;
    }
}
