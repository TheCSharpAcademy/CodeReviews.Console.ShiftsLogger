using HKHemanthSharma.ShiftsLogger.Model;
using HKHemanthSharma.ShiftsLogger.Repository;

namespace HKHemanthSharma.ShiftsLogger.Services
{
    public interface IWorkerService
    {
        public void GetAllWorkers();
        public void GetSingleWorker();
        public void CreateWorker();
        public void DeleteWorker();
        public void UpdateWorker();

    }
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository repository;
        private readonly UserInputs Uinp;
        public WorkerService(IWorkerRepository _repository, UserInputs ui)
        {
            repository = _repository;
            Uinp = ui;
        }
        public void CreateWorker()
        {
            string newWorker = Uinp.GetNewName().GetAwaiter().GetResult();
            ResponseDto<Worker> worker = repository.CreateWorker(newWorker).GetAwaiter().GetResult();
            UserInterface.ShowResponse(worker);
        }

        public void DeleteWorker()
        {
            Worker newWorker = Uinp.GetSelectWorker().GetAwaiter().GetResult();
            ResponseDto<Worker> worker = repository.DeleteWorker(newWorker.WorkerId).GetAwaiter().GetResult();
            UserInterface.ShowResponse(worker);
        }

        public void GetAllWorkers()
        {
            ResponseDto<List<Worker>> workers = repository.GetAllWorker().GetAwaiter().GetResult();
            UserInterface.ShowResponse(workers);
        }

        public void GetSingleWorker()
        {
            int WorkerId = Uinp.InputId();
            ResponseDto<List<Worker>> workers = repository.GetSingleWorker(WorkerId).GetAwaiter().GetResult();
            UserInterface.ShowResponse(workers);
        }

        public void UpdateWorker()
        {
            Worker newWorker = Uinp.GetSelectWorker().GetAwaiter().GetResult();
            ResponseDto<Worker> worker = repository.UpdateWorker(newWorker).GetAwaiter().GetResult();
            UserInterface.ShowResponse(worker);
        }
    }
}
