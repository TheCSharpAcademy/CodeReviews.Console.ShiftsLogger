namespace ShiftsLoggerUI.Services;

public interface IService<T>
{
    public Task<List<T>> GetAll();
    public Task<T> Find(int id);
    public Task<T> Create(T newObject);
    public Task<T> Update(int id, T updatedObject);
    public Task<string> Delete(int id);
}
