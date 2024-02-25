using Buutyful.ShiftsLogger.Api.Data;
using Buutyful.ShiftsLogger.Domain.Contracts.Shift;

namespace Buutyful.ShiftsLogger.Api.EndPoints.Shift;

public static class ShiftEndPoints
{
    public static void MapShiftEndPoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/shifts");

        group.MapPost("", async (ShiftDataAccess data, CreateShiftRequest shiftRequest) => 
        {
            var shift = await data.AddAsync(shiftRequest);
            return Results.Created($"shifts/{shift.ShiftId}", shift);
        });
        group.MapGet("", async (ShiftDataAccess data) => 
        {
            var shifts = await data.GetAsync();
            return Results.Ok(shifts);
        });




    }
}
