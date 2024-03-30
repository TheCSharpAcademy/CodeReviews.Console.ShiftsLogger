using ShiftsLoggerLibrary.DTOs;
using ShiftsLoggerLibrary.Models;

namespace ShiftsLoggerAPI;

public class Validate
{
    public static async ValueTask<object?> EndShiftIsValid(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        EndShift shift = context.GetArgument<EndShift>(0);
        Dictionary<string, string[]> errors = [];

        if (shift.Id < 0)
        {
            errors.Add(nameof(EndShift.Id), ["Shift Id should be a positive number."]);
        }

        if (shift.EndTime == DateTime.MinValue)
        {
            errors.Add(nameof(EndShift.EndTime), ["Shift end time should not be default."]);
        }

        if (errors.Count > 0)
        {
            return Results.ValidationProblem(errors);
        }

        return await next(context);
    }

    public static async ValueTask<object?> StartShiftIsValid(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        StartShift shift = context.GetArgument<StartShift>(0);
        Dictionary<string, string[]> errors = [];

        if (shift.EmployeeId < 0)
        {
            errors.Add(nameof(StartShift.EmployeeId), ["Employee Id should be a positive number."]);
        }

        if (shift.StartTime == DateTime.MinValue)
        {
            errors.Add(nameof(StartShift.StartTime), ["Shift start time should not be default."]);
        }

        if (errors.Count > 0)
        {
            return Results.ValidationProblem(errors);
        }

        return await next(context);
    }

    public static async ValueTask<object?> UpdatedShiftIsValid(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        Shift shift = context.GetArgument<Shift>(0);
        Dictionary<string, string[]> errors = [];

        if (shift.EmployeeId < 0)
        {
            errors.Add(nameof(Shift.EmployeeId), ["Employee Id should be a positive number."]);
        }

        if (shift.StartTime == DateTime.MinValue || shift.EndTime == DateTime.MinValue)
        {
            errors.Add(nameof(Shift.StartTime), ["Shift start time should not be default."]);
        }

        if (shift.EndTime == DateTime.MinValue)
        {
            errors.Add(nameof(Shift.StartTime), ["Shift end time should not be default."]);
        }

        if (shift.EndTime < shift.StartTime)
        {
            errors.Add(nameof(Shift.StartTime), ["Shift end time should after shift start time."]);
        }

        if (errors.Count > 0)
        {
            return Results.ValidationProblem(errors);
        }

        return await next(context);
    }
}