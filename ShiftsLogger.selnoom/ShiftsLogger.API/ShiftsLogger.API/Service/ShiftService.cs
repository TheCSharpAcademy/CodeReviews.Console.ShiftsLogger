using ShiftsLogger.API.Data;
using ShiftsLogger.API.Model;

namespace ShiftsLogger.API.Service
{
    public class ShiftService
    {
        private readonly ApiDbContext _apiDbContext;

        public ShiftService(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public Shift CreateShift(Shift shift)
        {
            var savedShift = _apiDbContext.Shifts.Add(shift);
            _apiDbContext.SaveChanges();
            return savedShift.Entity;
        }

        public string? DeleteShift(int id)
        {
            Shift? savedShift = _apiDbContext.Shifts.Find(id);

            if (savedShift == null)
            {
                return null;
            }

            _apiDbContext.Shifts.Remove(savedShift);
            _apiDbContext.SaveChanges();

            return $"Successfully deleted shift with id: {id}";
        }

        public List<Shift> GetAllShifts()
        {
            return _apiDbContext.Shifts.ToList();
        }

        public Shift? GetShiftById(int id)
        {
            Shift? savedShift = _apiDbContext.Shifts.Find(id);
            return savedShift == null ? null : savedShift;
        }

        public Shift? UpdateShift(Shift shift)
        {
            Shift? savedShift = _apiDbContext.Shifts.Find(shift.Id);

            if (savedShift == null)
            {
                return null;
            }

            _apiDbContext.Entry(savedShift).CurrentValues.SetValues(shift);
            _apiDbContext.SaveChanges();

            return savedShift;
        }
    }
}
