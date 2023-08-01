using System.Globalization;

partial class Program
{
    static string checkDateValidity( string date )
    {
        while (!DateTime.TryParseExact(date, "dd-MM-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            Console.Clear();
            Console.WriteLine("\nInvalid date. Try again:\n");
            date = Console.ReadLine();
        }

        return date;
    }

    static string checkDatesChronology( string startDate, string endDate )
    {
        DateTime start = DateTime.Parse(startDate);
        DateTime end = DateTime.Parse(endDate);

        while (DateTime.Compare(start, end) > 0)
        {
            Console.Clear();
            Console.WriteLine("The end date is earlier than the start date, please insert a valid end date!");
            endDate = Console.ReadLine();
            end = DateTime.Parse(endDate);
        }

        return endDate;
    }

    static (string startDate, string endDate) GetDateInput()
    {
        Console.WriteLine("\nInsert the Start date");
        Console.WriteLine("Please insert the date in the format dd-mm-yyyy. Make sure the ending date is the same or higher than the starting date!");

        string start = checkDateValidity(Console.ReadLine());

        Console.WriteLine("\nInsert the End date");
        Console.WriteLine("Please insert the date in the format dd-mm-yyyy. Make sure the ending date is the same or higher than the starting date!");

        string end = checkDateValidity(Console.ReadLine());

        end = checkDatesChronology(start, end);

        return (start, end);
    }

    static string checkTimeValidity( string time )
    {
        while (!DateTime.TryParseExact(time, "HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            Console.Clear();
            Console.WriteLine("\nInvalid time. Try again:\n");
            time = Console.ReadLine();
        }

        return time;
    }

    static string checkTimeChronology( string startTime, string endTime )
    {
        DateTime start = DateTime.Parse(startTime);
        DateTime end = DateTime.Parse(endTime);

        while (DateTime.Compare(start, end) > 0)
        {
            Console.Clear();
            Console.WriteLine("The end time is earlier than the start time, please insert a valid time!");
            endTime = Console.ReadLine();
            end = DateTime.Parse(endTime);
        }

        return endTime;
    }

    static (string startTime, string endTime) GetTimeInput()
    {
        Console.WriteLine("Insert the start time");
        Console.WriteLine("\nMake sure the ending time is higher than the starting time!");

        string startTime = checkTimeValidity(Console.ReadLine());

        Console.WriteLine("Insert the end time");
        Console.WriteLine("\nMake sure the ending time is higher than the starting time!");

        string endTime = checkTimeValidity(Console.ReadLine());

        endTime = checkTimeChronology(startTime, endTime);

        return (startTime, endTime);
    }
}

