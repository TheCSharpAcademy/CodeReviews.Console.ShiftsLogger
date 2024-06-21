﻿using SharedLibrary.Extensions;
using SharedLibrary.Models;
using SharedLibrary.Validations;
using ShiftsLoggerAPI.Interfaces;
using System.ComponentModel.DataAnnotations;

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
            EmployeeValidation.Validate(employee);
            _employeeRepository.AddEmployee(employee);
        }

        public void DeleteEmployee(int id)
        {
            if (EmployeeExists(id))
            {
                var employee = GetEmployee(id);
                EmployeeValidation.Validate(employee);
                _employeeRepository.DeleteEmployee(employee);
            }
        }

        public Employee GetEmployee(int id)
        {
            var employee = _employeeRepository.GetEmployee(id);

            return employee;
        }

        public List<Employee> GetEmployees()
        {
            return _employeeRepository.GetEmployees();
        }

        public void UpdateEmployee(Employee employee)
        {
            EmployeeValidation.Validate(employee);

            if (EmployeeExists(employee.Id))
            {
                _employeeRepository.UpdateEmployee(employee);
            }
        }

        private bool EmployeeExists(int id) => GetEmployee(id) is not null;
    }
}
