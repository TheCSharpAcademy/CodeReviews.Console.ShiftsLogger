using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace sadklouds.ShiftLogger.Services.ShiftLoggerService;
public class ShiftLoggerService : IShiftLoggerService
{
    private readonly IMapper _mapper;
    private readonly ShiftDataContext _context;

    public ShiftLoggerService(IMapper mapper, ShiftDataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<List<GetShiftDto>> AddShift(AddShiftDto newShift)
    {
        var serviceResponse = new List<GetShiftDto>();
        var shift = _mapper.Map<Shift>(newShift);

        shift.Duration = newShift.ShiftEnd - newShift.ShiftStart;
        shift.CreationDate = DateTime.Now;

        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();

        var dbShifts = await _context.Shifts.ToListAsync();
        serviceResponse = dbShifts.Select(s => _mapper.Map<GetShiftDto>(s)).ToList();
        return serviceResponse;
    }

    public async Task<List<GetShiftDto?>> DeleteShift(int id)
    {
        List<GetShiftDto?> serviceResponse = new();
        try
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
                return null;

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            var dbShifts = await _context.Shifts.ToListAsync();
            serviceResponse = dbShifts.Select(s => _mapper.Map<GetShiftDto>(s)).ToList();
          

        }
        catch (Exception ex)
        {
            throw;
        }
        return serviceResponse;
    }

    public async Task<GetShiftDto> GetShiftById(int id)
    {
        var serviceResponse = new GetShiftDto();
        try
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
                return null;

            serviceResponse = _mapper.Map<GetShiftDto>(shift);

        }
        catch (Exception ex)
        {
            throw;
        }
        return serviceResponse;

    }

    public async Task<List<GetShiftDto>> GetShifts()
    {
        var servResponse = new List<GetShiftDto>();
        var dbShifts = await _context.Shifts.ToListAsync();
        servResponse = dbShifts.Select(s => _mapper.Map<GetShiftDto>(s)).ToList();
        return servResponse;
    }

    public async Task<GetShiftDto> UpdateShift(int id, UpdateShiftDto updatedShift)
    {
        var serviceResponse = new GetShiftDto();
       
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
                return null;

            shift = _mapper.Map(updatedShift, shift);
            shift.Duration = updatedShift.ShiftEnd - updatedShift.ShiftStart;
            shift.LastUpdate = DateTime.Now;
            await _context.SaveChangesAsync();

            serviceResponse = _mapper.Map<GetShiftDto>(shift);
        
       
        return serviceResponse;
    }
}
