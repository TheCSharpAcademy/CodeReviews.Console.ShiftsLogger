using ShiftLogger.Brozda.UIConsole.Helpers;
using ShiftLogger.Brozda.UIConsole.InputOutput;
using ShiftLogger.Brozda.UIConsole.Interfaces.MenuActionHandlers;
using Spectre.Console;

namespace ShiftLogger.Brozda.UIConsole.Menu
{
    /// <summary>
    /// Defines basic actions and values used for managing assigned shifts
    /// Extends <see cref="BaseMenu"/>
    /// </summary>
    internal class AssignShiftsMenu : BaseMenu
    {
        /// <summary>
        /// Defines basic actions and values used for managing assigned shifts
        /// Extends <see cref="BaseMenu"/>
        /// </summary>
        private enum AssignedShiftsMenuOptions
        {
            ViewAllForDate = 1,
            SelectWorker,
            ViewAllForWorker,
            AssignNewShift,
            UpdateShiftForWorker,
            DeleteShiftForWorker
        }

        private IAssignedShiftsActionHandler _actionHandler;

        /// <summary>
        /// Initializes new instance of Worker menu
        /// </summary>
        /// <param name="actionHandler">Handler responsible for assigned shifts related actions</param>
        public AssignShiftsMenu(IAssignedShiftsActionHandler actionHandler)
        {
            _actionHandler = actionHandler;

            MapMenu();
        }

        /// <summary>
        /// Maps enum,respective actions and sets up the menu pannel value
        /// </summary>
        private void MapMenu()
        {
            _menuPanel = new Panel(MenuConstants.ShiftsMenuTitle);

            _menuOptions.AddRange(Enum.GetValues<AssignedShiftsMenuOptions>().Cast<int>().ToList());
            _menuOptions.Sort();

            _menuItems.Add((int)AssignedShiftsMenuOptions.ViewAllForDate, (MenuConstants.ShiftsMenuOptionViewByDate, _actionHandler.ProcessViewAllForDate));
            _menuItems.Add((int)AssignedShiftsMenuOptions.SelectWorker, (MenuConstants.ShiftsMenuOptionSelectWorker, _actionHandler.ProcessSelectWorker));
            _menuItems.Add((int)AssignedShiftsMenuOptions.ViewAllForWorker, (MenuConstants.ShiftsMenuOptionViewAll, _actionHandler.ProcessViewAllForWorker));
            _menuItems.Add((int)AssignedShiftsMenuOptions.AssignNewShift, (MenuConstants.ShiftsMenuOptionCreate, _actionHandler.ProcessAssignNewShift));
            _menuItems.Add((int)AssignedShiftsMenuOptions.UpdateShiftForWorker, (MenuConstants.ShiftsMenuOptionEdit, _actionHandler.ProcessUpdateShiftForWorker));
            _menuItems.Add((int)AssignedShiftsMenuOptions.DeleteShiftForWorker, (MenuConstants.ShiftsMenuOptionDelete, _actionHandler.ProcessDeleteShiftForWorker));
        }

        /// <summary>
        /// Overrides base class ClearConsoleAndPrintPanel method
        /// Clears console, prints menu and panel containing selected worker details
        /// </summary>
        /// <param name="action">Action to be performed</param>
        protected override void ClearConsoleAndPrintPanel()
        {
            AppOutput.ClearConsole();
            AppOutput.PrintMenuPanel(_menuPanel);
            AppOutput.PrintSelectedWorker(_actionHandler.SelectedWorker);
        }
    }
}