using SharedLibrary.DTOs;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharedLibrary.Validations;

public class EmployeeValidationException(string? message) : Exception(message)
{
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

        if (string.IsNullOrWhiteSpace(employee.Name) || employee.Name.Length < 2)
        {
            throw new EmployeeValidationException("Employee must have a valid name.");
        }

        // Phone number validation:
        // - Allows optional '+' at the start
        // - Allows optional country code (1-3 digits)
        // - Allows optional parentheses around the area code
        // - Allows separators between number groups (space, dash, or dot)
        // - Requires 10-13 digits in total
        if (string.IsNullOrWhiteSpace(employee.PhoneNumber) || !Regex.IsMatch(employee.PhoneNumber, @"^\+?(\d{1,3})?[-.\s]?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4,6}$"))
        {
            throw new EmployeeValidationException("Employee must have a valid Phone Number");
        }

        // Email address validation:
        // - Requires one or more characters that are not @ or whitespace before the @
        // - Requires a single @ symbol
        // - Requires one or more characters that are not @ or whitespace after the @
        // - Requires a dot after the domain name
        // - Requires one or more characters after the dot
        if (string.IsNullOrWhiteSpace(employee.EmailAddress) || !Regex.IsMatch(employee.EmailAddress, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            throw new EmployeeValidationException("Employee must have a valid Email Address");
        }
    }
}
