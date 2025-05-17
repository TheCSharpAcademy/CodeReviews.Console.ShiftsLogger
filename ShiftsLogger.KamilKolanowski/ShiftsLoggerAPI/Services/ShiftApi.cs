using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.KamilKolanowski.Models;

namespace ShiftsLoggerAPI.Services;

internal class ShiftApi
{
    private readonly ShiftDbService _shiftDbService;

    public ShiftApi(ShiftDbService shiftDbService)
    {
        _shiftDbService = shiftDbService;
    }

    internal void Configure(WebApplication app)
    {
        app.MapPost("/shifts", AddShift);
        app.MapDelete("/shifts/{id}", DeleteShift);
        app.MapPut("/shifts", EditShift);
        app.MapGet("/shifts/{id}", ReadShift);
        app.MapGet("/shifts", ReadAllShifts);
    }

    private async Task<IResult> AddShift([FromBody] Shift shift)
    {
        await _shiftDbService.AddShiftAsync(shift);
        return TypedResults.Created($"/shifts/{shift.ShiftId}", shift);
    }

    private async Task<IResult> DeleteShift(int id)
    {
        var shift = await _shiftDbService.ReadShiftAsync(id);
        if (shift == null)
            return TypedResults.NotFound();

        await _shiftDbService.DeleteShiftAsync(id);
        return TypedResults.NoContent();
    }

    private async Task<IResult> EditShift([FromBody] Shift updatedShift)
    {
        var existingShift = await _shiftDbService.ReadShiftAsync(updatedShift.ShiftId);
        if (existingShift == null)
            return TypedResults.NotFound();

        existingShift.ShiftType = updatedShift.ShiftType;
        existingShift.WorkerId = updatedShift.WorkerId;
        existingShift.WorkedHours = updatedShift.WorkedHours;
        existingShift.StartTime = updatedShift.StartTime;
        existingShift.EndTime = updatedShift.EndTime;

        await _shiftDbService.UpdateShiftAsync(existingShift);
        return TypedResults.NoContent();
    }

    private async Task<IResult> ReadShift(int id)
    {
        var shift = await _shiftDbService.ReadShiftAsync(id);
        return shift is null ? TypedResults.NotFound() : TypedResults.Ok(shift);
    }

    private async Task<IResult> ReadAllShifts()
    {
        var shift = await _shiftDbService.ReadAllShiftsAsync();
        return TypedResults.Ok(shift);
    }
}
