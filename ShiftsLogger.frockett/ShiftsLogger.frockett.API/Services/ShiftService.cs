using ShiftsLogger.frockett.API.DTOs;
using ShiftsLogger.frockett.API.Models;
using ShiftsLogger.frockett.API.Repositories;

namespace ShiftsLogger.frockett.API.Services;

public class ShiftService
{
    private readonly IShiftsRepository shiftsRepository;

    public ShiftService(IShiftsRepository shiftsRepository)
    {
        this.shiftsRepository = shiftsRepository;
    }

    public async Task<ShiftDto> AddShiftAsync(ShiftCreateDto shiftCreateDto)
    {
        var shift = new Shift
        {
            EmployeeId = shiftCreateDto.EmployeeId,
            StartTime = shiftCreateDto.StartTime,
            EndTime = shiftCreateDto.EndTime,
        };
        var createdShift = await shiftsRepository.AddShiftAsync(shift);

        return new ShiftDto
        {
            Id = createdShift.Id,
            StartTime = createdShift.StartTime,
            EndTime = createdShift.EndTime,
            EmployeeId = createdShift.EmployeeId,
            EmployeeName = createdShift.Employee.Name
        };
    }

    public async Task<IEnumerable<ShiftDto>> GetAllShiftsAsync()
    {
        var shifts = await shiftsRepository.GetAllShiftsAsync();

        var shiftDtos = shifts.Select(shift => new ShiftDto
        {
            Id = shift.Id,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            //Duration = shift.Duration,
            EmployeeId = shift.EmployeeId,
            EmployeeName = shift.Employee.Name
        }).ToList();

        return shiftDtos;
    }

    public async Task<ShiftDto> GetShiftsByIdAsync(int shiftId)
    {
        var shift = await shiftsRepository.GetShiftByIdAsync(shiftId);

        var shiftDto = new ShiftDto
        {
            Id = shift.Id,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            //Duration = shift.Duration,
            EmployeeId = shift.EmployeeId,
            EmployeeName = shift.Employee.Name
        };

        return shiftDto;
    }

    public async Task<ShiftDto> UpdateShiftAsync(int shiftId,  ShiftDto shiftDto)
    {
        Shift shift = new Shift
        {
            Id = shiftId,
            StartTime = shiftDto.StartTime,
            EndTime = shiftDto.EndTime,
            EmployeeId = shiftDto.EmployeeId,
        };

        var updatedShift = await shiftsRepository.UpdateShiftAsync(shift);

        return new ShiftDto
        {
            Id = updatedShift.Id,
            StartTime = updatedShift.StartTime,
            EndTime = updatedShift.EndTime,
            //Duration = updatedShift.Duration,
            EmployeeId = updatedShift.EmployeeId,
            EmployeeName = updatedShift.Employee.Name
        };
    }

    public async Task<bool> DeleteShiftAsync(int shiftId)
    {
        bool isDeleted = await shiftsRepository.DeleteShiftAsync(shiftId);
        
        return isDeleted ? true : false;
    }
}
