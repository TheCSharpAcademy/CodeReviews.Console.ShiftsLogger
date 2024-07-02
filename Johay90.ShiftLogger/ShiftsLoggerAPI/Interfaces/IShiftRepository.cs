using SharedLibrary.Models;

namespace ShiftsLoggerAPI.Interfaces;

public interface IShiftRepository
{
    public List<Shift> GetAll();
    public Shift GetById(int id);
    public void Create(Shift shift);
    public void Update(Shift shift);
    public void Delete(Shift shift);
}

