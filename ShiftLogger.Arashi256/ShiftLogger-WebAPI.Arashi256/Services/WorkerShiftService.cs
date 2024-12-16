using Microsoft.EntityFrameworkCore;
using ShiftLogger_Shared.Arashi256.Models;
using ShiftLogger_Shared.Arashi256.Classes;
using ShiftLogger_WebAPI.Arashi256.Models;
using System.Globalization;

namespace ShiftLogger_WebAPI.Arashi256.Services
{
    public class WorkerShiftService
    {
        private const string SHIFT_DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm";
        // DB Context
        private readonly ShiftLoggerContext _context;
        public WorkerShiftService(ShiftLoggerContext context)
        {
            _context = context;
        }

        // Check if workershift exists by Id
        private async Task<bool> WorkerShiftExistsAsync(int id)
        {
            return await _context.WorkerShifts.AnyAsync(e => e.Id == id);
        }

        // Method to calculate the Duration from ShiftStart and ShiftEnd
        private TimeSpan? CalculateDuration(DateTime shiftStart, DateTime shiftEnd)
        {
            if (shiftEnd < shiftStart)
            {
                return null;
            }
            return shiftEnd - shiftStart;
        }

        private string FormatDurationToHoursMinutes(TimeSpan duration)
        {
            int totalHours = (int)duration.TotalHours;
            int minutes = duration.Minutes;
            return $"{totalHours:D2}:{minutes:D2}";
        }

        // Private method to check if a shift overlaps with any existing shifts for the worker
        private async Task<bool> IsOverlappingShiftAsync(int workerId, DateTime shiftStart, DateTime shiftEnd, int? shiftIdToExclude = null)
        {
            return await _context.WorkerShifts
                .Where(ws => ws.WorkerId == workerId && (shiftIdToExclude == null || ws.Id != shiftIdToExclude))
                .AnyAsync(ws => shiftStart < ws.ShiftEnd && shiftEnd > ws.ShiftStart);
        }

        // Method to check if the shift already exists for a worker
        private async Task<bool> IsDuplicateShiftAsync(int workerId, DateTime shiftStart, DateTime shiftEnd)
        {
            return await _context.WorkerShifts
                .AnyAsync(ws => ws.WorkerId == workerId && ws.ShiftStart == shiftStart && ws.ShiftEnd == shiftEnd);
        }

        private DateTime? StringToDateTime(string? t)
        {
            if (t == null) return null;
            try
            {
                // Parse the input string into a DateTime object
                DateTime parsedDateTime = DateTime.ParseExact(t, SHIFT_DATE_TIME_FORMAT, CultureInfo.InvariantCulture);
                return parsedDateTime;
            }
            catch (FormatException)
            {
                return null;
            }
        }

        private string? DateTimeToString(DateTime t)
        {
            try
            {
                string converted = t.ToString(SHIFT_DATE_TIME_FORMAT);
                return converted;
            }
            catch (FormatException)
            {
                return null;
            }
        }

        private async Task<bool> IsValidDateTimeString(string? dateTime)
        {
            bool isValid = dateTime != null && StringToDateTime(dateTime).HasValue;
            return await Task.FromResult(isValid); // Return Task with the result
        }

        // Method to get all WorkerShifts
        public async Task<ServiceResponse> GetAllWorkerShiftsWithDisplayIdsAsync()
        {
            // Step 1: Fetch the workershifts and project them into WorkerShiftDto
            var workershifts = await _context.WorkerShifts
                .Include(ws => ws.Worker) // to fetch the worker details
                .Select(ws => new WorkerShiftOutputDto
                {
                    Id = ws.Id,
                    WorkerId = ws.WorkerId,
                    ShiftStart = ws.ShiftStart,
                    ShiftEnd = ws.ShiftEnd,
                    Duration = ws.Duration,
                    Worker = new WorkerOutputDto { Id = ws.Worker.Id, DisplayId = 1, FirstName = ws.Worker.FirstName, LastName = ws.Worker.LastName, Email = ws.Worker.Email }
                }).ToListAsync();
            // Step 2: Assign DisplayId (incremental) and format display data to each WorkerShiftOutputDto
            int i = 0;
            var workershiftsWithDisplayIds = workershifts.Select(workershift =>
            {
                workershift.DisplayId = ++i;
                workershift.DisplayShiftStart = DateTimeToString(workershift.ShiftStart);
                workershift.DisplayShiftEnd = DateTimeToString(workershift.ShiftEnd);
                workershift.DisplayDuration = FormatDurationToHoursMinutes(workershift.Duration);
                return workershift;
            }).ToList();
            if (workershiftsWithDisplayIds.Count > 0) 
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", workershiftsWithDisplayIds);
            else
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No WorkerShifts found", null);
        }

