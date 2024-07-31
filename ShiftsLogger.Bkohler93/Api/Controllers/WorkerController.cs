using Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations.Rules;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerController: ControllerBase {
    private readonly AppDbContext db;
    public WorkerController(AppDbContext appDbContext)
    {
        db = appDbContext; 
    }    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Worker>>> GetWorkers()
    {
        return await db.Workers.ToListAsync();
    }
}