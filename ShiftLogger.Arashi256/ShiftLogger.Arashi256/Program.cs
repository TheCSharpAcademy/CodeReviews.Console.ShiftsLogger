using ShiftLogger_Frontend.Arashi256.Views;
try
{
    MainView mainView = new MainView();
    mainView.DisplayView();
} catch (Exception e)
{
    Console.WriteLine(e.Message);
}