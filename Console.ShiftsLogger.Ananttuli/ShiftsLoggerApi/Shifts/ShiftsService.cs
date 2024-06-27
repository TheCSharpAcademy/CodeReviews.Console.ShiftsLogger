using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.Shifts.Models;
using ShiftsLoggerApi.Util;

namespace ShiftsLoggerApi.Shifts;

public class ShiftsService
{
    private readonly ShiftsLoggerContext Db;

    public ShiftsService(ShiftsLoggerContext dbContext)
    {
        Db = dbContext;
    }

    public async Task<Result<Shift>> CreateShift(ShiftCreateDto shiftCreateDto)
    {
        if (shiftCreateDto.StartTime > shiftCreateDto.EndTime)
        {
            return Result<Shift>.Fail(
                new Error(
                    ErrorType.BusinessRuleValidation,
                    "Shift StartTime must be before EndTime"
                )
            );
        }

        var overlappingResult = CheckShiftOverlapping(shiftCreateDto.StartTime, shiftCreateDto.EndTime);

        if (overlappingResult.Error != null)
        {
            return Result<Shift>.Fail(
                overlappingResult.Error
            );
        }

        var shiftToCreate = new Shift
        {
            EmployeeId = shiftCreateDto.EmployeeId,
            StartTime = shiftCreateDto.StartTime,
            EndTime = shiftCreateDto.EndTime
        };

        Db.Shifts.Add(shiftToCreate);

        await Db.SaveChangesAsync();

        return await FindById(shiftToCreate.ShiftId);
    }

    public async Task<Result<Shift>> UpdateShift(ShiftUpdateDto shiftUpdateDto)
    {
        var (existingShift, shiftFetchError) = await FindById(shiftUpdateDto.ShiftId);
        if (shiftFetchError != null || existingShift == null)
        {
            return Result<Shift>.Fail(shiftFetchError);
        }

        if (shiftUpdateDto.StartTime > shiftUpdateDto.EndTime)
        {
            return Result<Shift>.Fail(
                new Error(
                    ErrorType.BusinessRuleValidation,
                    "Shift StartTime must be before EndTime"
                )
            );
        }

        var overlappingResult = CheckShiftOverlapping(shiftUpdateDto.StartTime, shiftUpdateDto.EndTime);

        if (overlappingResult.Error != null)
        {
            return Result<Shift>.Fail(
                overlappingResult.Error
            );
        }

        existingShift.StartTime = shiftUpdateDto.StartTime;
        existingShift.EndTime = shiftUpdateDto.EndTime;

        Db.Entry(existingShift).State = EntityState.Modified;

        await Db.SaveChangesAsync();

        return await FindById(existingShift.ShiftId);
    }

    public Result<bool> CheckShiftOverlapping(DateTime start, DateTime end)
    {
        var numOverlappingShifts = Db.Shifts.Count(
            Shift.IsShiftTimeOverlapping(start, end)
        );

        if (numOverlappingShifts > 0)
        {
            return Result<bool>.Fail(
                new Error(
                    ErrorType.BusinessRuleValidation,
                    "Shift times must not overlap with existing shift times"
                )
            );
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<Shift>> FindById(int id)
    {
        var shift = await Db.Shifts.FindAsync(id);

        if (shift == null)
        {
            return Result<Shift>.Fail(new Error(
                ErrorType.DatabaseNotFound,
                "Could not find shift"
            ));
        }

        return Result<Shift>.Success(shift);
    }
}