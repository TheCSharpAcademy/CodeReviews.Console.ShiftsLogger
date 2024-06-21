using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
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

        public void AddEmployee(Employee employee)
        {
            _employeeRepository.AddEmployee(employee);
        }

        public void DeleteEmployee(int id)
        {
            var employee = GetEmployee(id);
            _employeeRepository.DeleteEmployee(employee);
        }

        public Employee GetEmployee(int id)
        {
            return _employeeRepository.GetEmployee(id);
        }

        public List<Employee> GetEmployees()
        {
            return _employeeRepository.GetEmployees();
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
        }
    }
}
