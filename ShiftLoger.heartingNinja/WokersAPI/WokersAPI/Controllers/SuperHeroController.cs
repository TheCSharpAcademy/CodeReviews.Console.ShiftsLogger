using Microsoft.AspNetCore.Mvc;
using WokersAPI.Services.SuperHeroServices;
namespace WokersAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuperHeroController : ControllerBase
{
    private readonly ISuperHeroService _superHeroService;

    public SuperHeroController(ISuperHeroService superHeroService)
    {
        _superHeroService = superHeroService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> GetAll()
    {
        return await _superHeroService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SuperHero>> Get(int id)
    {
        var results = await _superHeroService.Get(id);
        if (results is null)
            return NotFound();
        return Ok(results);
    }

    [HttpPost]
    public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
    {
        var results = await _superHeroService.AddHero(hero);
        return results;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<List<SuperHero>>> UpdateHero(int id, SuperHero response)
    {
        var results = await _superHeroService.UpdateHero(id ,response);
        if (results is null)
            return NotFound();
        return Ok(results);
    }   

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<SuperHero>>> Delete(int id)
    {
        var results = await _superHeroService.Delete(id);
        if (results is null)
            return NotFound();
        return Ok(results);
      
    }
}
