using API.Models;

namespace API.Interfaces;

public interface IShiftService
{
    public List<Shift> GetAll();
    public List<Shift> GetAllByWorkerId(int workerId);
    public Shift? GetById(int id);
    public Shift? GetLatest();
    public Shift Add(Shift shift);
    public bool Update(Shift shift);
    public bool Delete(int id);
}
