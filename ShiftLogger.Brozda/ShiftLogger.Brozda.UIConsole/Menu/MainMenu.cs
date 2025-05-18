using ShiftLogger.Brozda.UIConsole.Helpers;
using ShiftLogger.Brozda.UIConsole.Interfaces.MenuActionHandlers;
using Spectre.Console;

namespace ShiftLogger.Brozda.UIConsole.Menu
{
    /// <summary>
    /// Defines basic actions and values used through main menu execution
    /// Extends <see cref="BaseMenu"/>
    /// </summary>
    internal class MainMenu : BaseMenu
    {
        /// <summary>
        /// Enum defining menu options and values

        /// </summary>
        private enum MainMenuOptions
        {
            ManageWorkers = 1,
            ManageWorkerShifts,
            ManageShiftTypes,
        };

        /// <summary>
        /// SubMenus which can be accessed from the main menu
        /// </summary>
        private readonly ShiftTypeMenu _shiftTypeMenu;

        private readonly WorkerMenu _workerMenu;
        private readonly AssignShiftsMenu _assignShiftsMenu;

        /// <summary>
        /// Initializes new instance of the main menu class
        /// </summary>
        /// <param name="shiftTypeMenuHandler">Handler responsible for Shift type related actions</param>
        /// <param name="workerMenuHandler">Handler responsible for Worker related actions</param>
        /// <param name="assignShiftsMenuHandler">Handler responsible for Assigned shift related actions</param>
        public MainMenu(
            IShiftTypesActionHandler shiftTypeMenuHandler,
            IWorkerActionHandler workerMenuHandler,
            IAssignedShiftsActionHandler assignShiftsMenuHandler)
        {
            _shiftTypeMenu = new ShiftTypeMenu(shiftTypeMenuHandler);
            _workerMenu = new WorkerMenu(workerMenuHandler);
            _assignShiftsMenu = new AssignShiftsMenu(assignShiftsMenuHandler);

            MapMenu();
        }

        /// <summary>
        /// Maps enum,respective actions and sets up the menu pannel value
        /// </summary>
        private void MapMenu()
        {
            _menuOptions.AddRange(Enum.GetValues<MainMenuOptions>().Cast<int>().ToList());
            _menuOptions.Remove((int)CommonMenuOptions.ReturnToMainMenu);
            _menuOptions.Sort();

            _menuPanel = new Panel(AppConstants.MainMenuTitle);

            _menuItems.Add((int)MainMenuOptions.ManageWorkers, (AppConstants.MainMenuOptionWorkers, ProcessManageWorkers));
            _menuItems.Add((int)MainMenuOptions.ManageWorkerShifts, (AppConstants.MainMenuOptionAssignedShifts, ProcessManageWorkerShifts));
            _menuItems.Add((int)MainMenuOptions.ManageShiftTypes, (AppConstants.MainMenuOptionShiftTypes, ProcessManageShiftTypes));
        }

        /// <summary>
        /// Starts and manages the Worker menu flow.
        /// </summary>
        private async Task ProcessManageWorkers()
        {
            await _workerMenu.ProcessMenu();
        }

        /// <summary>
        /// Starts and manages the Assigned shifts menu flow.
        /// </summary>
        private async Task ProcessManageWorkerShifts()
        {
            await _assignShiftsMenu.ProcessMenu();
        }

        /// <summary>
        /// Starts and manages the Shift types menu flow.
        /// </summary>
        private async Task ProcessManageShiftTypes()
        {
            await _shiftTypeMenu.ProcessMenu();
        }

        /// <summary>
        /// Overrides base class PerformAction methods
        /// Awaits completion of provided action (do not awaits user input as the base class implementation does)
        /// </summary>
        /// <param name="action">Action to be performed</param>
        protected override async Task PerformAction(Func<Task> action)
        {
            await action();
        }
    }
}