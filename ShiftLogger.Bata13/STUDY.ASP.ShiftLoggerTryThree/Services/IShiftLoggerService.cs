using STUDY.ASP.ShiftLoggerTryThree.Models;

namespace STUDY.ASP.ShiftLoggerTryThree.Services
{
    public interface IShiftLoggerService
    {
        Task<List<ShiftLogger>> GetAllShiftLogs();
        Task<ShiftLogger?> GetSingleShiftLog(int id);
        Task<List<ShiftLogger>> AddShift(ShiftLogger shift);
        Task<List<ShiftLogger>?> UpdateShift(int id, ShiftLogger request);
        Task<List<ShiftLogger>?> DeleteShift(int id);
    }
}