        // Method to get all WorkerShifts for a given Worker
        public async Task<ServiceResponse> GetAllWorkerShiftsForWorkerAsync(int workerId)
        {
            var workerShifts = await _context.WorkerShifts
                .Where(ws => ws.WorkerId == workerId)
                .Select(ws => new WorkerShiftOutputDto
                {
                    Id = ws.Id,
                    WorkerId = ws.WorkerId,
                    ShiftStart = ws.ShiftStart,
                    ShiftEnd = ws.ShiftEnd,
                    Duration = ws.Duration,
                    Worker = new WorkerOutputDto { Id = ws.Worker.Id, DisplayId = 1, FirstName = ws.Worker.FirstName, LastName = ws.Worker.LastName, Email = ws.Worker.Email }
                })
                .ToListAsync();
            // Step 2: Assign DisplayId (incremental) and format display data to each WorkerShiftOutputDto
            int i = 0;
            var workershiftsWithDisplayIds = workerShifts.Select(workershift =>
            {
                workershift.DisplayShiftStart = DateTimeToString(workershift.ShiftStart);
                workershift.DisplayShiftEnd = DateTimeToString(workershift.ShiftEnd);
                workershift.DisplayDuration = FormatDurationToHoursMinutes(workershift.Duration);
                workershift.DisplayId = ++i;
                return workershift;
            }).ToList();
            if (workershiftsWithDisplayIds.Count > 0)
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", workershiftsWithDisplayIds);
            else
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No WorkerShifts found for Worker", null);
        }

        // Method to get a single WorkerShift by Id
        public async Task<ServiceResponse> GetWorkerShiftAsync(int id)
        {
            var workerShift = await _context.WorkerShifts
                .Include(ws => ws.Worker)
                .FirstOrDefaultAsync(ws => ws.Id == id);
            if (workerShift == null)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No WorkerShift found for ID", null);
            }
            var workerShiftDto =  new WorkerShiftOutputDto
            {
                Id = workerShift.Id,
                DisplayId = 1,
                WorkerId = workerShift.WorkerId,
                ShiftStart = workerShift.ShiftStart,
                ShiftEnd = workerShift.ShiftEnd,
                Duration = workerShift.Duration,
                DisplayShiftStart = DateTimeToString(workerShift.ShiftStart),
                DisplayShiftEnd = DateTimeToString(workerShift.ShiftEnd),
                DisplayDuration = FormatDurationToHoursMinutes(workerShift.Duration),
                Worker = new WorkerOutputDto { Id = workerShift.Worker.Id, DisplayId = 1, FirstName = workerShift.Worker.FirstName, LastName = workerShift.Worker.LastName, Email = workerShift.Worker.Email }
            };
            return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", workerShiftDto);
        }

