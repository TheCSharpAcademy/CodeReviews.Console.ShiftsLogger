using ShiftsLoggerLibrary.Models;
using ShiftsLoggerLibrary.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ShiftsLoggerAPI;

public class ShiftController
{
    public static async Task<Results<Ok<Shift>, NotFound>> GetShiftByIdAsync(int id, IShiftService service)
    {
        Shift? targetShift = await service.GetShiftByIdAsync(id);

        return targetShift is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(targetShift);
    }

    public static async Task<Results<Ok<List<Shift>>, NotFound>> GetShiftsAsync(IShiftService service)
    {
        List<Shift>? shfits = await service.GetShiftsAsync();

        if (shfits == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(shfits);
    }

    public static async Task<Results<Ok<List<Shift>>, NotFound>> GetShiftsByEmployeeIdAsync(int id, IShiftService service)
    {
        List<Shift>? shfits = await service.GetShiftsByEmployeeIdAsync(id);

        if (shfits == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(shfits);
    }

    public static async Task<Results<Ok<Shift>, NotFound>> GetRunningShiftsByEmployeeIdAsync(int id, IShiftService service)
    {
        Shift? runningShift = await service.GetRunningShiftsByEmployeeIdAsync(id);

        if (runningShift == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(runningShift);
    }

    public static async Task<Results<Created<Shift>, BadRequest<string>>> StartShiftAsync(StartShift startShift, IShiftService service)
    {
        Shift? shift = await service.StartShiftAsync(startShift);

        if (shift == null)
        {
            return TypedResults.BadRequest("Employee already has a shift running.");
        }

        return TypedResults.Created("/shifts/{id}", shift);
    }

    public static async Task<Results<Created<Shift>, NotFound>> EndShiftAsync(EndShift endShift, IShiftService service)
    {
        Shift? shift = await service.EndShiftAsync(endShift);

        if (shift == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Created("/shifts/{id}", shift);
    }

    public static async Task<Results<Created<Shift>, NotFound>> UpdateShiftAsync(Shift updatedShift, IShiftService service)
    {
        Shift? shift = await service.UpdateShiftAsync(updatedShift);

        if (shift == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Created("/shifts/{id}", shift);
    }

    public static async Task<Results<NoContent, NotFound>> DeleteShiftAsync(int id, IShiftService service)
    {
        if (!await service.DeleteShiftByIdAsync(id))
        {
            return TypedResults.NotFound();
        }

        return TypedResults.NoContent();
    }
}