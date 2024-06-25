using SharedLibrary.Extensions;
using SharedLibrary.Models;
using System.Net.Http.Headers;

namespace SharedLibrary.Validations;

public class EmployeeValidationException : Exception
{
    public EmployeeValidationException(string? message) : base(message)
    {
    }
}

public static class EmployeeValidation
{
    public static void Validate(Employee employee)
    {
        var age = employee.DateOfBirth.CalculateAge();
        if (age < 18)
        {
            throw new EmployeeValidationException("Employee must be at least 18 years of age");
        }

        if (age > 65)
        {
            throw new EmployeeValidationException("Employee should not be older than 65.");
        }

        if (employee.Name is null)
        {
            throw new EmployeeValidationException("Employee must have a valid name.");
        }
    }
}
