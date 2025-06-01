using API.Models;

namespace API.Services;

public interface IShiftRepository
{
    IQueryable<Shift> GetAll();

    Task Add(Shift shift);

    Task Update(Shift shift);

    Task Delete(int id);

    Task SaveChanges();
}