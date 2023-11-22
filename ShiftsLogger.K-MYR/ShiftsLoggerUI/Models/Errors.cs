namespace ShiftsLoggerUI;

public class Errors(List<string> InvalidEmail, List<string> PasswordTooShort, List<string> PasswordRequiresNonAlphanumeric, List<string> PasswordRequiresDigit, List<string> PasswordRequiresUpper, List<string> InvalidUserName)
{
    public string InvalidEmail { get; set; } = InvalidEmail is null ? "" : InvalidEmail![0].TrimEnd('.');
    public string PasswordTooShort { get; set; } = PasswordTooShort is null ? "" : PasswordTooShort![0].TrimEnd('.');
    public string PasswordRequiresNonAlphanumeric { get; set; } = PasswordRequiresNonAlphanumeric is null ? "" : PasswordRequiresNonAlphanumeric[0].TrimEnd('.');
    public string PasswordRequiresDigit { get; set; } = PasswordRequiresDigit is null ? "" : PasswordRequiresDigit[0].TrimEnd('.');
    public string PasswordRequiresUpper { get; set; } = PasswordRequiresUpper is null ? "" : PasswordRequiresUpper[0].TrimEnd('.');
    public string InvalidUserName { get; set; } = InvalidUserName is null ? "" : "Invalid Email Adress: Only Numbers, Letters And Certain Characters [-._@+] Are Allowed";

    public override string ToString()
    {
        var message = "| ";

        foreach (var prop in GetType().GetProperties())
        {
            if (!String.IsNullOrEmpty(prop.GetValue(this)!.ToString()))
                message += $"{prop.GetValue(this)} | ";
        };

        return message;
    }
}
