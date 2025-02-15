using ShiftsLogger.Domain;

namespace ShiftsLogger.Frontend.Services.Interfaces
{
    public interface IUserInputValidationService
    {
        Shift ValidateUserInput(Shift? existingEntry = null);
    }
}