public static class AppTexts
{
    public const string FORMAT_DATETIME = "dd/MM/yyyy HH:mm:ss";

    public const string PROMPT_ACTION = "What do you want to do?";
    public const string PROMPT_RECONFIRM = "Are you REALLY sure?";
    public const string PROMPT_NEWSHIFT_WORKERID = "Enter your worker ID:";
    public const string PROMPT_NEWSHIFT_STARTTIME = "Start date and time of your shift:";
    public const string PROMPT_NEWSHIFT_ENDTIME = "End date and time of your shift:";
    public const string PROMPT_EDITSHIFT_ID = "Enter ID to edit:";
    public const string PROMPT_EDITSHIFT_WORKERID = "Enter new Worker ID:";
    public const string PROMPT_EDITSHIFT_STARTTIME = "Enter new start time:";
    public const string PROMPT_EDITSHIFT_ENDTIME = "Enter new end time:";
    public const string PROMPT_REMOVESHIFT_ID = "Enter ID to remove:";
    public const string PROMPT_REMOVESHIFT_CONFIRM = "Are you sure you want to delete this entry?";

    public const string TOOLTIP_RETURN = "Press any key to return.";
    public const string TOOLTIP_CANCEL = "Enter '.' anywhere to cancel.";

    public const string LOG_NEWSHIFT_SUCCESS = "Shift logged successfully.";
    public const string LOG_VIEWSHIFT_NONEFOUND = "No shifts were found in the database.";
    public const string LOG_EDITSHIFT_SUCCESS = "Shift updated successfully.";
    public const string LOG_REMOVESHIFT_SUCCESS = "Shift removed successfully.";
    public const string LOG_IDNOTFOUND = "No shift found with this ID.";

    public const string OPTION_ADDSHIFT = "Add new shift";
    public const string OPTION_VIEWALL = "View all shifts";
    public const string OPTION_EDITSHIFT = "Edit shift";
    public const string OPTION_REMOVESHIFT = "Remove shift";
    public const string OPTION_EXIT = "Exit";
    
    public const string LABEL_APPTITLE = "Shifts Logger";
    public const string LABEL_UNDEFINED = "<undefined>";

    public const string LABEL_SHIFTTABLE_ID = "ID";
    public const string LABEL_SHIFTTABLE_WORKERID = "Worker ID";
    public const string LABEL_SHIFTTABLE_STARTTIME = "Start time";
    public const string LABEL_SHIFTTABLE_ENDTIME = "End time";

    public const string ERROR_BADSTARTDATETIME = "Invalid date and time for starting a shift.";
    public const string ERROR_BADENDDATETIME = "Invalid date and time for ending a shift.";
    public const string ERROR_USERINPUT_DATETIME = $"Couldn't parse DateTime. Use template <{FORMAT_DATETIME}>.";
    public const string ERROR_USERINPUT_NUMBER = "Couldn't parse number.";
    public const string ERROR_UNKNOWNOPTION = "Implement option: ";
}