        // Method to add a new WorkerShift
        public async Task<ServiceResponse> AddWorkerShiftAsync(WorkerShiftInputDto workerShiftDto)
        {
            var worker = await _context.Workers.FindAsync(workerShiftDto.WorkerId);
            if (worker == null)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Worker not found for this WorkerShift", null);
            }
            DateTime? shiftStartDateTime = StringToDateTime(workerShiftDto.ShiftStart);
            DateTime? shiftEndDateTime = StringToDateTime(workerShiftDto.ShiftEnd);
            if (await IsValidDateTimeString(workerShiftDto.ShiftStart) == false || await IsValidDateTimeString(workerShiftDto.ShiftEnd) == false || shiftStartDateTime == null || shiftEndDateTime == null)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"WorkerShift start or end time improper format. Should be '{SHIFT_DATE_TIME_FORMAT}'", null);
            }
            else
            {
                if (await IsDuplicateShiftAsync(workerShiftDto.WorkerId, (DateTime)shiftStartDateTime, (DateTime)shiftEndDateTime))
                {
                    return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "WorkerShift already exists for the Worker", null);
                }
                if (await IsOverlappingShiftAsync(workerShiftDto.WorkerId, (DateTime)shiftStartDateTime, (DateTime)shiftEndDateTime))
                {
                    return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "WorkerShift overlaps with an existing WorkerShift for the Worker", null);
                }
                // Calculate the duration
                TimeSpan? duration = CalculateDuration((DateTime)shiftStartDateTime, (DateTime)shiftEndDateTime);
                if (duration == null)
                {
                    return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Shift end cannot be before shift start", null);
                }
                var workerShift = new WorkerShift
                {
                    WorkerId = workerShiftDto.WorkerId,
                    ShiftStart = (DateTime)shiftStartDateTime,
                    ShiftEnd = (DateTime)shiftEndDateTime,
                    Duration = (TimeSpan)duration,
                    Worker = worker
                };
                _context.WorkerShifts.Add(workerShift);
                await _context.SaveChangesAsync();
                var workerShiftOutputDto = new WorkerShiftOutputDto
                {
                    Id = workerShift.Id,
                    WorkerId = worker.Id,
                    DisplayId = 1,
                    DisplayShiftStart = workerShift.ShiftStart.ToString("dd-MM-yy HH:mm"),
                    DisplayShiftEnd = workerShift.ShiftEnd.ToString("dd-MM-yy HH:mm"),
                    DisplayDuration = workerShift.Duration.ToString(@"hh\:mm"),
                    Worker = new WorkerOutputDto { Id = worker.Id, DisplayId = 1, FirstName = worker.FirstName, LastName = worker.LastName, Email = worker.Email }
                };
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", workerShiftOutputDto);
            }
        }

        // Method to update an existing WorkerShift
        public async Task<ServiceResponse> UpdateWorkerShiftAsync(int id, WorkerShiftInputDto workerShiftDto)
        {
            if (id != workerShiftDto.Id)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Id mismatch", null);
            }
            var worker = await _context.Workers.FindAsync(workerShiftDto.WorkerId);
            if (worker == null)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Worker not found for this WorkerShift", null);
            }
            var workerShift = await _context.WorkerShifts.FindAsync(id);
            if (workerShift == null)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "WorkerShift not found", null);
            }
            DateTime? shiftStartDateTime = StringToDateTime(workerShiftDto.ShiftStart);
            DateTime? shiftEndDateTime = StringToDateTime(workerShiftDto.ShiftEnd);
            if (await IsValidDateTimeString(workerShiftDto.ShiftStart) == false || await IsValidDateTimeString(workerShiftDto.ShiftEnd) == false || shiftStartDateTime == null || shiftEndDateTime == null)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"WorkerShift start and/or end time improper format. Should be '{SHIFT_DATE_TIME_FORMAT}'", null);
            }
            else
            {
                if (await IsDuplicateShiftAsync(workerShiftDto.WorkerId, (DateTime)shiftStartDateTime, (DateTime)shiftEndDateTime))
                {
                    return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "WorkerShift already exists for the Worker", null);
                }
                if (await IsOverlappingShiftAsync(workerShiftDto.WorkerId, (DateTime)shiftStartDateTime, (DateTime)shiftEndDateTime, workerShiftDto.Id))
                {
                    return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "WorkerShift overlaps with an existing WorkerShift for the Worker", null);
                }
                // Calculate the duration
                TimeSpan? duration = CalculateDuration((DateTime)shiftStartDateTime, (DateTime)shiftEndDateTime);
                if (duration == null)
                {
                    return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Shift end cannot be before shift start", null);
                }
                // Update the properties
                workerShift.WorkerId = worker.Id;
                workerShift.ShiftStart = (DateTime)shiftStartDateTime;
                workerShift.ShiftEnd = (DateTime)shiftEndDateTime;
                workerShift.Duration = (TimeSpan)duration;
                workerShift.Worker = worker;
                _context.Entry(workerShift).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                    return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", null);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await WorkerShiftExistsAsync(id))
                    {
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "DB update concurrency conflict: WorkerShift no longer exists. Please try again", null);
                    }
                    else
                    {
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Concurrency conflict: WorkerShift has been modified outside this request. Please try again", null);
                    }
                }
            }
        }

        // Method to delete a WorkerShift
        public async Task<ServiceResponse> DeleteWorkerShiftAsync(int id)
        {
            var workerShift = await _context.WorkerShifts.FindAsync(id);
            if (workerShift == null)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "WorkerShift not found", null);
            }
            _context.WorkerShifts.Remove(workerShift);
            await _context.SaveChangesAsync();
            return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", null);
        }
    }
}
