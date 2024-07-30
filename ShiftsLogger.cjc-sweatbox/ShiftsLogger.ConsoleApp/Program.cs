using ShiftsLogger.ConsoleApp.Views;

namespace ShiftsLogger.ConsoleApp;

/// <summary>
/// Main insertion point for the console application.
/// Configures the required application settings and launches the main menu view.
/// </summary>
internal class Program
{
    #region Methods

    private static void Main(string[] args)
    {
		try
		{
            MainMenuPage.Show();
		}
		catch (Exception exception)
		{
            MessagePage.Show(exception);
		}
		finally
		{
			Environment.Exit(0);
		}
    }

    #endregion
}
