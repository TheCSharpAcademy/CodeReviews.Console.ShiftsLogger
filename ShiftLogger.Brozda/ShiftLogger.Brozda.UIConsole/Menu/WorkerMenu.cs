using ShiftLogger.Brozda.UIConsole.Helpers;
using ShiftLogger.Brozda.UIConsole.Interfaces.MenuActionHandlers;
using Spectre.Console;

namespace ShiftLogger.Brozda.UIConsole.Menu
{
    /// <summary>
    /// Defines basic actions and values used for managing workers
    /// Extends <see cref="BaseMenu"/>
    /// </summary>
    internal class WorkerMenu : BaseMenu
    {
        /// <summary>
        /// Enum defining menu options and values
        /// </summary>
        private enum WorkerMenuOptions
        {
            ViewAll = 1,
            ViewById,
            Create,
            Edit,
            Delete,
        };

        private IWorkerActionHandler _actionHandler;

        /// <summary>
        /// Initializes new instance of Worker menu
        /// </summary>
        /// <param name="actionHandler">Handler responsible for worker related actions</param>
        public WorkerMenu(IWorkerActionHandler actionHandler)
        {
            _actionHandler = actionHandler;
            MapMenu();
        }

        /// <summary>
        /// Maps enum,respective actions and sets up the menu pannel value
        /// </summary>
        private void MapMenu()
        {
            _menuPanel = new Panel(AppConstants.WORKERS_MENU_TEXT);

            _menuOptions.AddRange(Enum.GetValues<WorkerMenuOptions>().Cast<int>().ToList());
            _menuOptions.Sort();

            _menuItems.Add((int)WorkerMenuOptions.ViewAll, (AppConstants.WORKERS_VIEWALL, _actionHandler.ProcessViewAll));
            _menuItems.Add((int)WorkerMenuOptions.ViewById, (AppConstants.WORKERS_VIEWBYID, _actionHandler.ProcessViewById));
            _menuItems.Add((int)WorkerMenuOptions.Create, (AppConstants.WORKERS_CREATE, _actionHandler.ProcessCreate));
            _menuItems.Add((int)WorkerMenuOptions.Edit, (AppConstants.WORKERS_EDIT, _actionHandler.ProcessEdit));
            _menuItems.Add((int)WorkerMenuOptions.Delete, (AppConstants.WORKERS_DELETE, _actionHandler.ProcessDelete));
        }
    }
}