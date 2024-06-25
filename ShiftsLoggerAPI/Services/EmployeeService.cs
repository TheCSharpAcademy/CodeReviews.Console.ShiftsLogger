using SharedLibrary.Models;
using SharedLibrary.Validations;
using ShiftsLoggerAPI.Interfaces;

namespace ShiftsLoggerAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void CreateEmployee(Employee employee)
        {
            EmployeeValidation.Validate(employee);
            _employeeRepository.Create(employee);
        }

        public void DeleteEmployee(int id)
        {
            if (EmployeeExists(id))
            {
                var employee = GetEmployee(id);
                EmployeeValidation.Validate(employee);
                _employeeRepository.Delete(employee);
            }
        }

        public Employee GetEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);

            return employee;
        }

        public List<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAll();
        }

        public void UpdateEmployee(Employee employee)
        {
            EmployeeValidation.Validate(employee);

            if (EmployeeExists(employee.Id))
            {
                _employeeRepository.Update(employee);
            }
        }

        private bool EmployeeExists(int id) => GetEmployee(id) is not null;
    }
}
