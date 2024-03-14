using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkerController : Controller
    {
        private readonly WorkerContext _context;

        public WorkerController(WorkerContext context)
        {
            _context = context;
        }
        //patch para edições pontuais, pouca coisa, nao tudo
        [HttpPatch]
        [Route("/start")]
        public void BeginWorking([FromBody] string name)
        {
            var worker = _context.Worker.FirstOrDefault(x => x.Name == name);
            worker.Begin = DateTime.Now;
            _context.SaveChanges();
        }

        [HttpPatch]
        [Route("/end")]
        public void EndWorking([FromBody] string name)
        {
            var worker = _context.Worker.FirstOrDefault(x => x.Name == name);
            worker.End = DateTime.Now;
            worker.WorkedHours = Services.CalculateTime(worker);
            worker.TotalHours += worker.WorkedHours;
            _context.SaveChanges();
        }

        [HttpPost]
        [Route("/create")]
        public void Create([FromBody] string name)
        {
            var worker = new Worker() { Name = name };
            _context.Worker.Add(worker);
            _context.SaveChanges();
        }

        [HttpGet]
        [Route("/getall")]
        public IEnumerable<Worker> GetAllWorkers()
        {
            return _context.Worker.ToList();
        }

        [HttpGet]
        [Route("/get/{name}")]
        public ActionResult<Worker> GetWorker(string name)
        {
            var worker = _context.Worker.FirstOrDefault(x => x.Name == name);

            if (worker == null)
            {
                return NotFound();
            }

            return worker;
        }
    }
}
