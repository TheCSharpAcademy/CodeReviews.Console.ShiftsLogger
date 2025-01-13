using UI.Models;

namespace UI.Interfaces;
internal interface IShiftService
{
    public Task<List<Shift>> GetAll();
    public Task<List<Shift>> GetAllByWorker(int workerId);
    public Task<Dictionary<int, string>> GetAllAsDictionary();
    public Task<Shift> GetShiftById(int id);
    public Task<Shift> GetLatestShift();
    public Task<bool> CreateShift(Shift shift);
    public Task<bool> UpdateShift(Shift shift);
    public Task<bool> DeleteShift(int id);
}
