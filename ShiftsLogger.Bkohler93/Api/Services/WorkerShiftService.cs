using Api.Data.Entities;
using Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;

# pragma warning disable CS1591
public class WorkerShiftService {
   private readonly AppDbContext db;
   public WorkerShiftService(AppDbContext dbContext)
   {
        db = dbContext; 
   } 

   public async Task<IEnumerable<GetWorkerShiftDto>> GetWorkerShifts()
   {
        var workerShifts = await db.WorkerShifts
            .Include(ws => ws.Worker)
            .Include(ws => ws.Shift)
            .ToListAsync();

        return workerShifts.Select(ws => new GetWorkerShiftDto{
            Id = ws.Id,
            WorkerId = ws.WorkerId,
            ShiftId = ws.ShiftId,
            ShiftDate = ws.ShiftDate,
            Worker = new GetWorkerDto{
                FirstName = ws.Worker!.FirstName,
                LastName = ws.Worker.LastName,
                Position = ws.Worker.Position,
                Id = ws.Worker.Id,
            },
            Shift = new GetShiftDto{
                Id = ws.Shift!.Id,
                Name = ws.Shift.Name,
                StartTime = ws.Shift.StartTime,
                EndTime = ws.Shift.EndTime,
            },
        }).ToList();
   }

   public async Task<GetWorkerShiftDto?> GetWorkerShift(int id)
   {
        var workerShift = await db.WorkerShifts.Include(ws => ws.Shift).Include(ws => ws.Worker).FirstOrDefaultAsync(ws => ws.Id == id);
        
        if (workerShift == null) {
            return null; 
        }

        return new GetWorkerShiftDto {
            Id = workerShift.Id,
            WorkerId = workerShift.WorkerId,
            ShiftId = workerShift.ShiftId,
            ShiftDate = workerShift.ShiftDate,
            Worker = new GetWorkerDto{
                Id = workerShift.Worker!.Id,
                FirstName = workerShift.Worker.FirstName,
                LastName = workerShift.Worker.LastName,
                Position = workerShift.Worker.Position,
            },
            Shift = new GetShiftDto{
                Id = workerShift.Shift!.Id,
                Name = workerShift.Shift.Name,
                StartTime = workerShift.Shift.StartTime,
                EndTime = workerShift.Shift.EndTime,
            }
        };
   }

   public async Task<WorkerShift> CreateWorkerShift(PostWorkerShiftDto dto)
   {
        var workerShift = new WorkerShift {
            WorkerId = dto.WorkerId,
            ShiftId = dto.ShiftId,
            ShiftDate = dto.ShiftDate,
        };

        db.WorkerShifts.Add(workerShift);
        await db.SaveChangesAsync(); 

        return workerShift;
   }

   public async Task<WorkerShift?> FindWorkerShiftEntity(int id)
   {
    return await db.WorkerShifts.FindAsync(id);
   }

   public async Task UpdateWorkerShift(WorkerShift workerShift, PutWorkerShiftDto dto)
   {
        workerShift.ShiftDate = dto.ShiftDate;
        workerShift.ShiftId = dto.ShiftId;
        workerShift.WorkerId = dto.WorkerId;

        db.Entry(workerShift).State = EntityState.Modified;
        await db.SaveChangesAsync();
   }

   public async Task DeleteWorkerShift(WorkerShift workerShift)
   {
        db.WorkerShifts.Remove(workerShift);
        await db.SaveChangesAsync();
   }
}
# pragma warning restore CS1591