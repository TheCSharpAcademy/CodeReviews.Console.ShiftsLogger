using ShiftLogger.Brozda.UIConsole.Helpers;
using ShiftLogger.Brozda.UIConsole.InputOutput;
using Spectre.Console;

namespace ShiftLogger.Brozda.UIConsole.Menu
{
    /// <summary>
    /// Defines base operation for menu classes
    /// Cannot be directly initialied
    /// </summary>
    internal abstract class BaseMenu
    {
        /// <summary>
        /// Enum defining menu options and values
        /// </summary>
        protected enum CommonMenuOptions
        {
            ReturnToMainMenu = 100,
            ExitApp = 101,
        }

        /// <summary>
        /// Represents menu panel - short descriptions of current menu capabilities
        /// </summary>
        protected Panel _menuPanel = new Panel(SharedConstants.PlaceHolderText);

        /// <summary>
        /// List of  integer values of menu options enumerable
        /// </summary>
        protected List<int> _menuOptions = Enum.GetValues<CommonMenuOptions>().Cast<int>().ToList();

        /// <summary>
        /// A Map of menu options to label - text defining the option and <see cref="Func<<see cref="Task"/>>"/> defining an action to be performed for each option
        /// </summary>
        protected Dictionary<int, (string label, Func<Task> action)> _menuItems = new()
        {
            {(int)CommonMenuOptions.ReturnToMainMenu, (MenuConstants.BaseMenuReturnToMainMenu, () => Task.CompletedTask) },
            {(int)CommonMenuOptions.ExitApp, (MenuConstants.BaseMenuExitApp, ()=> {Environment.Exit(0); return Task.CompletedTask;})}
        };

        /// <summary>
        /// An enterpoint for the menu class
        /// Process the user choice until user decides to return to main menu/ exit the app
        /// </summary>
        public async Task ProcessMenu()
        {
            ClearConsoleAndPrintPanel();

            var choice = await AppInput.ShowMenuAndGetInput(_menuOptions, _menuItems);

            while (choice != (int)CommonMenuOptions.ReturnToMainMenu)
            {
                if (_menuItems.TryGetValue(choice, out var kvp))
                {
                    await PerformAction(kvp.action);
                }
                else
                {
                    AppOutput.PrintText(SharedConstants.MenuInvalidOption);
                }

                ClearConsoleAndPrintPanel();
                choice = await AppInput.ShowMenuAndGetInput(_menuOptions, _menuItems);
            }
        }

        /// <summary>
        /// Clears console and print current menu panel to the output
        /// </summary>
        protected virtual void ClearConsoleAndPrintPanel()
        {
            AppOutput.ClearConsole();
            AppOutput.PrintMenuPanel(_menuPanel);
        }

        /// <summary>
        /// Performs action for current menu choice and pauses the execution until user press any key
        /// </summary>
        /// <param name="action">Action to be performed</param>
        protected virtual async Task PerformAction(Func<Task> action)
        {
            await action();
            AppOutput.PrintPressAnyKeyToContinue();
        }
    }
}