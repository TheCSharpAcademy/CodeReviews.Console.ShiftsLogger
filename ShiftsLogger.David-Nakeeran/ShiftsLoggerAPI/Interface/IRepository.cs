namespace ShiftsLoggerAPI.Interface;

public interface IRepository<T> where T : class
{
    public Task<bool> DeleteAsync(long id);
}