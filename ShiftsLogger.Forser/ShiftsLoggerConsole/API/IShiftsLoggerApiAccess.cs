namespace ShiftsLoggerConsole.API
{
    internal interface IShiftsLoggerApiAccess
    {
        public Task<IEnumerable<Shift>> GetShifts();
    }
}