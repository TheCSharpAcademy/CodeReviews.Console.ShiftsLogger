// using System.Net.Mail;
// using System.Text.RegularExpressions;
// using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Spectre.Console;

public class UserInputValidator
{
    // public ValidationResult ValidateUniqueNameOrPeriod(string input)
    // {
    //     if (input.Equals("."))
    //     {
    //         return ValidationResult.Success();
    //     }

    //     using (var dbContext = new PhonebookContext())
    //     {
    //         try
    //         {
    //             var contact = dbContext.Contacts.SingleOrDefault(c => c.Name.Equals(input));
    //             if (contact != null)
    //             {
    //                 return ValidationResult.Error("Name already exists.");
    //             }

    //             return ValidationResult.Success();
    //         }
    //         catch (DbUpdateException ex)
    //         {
    //             Console.WriteLine("Error: " + ex.Message);
    //             Console.ReadLine();
    //         }
    //     }

    //     return ValidationResult.Error();
    // }

    // public ValidationResult ValidateUniqueNameEmptyOrPeriod(string input)
    // {
    //     if (string.IsNullOrEmpty(input))
    //     {
    //         return ValidationResult.Success();
    //     }

    //     return ValidateUniqueNameOrPeriod(input);
    // }

    // public ValidationResult ValidatePhoneNumberOrPeriod(string input)
    // {
    //     if (input.Equals("."))
    //     {
    //         return ValidationResult.Success();
    //     }

    //     string pattern = @"^\([0-9]{3}\) [0-9]{3}-[0-9]{4}$";
    //     if (Regex.IsMatch(input, pattern))
    //     {
    //         return ValidationResult.Success();
    //     }

    //     return ValidationResult.Error("[grey]Use specified format: (XXX) XXX-XXXX[/]");
    // }

    // public ValidationResult ValidatePhoneNumberEmptyOrPeriod(string input)
    // {
    //     if (string.IsNullOrEmpty(input))
    //     {
    //         return ValidationResult.Success();
    //     }

    //     return ValidatePhoneNumberOrPeriod(input);
    // }

    // public ValidationResult ValidateEmailOrPeriod(string input)
    // {
    //     if (input.Equals("."))
    //     {
    //         return ValidationResult.Success();
    //     }

    //     try
    //     {
    //         MailAddress address = new(input);
    //         if (!address.Address.Equals(input))
    //         {
    //             return ValidationResult.Error("[grey]E-mail format not accepted.[/]");
    //         }
    //     }
    //     catch (FormatException ex)
    //     {
    //         return ValidationResult.Error(ex.Message);
    //     }

    //     return ValidationResult.Success();
    // }

    // public ValidationResult ValidateEmailEmptyOrPeriod(string input)
    // {
    //     if (string.IsNullOrEmpty(input))
    //     {
    //         return ValidationResult.Success();
    //     }

    //     return ValidateEmailOrPeriod(input);
    // }

    public ValidationResult ValidateIdOrPeriod(string input)
    {
        if (input.ToLower().Equals("."))
        {
            return ValidationResult.Success();
        }

        if (!uint.TryParse(input, out uint result))
        {
            return ValidationResult.Error(AppTexts.ERROR_USERINPUT_NUMBER);
        }

        return ValidationResult.Success();
    }

    public ValidationResult ValidateDatetimeOrPeriod(string input)
    {
        if (input.Equals("."))
        {
            return ValidationResult.Success();
        }

        if (!DateTime.TryParseExact(input, AppTexts.FORMAT_DATETIME, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        {
            return ValidationResult.Error(AppTexts.ERROR_USERINPUT_DATETIME);
        }

        return ValidationResult.Success();
    }
}