using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ShiftLogger.ShiftTrack.Helper;

internal class Validation
{
    internal static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("The email address is empty or contains only whitespace.");
            return false;
        }
        if (email.Length > 256)
        {
            Console.WriteLine("The email address is too long.");
            return false;
        }
        if (!Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"))
        {
            Console.WriteLine("The email address format is invalid.");
            return false;
        }
        if (!new EmailAddressAttribute().IsValid(email))
        {
            Console.WriteLine("The email address failed the EmailAddressAttribute validation.");
            return false;
        }
        return true;
    }
}
