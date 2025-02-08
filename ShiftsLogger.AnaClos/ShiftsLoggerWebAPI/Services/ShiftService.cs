using Microsoft.EntityFrameworkCore;
using ShiftsLoggerWebAPI.Data;
using ShiftsLoggerWebAPI.DTOs;
using ShiftsLoggerWebAPI.Models;

namespace ShiftsLoggerWebAPI.Services
{
    public class ShiftService : IShiftService
    {
        private readonly ShiftsDbContext _dbContext;

        public ShiftService(ShiftsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string? CreateShift(ShiftDto shiftDto)
        {
            if (shiftDto == null)
            {
                return null;
            }

            try
            {
                Shift shift = Dto2Model(shiftDto);
                _dbContext.Shifts.Add(shift);
                _dbContext.SaveChanges();
                return "Successfully created shift.";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string? DeleteShift(int id)
        {
            Shift savedShift = _dbContext.Shifts.Find(id);
            if (savedShift == null)
            {
                return null;
            }

            _dbContext.Shifts.Remove(savedShift);
            _dbContext.SaveChanges();
            return "Successfully deleted shift";
        }

        public List<ShiftDto>? GetAllShifts()
        {
            var shifts = _dbContext.Shifts.ToList();
            if (shifts == null)
            {
                return null;
            }

            var shiftsDto = new List<ShiftDto>();
            foreach (var shift in shifts)
            {
                shiftsDto.Add(Model2Dto(shift));
            }
            return shiftsDto;
        }

        public List<ShiftDto>? Get10ShiftsByEmployee(int employeeId)
        {
            var shifts = _dbContext.Shifts
           .Where(s => s.EmployeeId == employeeId)
           .OrderByDescending(s => s.Id)
           .Take(10)
           .ToList();

            if (shifts == null)
            {
                return null;
            }

            var shiftsDto = new List<ShiftDto>();
            foreach (var shift in shifts)
            {
                shiftsDto.Add(Model2Dto(shift));
            }
            return shiftsDto;
        }

        public ShiftDto? GetShiftById(int id)
        {
            var savedShift = _dbContext.Shifts.Find(id);
            if (savedShift == null)
            {
                return null;
            }
            return Model2Dto(savedShift);
        }

        public string? UpdateShift(ShiftDto updatedShift)
        {
            var savedShift = _dbContext.Shifts.Find(updatedShift.Id);
            if (savedShift == null)
            {
                return null;
            }
            try
            {
                _dbContext.Entry(savedShift).CurrentValues.SetValues(updatedShift);
                _dbContext.SaveChanges();
                return "Successfully updated shift.";
            }
            catch (DbUpdateException ex)
            {
                return "Failed to update shift";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static ShiftDto? Model2Dto(Shift shift)
        {
            if (shift == null)
            {
                return null;
            }
            ShiftDto shiftDto = new ShiftDto
            {
                Id = shift.Id,
                EmployeeId = shift.EmployeeId,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime
            };
            return shiftDto;
        }

        public static Shift? Dto2Model(ShiftDto shiftDto)
        {
            if (shiftDto == null)
            {
                return null;
            }
            Shift shift = new Shift
            {
                Id = shiftDto.Id,
                EmployeeId = shiftDto.EmployeeId,
                StartTime = shiftDto.StartTime,
                EndTime = shiftDto.EndTime
            };
            return shift;
        }

        public ShiftDto GetLastShift(int id)
        {
            var shift = _dbContext.Shifts.LastOrDefault(s => s.Employee.Id == id);
            if (shift == null)
            {
                return null;
            }

            return Model2Dto(shift);
        }
    }
}