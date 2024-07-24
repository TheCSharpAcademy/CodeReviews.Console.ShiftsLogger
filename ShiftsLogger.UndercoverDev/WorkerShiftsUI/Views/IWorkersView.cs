using WorkerShiftsUI.Models;

namespace WorkerShiftsUI.Views;
public interface IWorkersView
{
    public Task WorkersMenu();
    public void ShowWorkers(List<Worker> workers);
    public void ShowWorker(Worker worker);
}