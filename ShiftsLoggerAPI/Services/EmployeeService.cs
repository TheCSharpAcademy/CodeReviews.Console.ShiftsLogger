using AutoMapper;
using SharedLibrary.DTOs;
using SharedLibrary.Models;
using SharedLibrary.Validations;
using ShiftsLoggerAPI.Interfaces;

namespace ShiftsLoggerAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        private IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public void CreateEmployee(CreateEmployeeDto employee)
        {
            EmployeeValidation.Validate(_mapper.Map<EmployeeDto>(employee));
            _employeeRepository.Create(_mapper.Map<Employee>(employee));
        }

        public void DeleteEmployee(int id)
        {
            if (EmployeeExists(id))
            {
                EmployeeValidation.Validate(_mapper.Map<EmployeeDto>(GetEmployee(id)));
                var e = _employeeRepository.GetById(id);
                _employeeRepository.Delete(e);
            }
        }

        public EmployeeDto GetEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);

            return _mapper.Map<EmployeeDto>(employee);
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            return _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetAll());
        }

        public void UpdateEmployee(UpdateEmployeeDto employeeDto, int id)
        {
            EmployeeValidation.Validate(_mapper.Map<EmployeeDto>(employeeDto));

            if (EmployeeExists(id))
            {
                var employee = _employeeRepository.GetById(id);
                _mapper.Map(employeeDto, employee);
                _employeeRepository.Update(employee); 
            }
        }

        private bool EmployeeExists(int id) => GetEmployee(id) is not null;
    }
}
