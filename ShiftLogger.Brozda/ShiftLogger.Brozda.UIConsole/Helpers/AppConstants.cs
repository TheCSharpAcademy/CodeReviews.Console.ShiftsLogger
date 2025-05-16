using Microsoft.Extensions.ObjectPool;

namespace ShiftLogger.Brozda.UIConsole.Helpers
{
    /// <summary>
    /// Static class containing constant values across the app
    /// </summary>
    public static class AppConstants
    {
        public const int CancelledID = 0;


        //API HELPER
        public const string NotAvailable = "n/a";
        public const string BadRequest = "Bad request";
        public const string DeserializationFailure = "Unknown deserialization failure";
        public const string ResponseFormatException = "Response format exception";
        public const string ServerError = "Server error";
        public const string UnhandedServerError = "Unhandled Server error";

        //API Result
        public const string ResultNoRecord = "Your request did not match any record in the database";
        public const string ResultInvalidDataInRequest = "Invalid data passed";

        //INPUT
        public const string InputShiftName = "Please enter a shift type name: ";
        public const string InputWorkerName = "Enter worker name: ";

        public const string InputErrorName = "Name can contain only letters separated with space";

        public const string InputDescription = "Please enter description, you can leave it blank to not set it: ";
        public const string InputErrorDescription = "Name can contain only alphanumeric characters along with space, - and _";

        public const string InputDateFormat = "yyyy-mm-dd";
        public const string InputDate = $"Enter a date in format {InputDateFormat}: ";
        public const string InputInvalidDate = "Invalid date format";

        public const string InputRecordId = "Enter a ID of record you wish to select, alternatively you can enter 0 to return: ";
        public const string InputSelectRecordId = "Enter a ID of record you wish to select: ";
        public const string InputErrorRecordId = "Input must be valid Id or 0";

        public const string InputTimeHoursStart = "Enter starting hour: ";
        public const string InputTimeHoursEnd = "Enter ending hour: ";
        public const string InputTimeMinutesStart = "Enter starting minute: ";
        public const string InputTimeMinutesEnd = "Enter ending minute: ";
        public const string InputErrorTimeHour = "Hour value must be number between 0 and 23: ";
        public const string InputErrorTimeMinute = "Hour value must be number between 0 and 59: ";

        //OUTPUT
        public const string OutputUnsuportedData = "Unsuported data";
        public const string OutputPanelShiftTypes = "Shift type details";
        public const string OutputPanelWorker = "Worker details";
        public const string OutputPanelAssignedShifts = "Assigned shift(s) details"";

        public const string OutputPanelSelectedWorkerText = "Currently selected worker: ";
        public const string OutputPanelNoSelectedWorker = "No worker";

        public const string OutputDateFormat = "dd-MM-yyyy";
        public const string OutputNullValueSymbol = " - ";

        public const string OutputPressAnyKeyToContinue = "Press any key to continue...";

        //GENERAL
        public const string PLACEHOLDER = "This text wasnt properly set by developer";

        public const string INVALID_MENU_OPTION = "Invalid menu option";

        public const string MENU_NAVIGATION = "Use arrow keys to select an option";
        public const string MENU_UNKNOWN_OPTION = "Unknown option";

        public const string SUCCESS_CREATE = "Record created successfully";
        public const string SUCCESS_EDIT = "Record updated successfully";
        public const string SUCCESS_DELETE = "Record deleted successfully";

        public const string ERROR_FETCHING_ALL = "Error occured during fetching records";
        public const string ERROR_NOTFOUND = "Record not found";
        public const string ERROR_CREATE = "Error occured during create";
        public const string ERROR_EDIT = "Error occured during update";
        public const string ERROR_DELETE = "Error occured during delete";
        public const string ERROR_UNHANDLED = "Unexpected error";

        //BASE MENU
        public const string BaseMenuReturnToMainMenu = "Return to main menu";
        public const string BaseMenuExitApp = "Exit the application";

        //MAIN MENU
        public const string MAIN_MENU_TEXT = $"Welcome to shift logger app\n" +
                                            $"You can manage workers, assigned shifts and shifts that can be assigned in menu below";

        public const string MAIN_MENU_WORKERS = "Manage Workers";
        public const string MAIN_MENU_WORKERSHIFTS = "Manage Worker shifts";
        public const string MAIN_MENU_SHIFTTYPES = "Manage Shift types";

        //SHIFT TYPE MENU
        public const string SHIFTTYPES_MENU_TEXT = "This sub menu allows you to manage shift types details";

        public const string SHIFTTYPES_MENU_VIEWALL = "View all shift types";
        public const string SHIFTTYPES_MENU_CREATE = "Create new shift type";
        public const string SHIFTTYPES_MENU_EDIT = "Edit existing shift type";
        public const string SHIFTTYPES_MENU_DELETE = "Delete existing shift type";

        //WORKER MENU
        public const string WORKERS_MENU_TEXT = "This sub menu allows you to manage worker details";

        public const string WORKERS_VIEWALL = "View all workers";
        public const string WORKERS_VIEWBYID = "View specific worker details";
        public const string WORKERS_CREATE = "Create new worker in the database";
        public const string WORKERS_EDIT = "Update worker details";
        public const string WORKERS_DELETE = "Delete worker from database";

        //ASSIGNED SHIFTS MENU
        public const string SHIFTS_MENU_UNSELECTEDWORKER = "You need to select worker first";

        public const string SHIFTS_MENU_TEXT = "This sub menu allows you to manage worker shifts details";

        public const string SHIFTS_VIEWALLFORWORKER = "View all shifts for worker";
        public const string SHIFTS_VIEWALLFORDATE = "View all workers on shift for specific date";
        public const string SHIFTS_SELECT = "Select worker for operations";
        public const string SHIFTS_ASSIGNNEWSHIFT = "Assign new shift";
        public const string SHIFTS_EDIT = "Update existing assigned shift";
        public const string SHIFTS_DELETE = "Unnasign shift for worker";
    }
}