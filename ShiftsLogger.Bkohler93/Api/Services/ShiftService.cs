using Api.Data.Entities;
using Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;

# pragma warning disable CS1591
public class ShiftService {
    private readonly AppDbContext db;
    public ShiftService(AppDbContext dbContext)
    {
        db = dbContext; 
    }

    public async Task<IEnumerable<GetShiftDto>> GetShiftsAsync()
    {
        var shifts = await db.Shifts.ToListAsync();

        return shifts.Select(s => new GetShiftDto {
            Id = s.Id,
            Name = s.Name,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
        }).ToList();
    }

    public async Task<GetShiftDto?> GetShiftById(int id)
    {
        var shift = await db.Shifts.FindAsync(id);
        if (shift == null) {
            return null; 
        }

        return new GetShiftDto {
            Id = shift.Id,
            Name = shift.Name,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime
        };
    }

    public async Task<GetShiftDto> CreateShift(PostShiftDto dto)
    {
        var shift = new Shift {
            Name = dto.Name,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            WorkerShifts = []
        };

        db.Shifts.Add(shift);
        await db.SaveChangesAsync();

        return new GetShiftDto {
            Id = shift.Id,
            Name = shift.Name,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
        };
    }

    public async Task<Shift?> FindShift(int id)
    {
        var shift = await db.Shifts.FindAsync(id);

        return shift;
    }

    public async Task<bool> UpdateShift(Shift shift, PutShiftDto dto)
    {
        shift.Name = dto.Name;
        shift.StartTime = dto.StartTime;
        shift.EndTime = dto.EndTime;

        db.Entry(shift).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return true;
    }
    public async Task DeleteShift(Shift shift)
    {
        db.Shifts.Remove(shift);
        await db.SaveChangesAsync();
    }
}
# pragma warning restore CS1591