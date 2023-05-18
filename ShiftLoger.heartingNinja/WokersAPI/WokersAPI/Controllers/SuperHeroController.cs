using Microsoft.AspNetCore.Mvc;

namespace WokersAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuperHeroController : ControllerBase
{
    private readonly DataContext context;

    public SuperHeroController(DataContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> Get()
    {
        return Ok(await context.SuperHeroes.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SuperHero>> Get(int id)
    {
        var hero = await context.SuperHeroes.FindAsync(id);
        if (hero == null)
            return BadRequest("Hero not found.");
        return Ok(hero);
    }

    [HttpPost]
    public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
    {
        context.SuperHeroes.Add(hero);
        await context.SaveChangesAsync();

        return Ok(await context.SuperHeroes.ToListAsync());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHero(int id, [FromBody] SuperHero hero)
    {
        if (hero == null || hero.Id != id)
        {
            return BadRequest("hero not updated");
        }

        var dbHero = await context.SuperHeroes.FindAsync(id);
        if (dbHero == null)
        {
            return NotFound();
        }

        dbHero.Name = hero.Name;
        dbHero.FirstName = hero.FirstName;
        dbHero.LastName = hero.LastName;
        dbHero.Place = hero.Place;

        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<SuperHero>>> Delete(int id)
    {
        var dbHero = await context.SuperHeroes.FindAsync(id);
        if (dbHero == null)
            return BadRequest("Hero not found.");

        context.SuperHeroes.Remove(dbHero);
        await context.SaveChangesAsync();

        return Ok(await context.SuperHeroes.ToListAsync());
    }
}
