using API.Models;

namespace API.Interfaces;

public interface IWorkerService
{
    public List<Worker> GetAll();
    public Worker? GetById(int id);
    public Worker Add(Worker worker);
    public bool Update(Worker worker);
    public bool Delete(int workerId);
}
