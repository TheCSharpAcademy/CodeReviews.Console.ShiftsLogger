using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.Employees.Models;
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
        var (_, validationError) = ValidateShiftTimes(
            shiftCreateDto.StartTime,
            shiftCreateDto.EndTime
        );

        if (validationError != null)
        {
            return Result<Shift>.Fail(validationError);
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

    public async Task<Result<List<ShiftDto>>> GetShifts()
    {
        List<ShiftDto>? shifts = await GetShiftsQuery().ToListAsync();

        if (shifts != null)
        {
            return Result<List<ShiftDto>>.Success(shifts);
        }

        return Result<List<ShiftDto>>.Fail(
            new Error(ErrorType.DatabaseNotFound, "Could not get shifts")
        );
    }

    public async Task<Result<ShiftDto>> GetShift(int id)
    {
        var shiftsQuery = GetShiftsQuery(id);

        var employee = await shiftsQuery
            .FirstOrDefaultAsync();

        if (employee == null)
        {
            return Result<ShiftDto>.Fail(
                new Error(
                    ErrorType.DatabaseNotFound,
                    "Employee not found"
                )
            );
        }

        return Result<ShiftDto>.Success(employee);
    }


    public async Task<Result<Shift>> UpdateShift(int id, ShiftUpdateDto shiftUpdateDto)
    {
        if (id != shiftUpdateDto.ShiftId)
        {
            return Result<Shift>.Fail(
                new Error(
                    ErrorType.BusinessRuleValidation,
                    "Param ID does not match payload ID"
                )
            );
        }

        var (existingShift, shiftFetchError) = await FindById(shiftUpdateDto.ShiftId);

        if (shiftFetchError != null || existingShift == null)
        {
            return Result<Shift>.Fail(shiftFetchError);
        }

        var (_, validationError) = ValidateShiftTimes(
            shiftUpdateDto.StartTime,
            shiftUpdateDto.EndTime
        );

        if (validationError != null)
        {
            return Result<Shift>.Fail(validationError);
        }

        existingShift.StartTime = shiftUpdateDto.StartTime;
        existingShift.EndTime = shiftUpdateDto.EndTime;

        Db.Entry(existingShift).State = EntityState.Modified;

        await Db.SaveChangesAsync();

        return await FindById(existingShift.ShiftId);
    }

    public async Task<Result<int?>> DeleteShift(int id)
    {
        var (shift, error) = await FindById(id);

        if (error != null || shift == null)
        {
            return Result<int?>.Fail(error);
        }

        Db.Shifts.Remove(shift);
        await Db.SaveChangesAsync();

        return Result<int?>.Success(id);
    }

    private IQueryable<ShiftDto> GetShiftsQuery(int? shiftId = null)
    {
        return Db.Shifts
            .Where(s => shiftId != null ? s.ShiftId == shiftId : true)
            .Where(s => s.Employee != null)
            .Select(s =>
                new ShiftDto(
                    s.ShiftId,
                    s.StartTime,
                    s.EndTime,
                    s.Employee == null ? null :
                        new EmployeeDto(
                            s.Employee!.EmployeeId,
                            s.Employee.Name,
                            null
                        )
                )
        );
    }

    public Result<bool> ValidateShiftTimes(DateTime start, DateTime end)
    {
        if (start > end)
        {
            return Result<bool>.Fail(
                new Error(
                    ErrorType.BusinessRuleValidation,
                    "Shift StartTime must be before EndTime"
                )
            );
        }

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

    private async Task<Result<Shift>> FindById(int id)
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