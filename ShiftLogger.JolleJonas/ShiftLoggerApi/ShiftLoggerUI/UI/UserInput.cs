using ShiftLoggerUI.Dtos;
using ShiftLoggerUI.Services;

namespace ShiftLoggerUI.UI;

public class UserInput
{
    private readonly ValidationService _validationService;

    public UserInput(ValidationService validationService)
    {
        _validationService = validationService;
    }

    public StartShiftDto GetStartShiftDetails()
    {
        var shift = new StartShiftDto();

        Console.WriteLine("Enter Employee Name");
        shift.EmployeeName = _validationService.ValidateString(Console.ReadLine());

        if (shift.EmployeeName == null)
        {
            Console.WriteLine("Employee Name cannot be empty.");
            return GetStartShiftDetails();
        }

        return shift;
    }
}
