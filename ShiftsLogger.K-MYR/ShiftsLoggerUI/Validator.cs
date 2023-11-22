using System.Net.Mail;

namespace ShiftsLoggerUI;

internal static class Validator
{
    internal static bool EmailIsValid(string email)
    {
        return MailAddress.TryCreate(email, out _);
    }

    internal static bool PasswordIsValid(string password)
    {
        if (password.Length < 6)
            return false;

        bool hasDigit = false;
        bool hasUpper = false;
        bool hasAlphaNumeric = false;

        for (int i = 0; i < password.Length; i++)
        {
            if (char.IsUpper(password[i]))
                hasUpper = true;
            else if (char.IsDigit(password[i]))
                hasDigit = true;
            else if (!char.IsLetterOrDigit(password[i]))
                hasAlphaNumeric = true;
        }

        return (hasUpper && hasAlphaNumeric && hasDigit);
    }
}
