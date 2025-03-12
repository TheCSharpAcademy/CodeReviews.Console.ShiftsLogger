using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ShiftsLoggerAPI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepo;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepo = employeeRepository;
    }

    public async Task<ServiceResponse<List<Employee>>> GetAllEmployeesAsync()
    {
        ServiceResponse<List<Employee>> _response = new ServiceResponse<List<Employee>>();

        try
        {
            var employeeList = await _employeeRepo.GetAllWithShiftsAsync();
            if (employeeList.Any())
            {
                _response.Success = true;
                _response.Message = "Ok";
                _response.Data = employeeList;
            }
            else
            {
                _response.Message = "NotFound";
            }
        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Error = $"Internal server error:{ex.Message}";
        }
        return _response;

    }
    public async Task<ServiceResponse<Employee>> GetEmployeeByIdAsync(long id)
    {
        ServiceResponse<Employee> _response = new ServiceResponse<Employee>();

        try
        {
            var employee = await _employeeRepo.GetByIdWithShiftsAsync(id);

            if (employee == null)
            {
                _response.Success = false;
                _response.Message = $"Employee with ID {id} not found";
                _response.Data = null;
                return _response;
            }

            _response.Success = true;
            _response.Message = "Ok";
            _response.Data = employee;

        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Error = $"Internal server error:{ex.Message}";

        }
        return _response;
    }

    public async Task<ServiceResponse<Employee>> CreateEmployee(EmployeeDTO employeeDTO)
    {
        ServiceResponse<Employee> _response = new ServiceResponse<Employee>();

        try
        {
            var createdEmployee = await _employeeRepo.CreateWithShiftAsync(employeeDTO);

            if (createdEmployee == null)
            {
                _response.Success = false;
                _response.Message = $"Employee with ID {employeeDTO.EmployeeId} not found";
                _response.Data = null;
                return _response;
            }

            _response.Success = true;
            _response.Message = "Ok";
            _response.Data = createdEmployee;
        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Error = $"Internal server error:{ex.Message}";
        }
        return _response;
    }
    public async Task<ServiceResponse<Employee>> UpdateEmployee(long id, EmployeeDTO employeeDTO)
    {
        ServiceResponse<Employee> _response = new ServiceResponse<Employee>();
        try
        {
            var savedEmployee = await _employeeRepo.UpdateWithShiftsAsync(id, employeeDTO);

            if (savedEmployee == null)
            {
                _response.Success = false;
                _response.Message = $"Employee with ID {id} not found";
                _response.Data = null;
                return _response;
            }

            _response.Success = true;
            _response.Message = "Updated";
            _response.Data = savedEmployee;
        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Error = $"Internal server error:{ex.Message}";
        }
        return _response;
    }
    public async Task<ServiceResponse<bool>> DeleteEmployee(long id)
    {
        ServiceResponse<bool> _response = new ServiceResponse<bool>();
        try
        {
            var isEmployeeDeleted = await _employeeRepo.DeleteEmployeeAsync(id);

            if (!isEmployeeDeleted)
            {
                _response.Success = false;
                _response.Message = $"Employee with ID {id} not found";
                _response.Data = false;
                return _response;
            }

            _response.Success = true;
            _response.Message = "Employee deleted successfully";
            _response.Data = true;
        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Error = $"Internal server error:{ex.Message}";
        }
        return _response;
    }

}