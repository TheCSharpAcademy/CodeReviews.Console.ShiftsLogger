using System.Net;
using AutoMapper;
using ShiftsLogger.Ryanw84.Data;
using ShiftsLogger.Ryanw84.Dtos;
using ShiftsLogger.Ryanw84.Models;
using Microsoft.EntityFrameworkCore;

namespace ShiftsLogger.Ryanw84.Services;

public class ShiftService : IShiftService
{
    private readonly ShiftsDbContext _dbContext;
    private readonly IMapper _mapper;

    public ShiftService(ShiftsDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponseDto<Shift>> CreateShift(ShiftApiRequestDto shift)
    {
        Shift newShift = _mapper.Map<Shift>(shift); // Use Mapper to map DTO to shift entity
        var savedShift = await _dbContext.Shift.AddAsync(newShift);
        await _dbContext.SaveChangesAsync();
        return new ApiResponseDto<Shift>
        {
            Data = savedShift.Entity,
            ResponseCode = HttpStatusCode.Created,
        };
    }

    public async Task<ApiResponseDto<List<Shift>>> GetAllShifts(ShiftOptions shiftOptions)
        {
        var query = _dbContext
            .Shift.Include(s => s.Worker) // Include the Worker navigation property
            .Include(s => s.Location) // Include the Location navigation property
            .AsQueryable();
        // AsQueryable() allows for expandable filtering and the queries are stackable
        var totalShifts = await query.CountAsync();

        List<Shift>? shifts;

        if(!string.IsNullOrWhiteSpace(shiftOptions.WorkerName))
            {
            query = query.Where(s => s.Worker != null && s.Worker.Name == shiftOptions.WorkerName); // Filter by Worker Name
            }

        if(!string.IsNullOrEmpty(shiftOptions.LocationName))
            {
            query = query.Where(s => s.Location != null && s.Location.Name != null && s.Location.Name == shiftOptions.LocationName); // Filter by Location Name
            }

        if(shiftOptions.ShiftStartTime.HasValue) // Not a string
            {
            query = query.Where(s => s.StartTime.Date <= shiftOptions.ShiftStartTime.Value.Date); // Filter by Shift start time
            }
        if(shiftOptions.ShiftEndTime.HasValue) // Not a string
            {
            query = query.Where(s => s.EndTime.Date <= shiftOptions.ShiftEndTime.Value.Date); // Filter by Shift end time
            }
        if(shiftOptions.SortBy == "id" || !string.IsNullOrEmpty(shiftOptions.SortBy))
        // allowing to send other values later on
            {
            switch(shiftOptions.SortBy)
                {
                case "worker_name":
                query =
                    shiftOptions.SortOrder == "ASC"
                        ? query.OrderBy(s => s.Worker != null ? s.Worker.Name : string.Empty)
                        : query.OrderByDescending(s => s.Worker != null ? s.Worker.Name : string.Empty);
                break;
                case "location_name":
                query =
                    shiftOptions.SortOrder == "ASC"
                        ? query.OrderBy(s => s.Location != null ? s.Location.Name : string.Empty)
                        : query.OrderByDescending(s => s.Location != null ? s.Location.Name : string.Empty);
                break;
                case "shift_start_time":
                query =
                    shiftOptions.SortOrder.ToUpper() == "ASC"
                        ? query.OrderBy(s => s.StartTime)
                        : query.OrderByDescending(s => s.StartTime);
                break;
                case "shift_end_time":
                query =
                    shiftOptions.SortOrder.ToUpper() == "ASC"
                        ? query.OrderBy(s => s.EndTime)
                        : query.OrderByDescending(s => s.EndTime);
                break;
                default:
                query =
                    shiftOptions.SortOrder == "ASC"
                        ? query.OrderBy(s => s.Id)
                        : query.OrderByDescending(s => s.Id);
                break;
                }
            }

        if(!string.IsNullOrEmpty(shiftOptions.Search))
            {
            string searchLower = shiftOptions.Search.ToLower();
            var searchChars = searchLower.ToCharArray();

            var data = await query.ToListAsync();

            shifts = data.Where(s =>
                    searchChars.All(c =>
                        (s.ShiftName != null && s.ShiftName.ToLower().Contains(c))
                        || (s.Worker != null && s.Worker.Name != null && s.Worker.Name.ToLower().Contains(c))
                        || (s.Location != null && s.Location.Name != null && s.Location.Name.ToLower().Contains(c))
                        || s.StartTime.ToString("yyyy-MM-ddTHH:mm:ss").ToLower().Contains(c)
                        || s.EndTime.ToString("yyyy-MM-ddTHH:mm:ss").ToLower().Contains(c)
                    )
                )
                .ToList();
            shifts =
                (List<Shift>)
                    data.Skip((shiftOptions.PageNumber - 1) * shiftOptions.PageSize)
                        .Take(shiftOptions.PageSize)
                        .ToList();
            } else
            {
            query = query
                .Skip((shiftOptions.PageNumber - 1) * shiftOptions.PageSize)
                .Take(shiftOptions.PageSize); //pagination

            shifts = await query.ToListAsync(); // Execute the query and get the results
            }

        bool hasPrevious = shiftOptions.PageNumber > 1;
        bool hasNext = (shiftOptions.PageNumber * shiftOptions.PageSize) < totalShifts;

        return new ApiResponseDto<List<Shift>>
            {
            Data = shifts ,
            ResponseCode = HttpStatusCode.OK ,
            TotalCount = totalShifts ,
            CurrentPage = shiftOptions.PageNumber ,
            PageSize = shiftOptions.PageSize ,
            HasPreviousPage = hasPrevious ,
            HasNextPage = hasNext ,
            };
        }

    public async Task<ApiResponseDto<Shift?>> GetShiftById(int id)
    {
        var result = await _dbContext
            .Shift.FirstOrDefaultAsync(s => s.Id == id); 

        if (result is null)
        {
            return new ApiResponseDto<Shift?>()
            {
                RequestFailed = true,
                Data = null,
                ResponseCode = HttpStatusCode.NotFound,
                ErrorMessage = $"Resource with ID: {id} was not found",
            };
        }

        return new ApiResponseDto<Shift?>() { Data = result, ResponseCode = HttpStatusCode.OK };
    }

    public async Task<ApiResponseDto<Shift?>> UpdateShift(int id, ShiftApiRequestDto updatedShift)
    {
        Shift? savedShift = await _dbContext.Shift.FindAsync(id);

        if (savedShift is null)
        {
            return new ApiResponseDto<Shift?>()
            {
                RequestFailed = true,
                Data = null,
                ResponseCode = HttpStatusCode.NotFound,
                ErrorMessage = $"Resource with ID: {id} was not found",
            };
        }

        savedShift = _mapper.Map(updatedShift, savedShift); // Use Mapper to map DTO to Shift entity
        savedShift.Id = id; // Ensure the ID is set correctly

        await _dbContext.SaveChangesAsync();

        return new ApiResponseDto<Shift?>() { Data = savedShift, ResponseCode = HttpStatusCode.OK };
    }

    public async Task<ApiResponseDto<string?>> DeleteShift(int id)
    {
        Shift? savedShift = await _dbContext.Shift.FindAsync(id);

        if (savedShift == null)
        {
            return new ApiResponseDto<string?>()
            {
                RequestFailed = true,
                Data = null,
                ResponseCode = HttpStatusCode.NotFound,
                ErrorMessage = $"Resource with ID: {id} was not found",
            };
        }
        _dbContext.Shift.Remove(savedShift);

        await _dbContext.SaveChangesAsync();

        return new ApiResponseDto<string?>()
        {
            Data = null,
            ResponseCode = HttpStatusCode.NoContent,
        };
    }
}
