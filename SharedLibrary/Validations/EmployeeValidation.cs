using SharedLibrary.DTOs;
using SharedLibrary.Extensions;

namespace SharedLibrary.Validations;

public class EmployeeValidationException : Exception
{
    public EmployeeValidationException(string? message) : base(message)
    {
    }
}

public static class EmployeeValidation
{
    public static void Validate(EmployeeDto employee)
    {
        if (employee.Age < 18)
        {
            throw new EmployeeValidationException("Employee must be at least 18 years of age");
        }

        if (employee.Age > 65)
        {
            throw new EmployeeValidationException("Employee should not be older than 65.");
        }

        if (employee.Name is null)
        {
            throw new EmployeeValidationException("Employee must have a valid name.");
        }
    }
}
