using AutoMapper;

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
    public async Task<ServiceResponse<List<GetShiftDto>>> AddShift(AddShiftDto newShift)
    {
        var serviceResponse = new ServiceResponse<List<GetShiftDto>>();
        var shift = _mapper.Map<Shift>(newShift);

        shift.Duration = newShift.ShiftEnd - newShift.ShiftStart;
        shift.CreationDate = DateTime.Now;

        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();

        var dbShifts = await _context.Shifts.ToListAsync();
        serviceResponse.Data = dbShifts.Select(s => _mapper.Map<GetShiftDto>(s)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetShiftDto>>> DeleteShift(int id)
    {
        var serviceResponse = new ServiceResponse<List<GetShiftDto>>();
        try
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
                throw new Exception($"Shift Id'{id}' not found");

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            var dbShifts = await _context.Shifts.ToListAsync();
            serviceResponse.Data = dbShifts.Select(s => _mapper.Map<GetShiftDto>(s)).ToList();
            serviceResponse.Message = "Shift successfully deleted";

        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetShiftDto>> GetShiftById(int id)
    {
        var serviceResponse = new ServiceResponse<GetShiftDto>();
        try
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
                throw new Exception($"Shift Id'{id}' not found");

            serviceResponse.Data = _mapper.Map<GetShiftDto>(shift);


        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;

    }

    public async Task<ServiceResponse<List<GetShiftDto>>> GetShifts()
    {
        var servResponse = new ServiceResponse<List<GetShiftDto>>();
        var dbShifts = await _context.Shifts.ToListAsync();
        servResponse.Data = dbShifts.Select(s => _mapper.Map<GetShiftDto>(s)).ToList();
        return servResponse;
    }

    public async Task<ServiceResponse<GetShiftDto>> UpdateShift(UpdateShiftDto updatedShift)
    {
        var serviceResponse = new ServiceResponse<GetShiftDto>();
        try
        {
            var shift = await _context.Shifts.FindAsync(updatedShift.Id);
            if (shift == null)
                throw new Exception($"shift Id'{updatedShift.Id} not found");

            shift = _mapper.Map(updatedShift, shift);
            shift.Duration = updatedShift.ShiftEnd - updatedShift.ShiftStart;
            shift.LastUpdate = DateTime.Now;
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetShiftDto>(shift);
            serviceResponse.Message = "Shift updated successfully";
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }
}
