using Microsoft.AspNetCore.Mvc;
using Shiftlogger.DTOs;
using Shiftlogger.Entities;
using Shiftlogger.Repositories.Interfaces;


namespace Shiftlogger.javedkhan2k2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerRepository _workerRepository;

        public WorkerController(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Worker>> GetWorkers()
        {
            return Ok(_workerRepository.GetAllWorkers());
        }

        [HttpGet("{id}")]
        public ActionResult<Worker> GetWorker(int id)
        {
            var worker = _workerRepository.GetWorkerById(id);
            if (worker == null)
            {
                return NotFound();
            }
            return Ok(worker);
        }

        [HttpPost]
        public ActionResult<Worker> PostWorker(Worker worker)
        {
            _workerRepository.AddWorker(worker);
            return CreatedAtAction(nameof(GetWorker), new { id = worker.Id }, worker);
        }

        [HttpPut("{id}")]
        public ActionResult<Worker> PutWorker(int id, WorkerPutDto worker)
        {
            if (id != worker.Id)
            {
                return BadRequest("Worker ID mismatch");
            }
            var existingWorker = _workerRepository.GetWorkerById(id);
            if (existingWorker == null)
            {
                return NotFound();
            }
            _workerRepository.UpdateWorker(worker.ToWorker());
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Worker> DeleteWorker(int id)
        {
            var worker = _workerRepository.GetWorkerById(id);
            if (worker == null)
            {
                return NotFound();
            }
            _workerRepository.DeleteWorker(worker);
            return NoContent();
        }

    }
    
}
