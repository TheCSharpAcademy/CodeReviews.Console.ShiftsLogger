using ShiftLogger.Brozda.UIConsole.Helpers;
using ShiftLogger.Brozda.UIConsole.Interfaces.MenuActionHandlers;
using Spectre.Console;

namespace ShiftLogger.Brozda.UIConsole.Menu
{
    /// <summary>
    /// Defines basic actions and values used for managing shift types
    /// Extends <see cref="BaseMenu"/>
    /// </summary>
    internal class ShiftTypeMenu : BaseMenu
    {
        /// <summary>
        /// Enum defining menu options and values
        /// </summary>
        private enum ShiftTypeMenuOptions
        {
            ViewAll = 1,
            Create,
            Edit,
            Delete,
        };

        private IShiftTypesActionHandler _actionHandler;

        /// <summary>
        /// Initializes new instance of ShiftType menu
        /// </summary>
        /// <param name="actionHandler">Handler responsible for Shift type related actions</param>
        public ShiftTypeMenu(IShiftTypesActionHandler actionHandler)
        {
            _actionHandler = actionHandler;
            MapMenu();
        }

        /// <summary>
        /// Maps enum,respective actions and sets up the menu pannel value
        /// </summary>
        private void MapMenu()
        {
            _menuPanel = new Panel(MenuConstants.ShiftTypesMenuTitle);

            _menuOptions.AddRange(Enum.GetValues<ShiftTypeMenuOptions>().Cast<int>().ToList());
            _menuOptions.Sort();

            _menuItems.Add((int)ShiftTypeMenuOptions.ViewAll, (MenuConstants.ShiftTypesMenuOptionViewAll, _actionHandler.ProcessViewAll));
            _menuItems.Add((int)ShiftTypeMenuOptions.Create, (MenuConstants.ShiftTypesMenuOptionCreate, _actionHandler.ProcessCreate));
            _menuItems.Add((int)ShiftTypeMenuOptions.Edit, (MenuConstants.ShiftTypesMenuOptionEdit, _actionHandler.ProcessEdit));
            _menuItems.Add((int)ShiftTypeMenuOptions.Delete, (MenuConstants.ShiftTypesMenuOptionDelete, _actionHandler.ProcessDelete));
        }
    }
}