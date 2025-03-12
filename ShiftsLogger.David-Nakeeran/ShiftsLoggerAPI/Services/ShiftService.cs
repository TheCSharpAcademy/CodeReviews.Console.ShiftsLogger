using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;


namespace ShiftsLoggerAPI.Services;

public class ShiftService : IShiftService
{
    private readonly IShiftRepository _shiftRepo;

    private readonly IEmployeeRepository _employeeRepo;

    public ShiftService(IShiftRepository shiftRepository, IEmployeeRepository employeeRepo)
    {
        _shiftRepo = shiftRepository;
        _employeeRepo = employeeRepo;
    }

    public async Task<ServiceResponse<List<Shift>>> GetAllShiftsAsync()
    {
        ServiceResponse<List<Shift>> _response = new ServiceResponse<List<Shift>>();

        try
        {
            var shiftList = await _shiftRepo.GetAllWithEmployeeAsync();
            if (shiftList.Any())
            {
                _response.Success = true;
                _response.Message = "Ok";
                _response.Data = shiftList;
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

    public async Task<ServiceResponse<Shift>> GetShiftByIdAsync(long id)
    {
        ServiceResponse<Shift> _response = new ServiceResponse<Shift>();

        try
        {
            var shift = await _shiftRepo.GetByIdWithEmployeeAsync(id);

            if (shift == null)
            {
                _response.Success = false;
                _response.Message = $"Shift with ID {id} not found";
                _response.Data = null;
                return _response;
            }

            _response.Success = true;
            _response.Message = "Ok";
            _response.Data = shift;

        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Error = $"Internal server error:{ex.Message}";

        }
        return _response;
    }

    public async Task<ServiceResponse<Shift>> UpdateShift(long id, ShiftDTO shiftDTO)
    {
        ServiceResponse<Shift> _response = new ServiceResponse<Shift>();
        try
        {
            var savedShift = await _shiftRepo.UpdateWithEmployeeAsync(id, shiftDTO);

            if (savedShift == null)
            {
                _response.Success = false;
                _response.Message = $"Shift with ID {id} not found";
                _response.Data = null;
                return _response;
            }

            _response.Success = true;
            _response.Message = "Updated";
            _response.Data = savedShift;
        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Error = $"Internal server error:{ex.Message}";
        }
        return _response;
    }

    public async Task<ServiceResponse<Shift>> CreateShift(ShiftDTO shiftDTO)
    {
        ServiceResponse<Shift> _response = new ServiceResponse<Shift>();

        try
        {
            var employeeExists = await _employeeRepo.EmployeeExistsAsync(shiftDTO.EmployeeId);
            if (!employeeExists)
            {
                _response.Success = false;
                _response.Message = "Employee does not exist";
                return _response;
            }
            var createdShift = await _shiftRepo.CreateWithEmployeeAsync(shiftDTO);

            if (createdShift == null)
            {
                _response.Success = false;
                _response.Message = $"Shift with ID {shiftDTO.EmployeeId} not found";
                _response.Data = null;
                return _response;
            }

            _response.Success = true;
            _response.Message = "Ok";
            _response.Data = createdShift;
        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Error = $"Internal server error:{ex.Message}";
        }
        return _response;
    }

    public async Task<ServiceResponse<bool>> DeleteShift(long id)
    {
        ServiceResponse<bool> _response = new ServiceResponse<bool>();
        try
        {
            var isShiftDeleted = await _shiftRepo.DeleteAsync(id);

            if (!isShiftDeleted)
            {
                _response.Success = false;
                _response.Message = $"Shift with ID {id} not found";
                _response.Data = false;
                return _response;
            }

            _response.Success = true;
            _response.Message = "Shift deleted successfully";
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