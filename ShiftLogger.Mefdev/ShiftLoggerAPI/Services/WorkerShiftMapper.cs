using ShiftLogger.Mefdev.ShiftLoggerAPI.Models;

namespace ShiftLogger.Mefdev.ShiftLoggerAPI.Services;

public class WorkerShiftMapper
{
    public WorkerShiftDto ShiftToDTO(WorkerShift shift) 
            =>  new WorkerShiftDto(shift.Id,shift.EmployeeName, shift.StartDate, shift.EndDate, CalculateShiftDuration(shift.StartDate, shift.EndDate));
                
    public List<WorkerShiftDto> ShiftsToDTO(List<WorkerShift> shifts)
    {
        List<WorkerShiftDto> workerShiftDtos = new List<WorkerShiftDto>();
        if(shifts is null)
        {
            return null;
        }
        foreach(var shift in shifts)
        {
            var shiftDto = ShiftToDTO(shift);
            workerShiftDtos.Add(shiftDto);
        }
        return workerShiftDtos;  
    }

    public WorkerShift DTOtoShift(WorkerShiftDto shift)
            => new WorkerShift() { Id=shift.Id, EmployeeName=shift.Name, StartDate=shift.Start, EndDate=shift.End};

    private TimeSpan CalculateShiftDuration(DateTime start, DateTime end)
    {
        if (end >= start)
        {
            return end - start;
        }
        else
        {
            throw new InvalidOperationException("End date must be after or equal to the start date.");
        }
    }
}