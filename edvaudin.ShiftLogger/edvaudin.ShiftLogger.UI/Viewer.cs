namespace ShiftLogger.UI
{
    internal class Viewer
    {
        internal static void DisplayOptionsMenu()
        {
            Console.WriteLine("\nChoose an action from the following list:");
            Console.WriteLine("\tv - View shifts");
            Console.WriteLine("\ta - Add new shift");
            Console.WriteLine("\td - Delete a shift");
            Console.WriteLine("\tu - Update a shift");
            Console.WriteLine("\t0 - Quit this application");
            Console.Write("Your option? ");
        }

        internal static void DisplayTitle()
        {
            Console.WriteLine("+----- Shift Logger -----+");
        }
    }
}