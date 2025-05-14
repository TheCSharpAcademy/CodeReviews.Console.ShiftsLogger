namespace ShiftLogger.Brozda.UIConsole.Helpers
{
    /// <summary>
    /// Static class containing constant values across the app
    /// </summary>
    public static class AppConstants
    {
        public const int CancelledID = 0;

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