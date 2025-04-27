using ShiftsLogger.SpyrosZoupas.DAL.Model;

namespace ShiftsLogger.SpyrosZoupas.Services
{
    public interface IShiftService
    {
        public Shift CreateShift();
        public Shift UpdateShift();
        public string DeleteShift();
        public Shift GetShiftById(int id);
        public List<Shift> GetAllShifts();
    }
}