using SharedLibrary.Models;
using SharedLibrary.Validations;
using ShiftsLoggerAPI.Interfaces;

namespace ShiftsLoggerAPI.Services
{
    public class ShiftService : IShiftService
    {
        private IShiftRepository _repository;
        private IEmployeeService _employeeService;

        public ShiftService(IShiftRepository repository)
        {
            _repository = repository;
        }

        public Shift CreateShift(Shift shift)
        {
            ValidateShift(shift);
            _repository.Create(shift);
            return shift;

        }

        public bool DeleteShift(Shift shift)
        {
            _repository.Delete(shift);
            return true;
        }

        public List<Shift> GetAllShifts()
        {
            return _repository.GetAll();
        }

        public Shift GetShift(int id)
        {
            if (_repository.GetById(id) is not null)
            {
                return _repository.GetById(id);
            }
            return null!;
        }

        public Shift UpdateShift(Shift shift)
        {
            ValidateShift(shift);
            _repository.Update(shift);
            return shift;
        }

        private bool EmployeeExists(int id)
        {
            return _employeeService.GetEmployee(id) is not null;
        }

        private void ValidateShift(Shift shift)
        {
            if (!EmployeeExists(shift.EmployeeId))
            {
                throw new ShiftValidationException("Employee does not exist.");
            }
            ShiftValidation.Validate(shift);
        }
    }
}
