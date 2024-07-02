using SharedLibrary.Models;

namespace SharedLibrary.Validations;

public class ShiftValidationException(string? message) : Exception(message)
{
}

public static class ShiftValidation
{
    public static void Validate(Shift shift)
    {
        var shiftLength = shift.EndTime - shift.StartTime;
        if (shiftLength .TotalHours <= 1)
        {
            throw new ShiftValidationException("Can only add shifts longer than 1 hour.");
        }

        if (shiftLength.TotalHours > 16)
        {
            throw new ShiftValidationException("Shifts cannot last longer than 16 hours.");
        }
    }
}
