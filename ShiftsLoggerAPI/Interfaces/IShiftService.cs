using SharedLibrary.DTOs;

namespace ShiftsLoggerAPI.Interfaces;

public interface IShiftService
{
    public List<ShiftDto> GetAllShifts();
    public ShiftDto GetShift(int id);
    public CreateShiftDto CreateShift(CreateShiftDto shift);
    public UpdateShiftDto UpdateShift(UpdateShiftDto shift);
    public bool DeleteShift(ShiftDto shift);
}

