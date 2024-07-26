namespace ShiftsLoggerUI.Utilities
{
    internal class Validation
    { 
        public bool ValidateRange(DateTime firstRange, DateTime secondRange)
        {
            return firstRange > secondRange;
        }
    }
}

