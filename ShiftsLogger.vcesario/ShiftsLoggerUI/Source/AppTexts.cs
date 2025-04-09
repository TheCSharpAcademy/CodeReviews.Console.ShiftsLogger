public static class AppTexts
{
    public const string FORMAT_DATETIME = "dd/MM/yyyy HH:mm:ss";

    public const string PROMPT_ACTION = "What do you want to do?";
    public const string PROMPT_NEWSHIFT_WORKERID = "Enter your worker ID:";
    public const string PROMPT_NEWSHIFT_STARTDATETIME = "Start date and time of your shift:";
    public const string PROMPT_NEWSHIFT_ENDDATETIME = "End date and time of your shift:";

    public const string TOOLTIP_CANCEL = "Enter '.' anywhere to cancel.";

    public const string LOG_NEWSHIFT_SUCCESS = "Shift logged successfully.";

    public const string OPTION_MAINMENU_NEWSHIFT = "Add new shift";
    public const string OPTION_MAINMENU_MANAGESHIFTS = "Manage shifts";
    public const string OPTION_EXIT = "Exit";
    
    public const string LABEL_APPTITLE = "Shifts Logger";
    public const string LABEL_UNDEFINED = "<undefined>";

    public const string ERROR_BADSTARTDATETIME = "Invalid date and time for starting a shift.";
    public const string ERROR_BADENDDATETIME = "Invalid date and time for ending a shift.";
    public const string ERROR_USERINPUT_DATETIME = $"Couldn't parse DateTime. Use template <{FORMAT_DATETIME}>.";
    public const string ERROR_USERINPUT_NUMBER = "Couldn't parse number.";
}