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

    public async Task<Result<ShiftDto>> CreateShift(ShiftCreateDto shiftCreateDto)
    {
        var (_, validationError) = ValidateShiftTimes(
            shiftCreateDto.StartTime,
            shiftCreateDto.EndTime,
            shiftCreateDto.EmployeeId,
            null
        );

        if (validationError != null)
        {
            return Result<ShiftDto>.Fail(validationError);
        }

        var shiftToCreate = new Shift
        {
            EmployeeId = shiftCreateDto.EmployeeId,
            StartTime = shiftCreateDto.StartTime,
            EndTime = shiftCreateDto.EndTime
        };

        Db.Shifts.Add(shiftToCreate);

        await Db.SaveChangesAsync();

        return await FindDtoById(shiftToCreate.ShiftId);
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


    public async Task<Result<ShiftDto>> UpdateShift(int id, ShiftUpdateDto shiftUpdateDto)
    {
        if (id != shiftUpdateDto.ShiftId)
        {
            return Result<ShiftDto>.Fail(
                new Error(
                    ErrorType.BusinessRuleValidation,
                    "Param ID does not match payload ID"
                )
            );
        }

        var (existingShift, shiftFetchError) = await FindById(shiftUpdateDto.ShiftId);

        if (shiftFetchError != null || existingShift == null)
        {
            return Result<ShiftDto>.Fail(shiftFetchError);
        }

        var (_, validationError) = ValidateShiftTimes(
            shiftUpdateDto.StartTime,
            shiftUpdateDto.EndTime,
            existingShift.EmployeeId,
            existingShift.ShiftId
        );

        if (validationError != null)
        {
            return Result<ShiftDto>.Fail(validationError);
        }

        existingShift.StartTime = shiftUpdateDto.StartTime;
        existingShift.EndTime = shiftUpdateDto.EndTime;

        Db.Entry(existingShift).State = EntityState.Modified;

        await Db.SaveChangesAsync();

        return await FindDtoById(id);
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
            .Include(s => s.Employee)
            .Where(s => s.Employee != null)
            .Select(s =>
                new ShiftDto(
                    s.ShiftId,
                    s.StartTime,
                    s.EndTime,
                    new EmployeeCoreDto(
                        s.Employee!.EmployeeId,
                        s.Employee.Name
                    )
                )
        );
    }

    public Result<bool> ValidateShiftTimes(DateTime start, DateTime end, int employeeId, int? ignoreShiftId)
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

        var numOverlappingShifts = Db.Shifts
            .Where(s => s.EmployeeId == employeeId)
            .Where(s =>
                ignoreShiftId == null || s.ShiftId != ignoreShiftId)
            .Count(
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
        var shift = await Db.Shifts
            .Where(s => s.ShiftId == id)
            .Include(s => s.Employee)
            .FirstOrDefaultAsync();

        if (shift == null)
        {
            return Result<Shift>.Fail(new Error(
                ErrorType.DatabaseNotFound,
                "Could not find shift"
            ));
        }

        return Result<Shift>.Success(shift);
    }

    private async Task<Result<ShiftDto>> FindDtoById(int id)
    {
        var (shift, error) = await FindById(id);

        if (shift == null)
        {
            return Result<ShiftDto>.Fail(error);
        }

        return Result<ShiftDto>.Success(ShiftMapping.ToDto(shift));
    }
}