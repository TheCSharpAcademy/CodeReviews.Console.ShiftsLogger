using Microsoft.EntityFrameworkCore;
using ShiftLogger_Shared.Arashi256.Models;
using ShiftLogger_Shared.Arashi256.Classes;
using ShiftLogger_WebAPI.Arashi256.Models;
using System.Text.RegularExpressions;

namespace ShiftLogger_WebAPI.Arashi256.Services
{
    public class WorkerService
    {
        // DB Context
        private readonly ShiftLoggerContext _context;
        // Regular expression for validating email format
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public WorkerService(ShiftLoggerContext context)
        {
            _context = context;
        }

        // Method to check if a Worker with the same Email exists
        private async Task<bool> WorkerExistsAsync(string email)
        {
            return await _context.Workers.AnyAsync(worker => worker.Email.ToLower() == email.ToLower());
        }

        // Method to get all workers and assign DisplayId
        public async Task<ServiceResponse> GetAllWorkersWithDisplayIdsAsync()
        {
            // Step 1: Fetch the workers and project them into WorkerDto
            var workers = await _context.Workers.Select(worker => new WorkerOutputDto
            {
                Id = worker.Id,
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                Email = worker.Email
            }).ToListAsync();
            // Step 2: Assign DisplayId (incremental) to each WorkerDto
            int i = 0;
            var workersWithDisplayIds = workers.Select(worker =>
            {
                worker.DisplayId = ++i;
                return worker;
            }).ToList();
            if (workersWithDisplayIds.Count > 0)
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", workersWithDisplayIds);
            else
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No Workers found", workersWithDisplayIds);
        }

        // Method to get a worker by Id and map it to WorkerOutputDto
        public async Task<ServiceResponse> GetWorkerByIdAsync(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Worker not found", null);
            }
            var workerDto = new WorkerOutputDto
            {
                Id = worker.Id,
                DisplayId = 1,
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                Email = worker.Email
            };
            return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", workerDto);
        }

        // Method to update a worker based on WorkerDto
        public async Task<ServiceResponse> UpdateWorkerAsync(int id, WorkerInputDto workerDto)
        {
            if (id != workerDto.Id)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Update Worker and Id mismatch", null);
            }
            // Validate email format
            if (!IsValidEmailFormat(workerDto.Email))
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Invalid email format", null);
            }
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Worker not found for update", null);
            }
            // Check if another worker with the same email exists (excluding the current worker)
            if (await _context.Workers.AnyAsync(w => w.Email.ToLower() == workerDto.Email.ToLower() && w.Id != id))
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "A Worker with the same email address already exists", null);
            }
            // Update the worker's properties
            worker.FirstName = workerDto.FirstName;
            worker.LastName = workerDto.LastName;
            worker.Email = workerDto.Email;
            _context.Entry(worker).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", null);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WorkerExistsAsync(id))
                {
                    return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "DB update concurrency conflict: Worker no longer exists. Please try again", null);
                }
                else
                {
                    return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Concurrency conflict: Worker has been modified outside this request. Please try again", null);
                }
            }
        }

        // Method to add a new worker
        public async Task<ServiceResponse> AddWorkerAsync(WorkerInputDto workerDto)
        {
            // Validate email format
            if (!IsValidEmailFormat(workerDto.Email))
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Invalid email format", null);
            }
            // Check if a worker with the same email already exists
            if (await WorkerExistsAsync(workerDto.Email))
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "A Worker with the same email address already exists", null);
            }
            var worker = new Worker
            {
                FirstName = workerDto.FirstName,
                LastName = workerDto.LastName,
                Email = workerDto.Email
            };
            // Add it
            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();
            var workerOutputDto = new WorkerOutputDto
            {
                Id = worker.Id,
                DisplayId = 1,
                FirstName = worker.FirstName,
                LastName = worker.LastName,
                Email = worker.Email
            };
            return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", workerOutputDto);
        }

        // Method to delete a worker by Id
        public async Task<ServiceResponse> DeleteWorkerAsync(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Worker for deletion not found", null);
            }
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", null);
        }

        // Check if worker exists by Id
        private async Task<bool> WorkerExistsAsync(int id)
        {
            return await _context.Workers.AnyAsync(e => e.Id == id);
        }

        // Method to validate email format
        public bool IsValidEmailFormat(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return EmailRegex.IsMatch(email);
        }
    }
}
