using AutoMapper;
using SharedLibrary.DTOs;
using SharedLibrary.Models;
using SharedLibrary.Validations;
using ShiftsLoggerAPI.Interfaces;

namespace ShiftsLoggerAPI.Services
{
    public class ShiftService : IShiftService
    {
        private IShiftRepository _repository;
        private IEmployeeService _employeeService;
        private IMapper _mapper;

        public ShiftService(IShiftRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public CreateShiftDto CreateShift(CreateShiftDto shift)
        {
            var domain = _mapper.Map<Shift>(shift);
            ValidateShift(domain);
            _repository.Create(domain);
            return shift;
        }

        public bool DeleteShift(ShiftDto shift)
        {
            _repository.Delete(_mapper.Map<Shift>(shift));
            return true;
        }

        public List<ShiftDto> GetAllShifts()
        {
            return _mapper.Map<List<ShiftDto>>(_repository.GetAll());
        }

        public ShiftDto GetShift(int id)
        {
            if (_repository.GetById(id) is not null)
            {
                return _mapper.Map<ShiftDto>(_repository.GetById(id));
            }
            return null!;
        }

        public UpdateShiftDto UpdateShift(UpdateShiftDto shift)
        {
            var domain = _mapper.Map<Shift>(shift);
            ValidateShift(domain);
            _repository.Update(domain);
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
