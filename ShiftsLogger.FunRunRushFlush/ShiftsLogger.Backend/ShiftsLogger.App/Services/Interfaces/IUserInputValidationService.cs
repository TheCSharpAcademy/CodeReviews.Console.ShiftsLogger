using ShiftsLogger.Domain;

namespace ShiftsLogger.App.Services.Interfaces
{
    public interface IUserInputValidationService
    {
        Shift ValidateUserInput(Shift? existingEntry = null);
    }
}