using Microsoft.AspNetCore.Mvc;

namespace WokersAPI.Services.SuperHeroServices;

public class SuperHeroService : ISuperHeroService
{
    private readonly DataContext _context;
   
    public SuperHeroService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<SuperHero>> AddHero(SuperHero hero)
    {
        _context.SuperHeroes.Add(hero);
        await _context.SaveChangesAsync();

        return await _context.SuperHeroes.ToListAsync();
    }

    public async Task<List<SuperHero>> Delete(int id)
    {
        var dbHero = await _context.SuperHeroes.FindAsync(id);
        if (dbHero == null)
            return new List<SuperHero>();

        _context.SuperHeroes.Remove(dbHero);
        await _context.SaveChangesAsync();

        return await _context.SuperHeroes.ToListAsync();
    }

    public async Task<List<SuperHero>> GetAll()
    {
        return await _context.SuperHeroes.ToListAsync();
    }

    public async Task<SuperHero> Get(int id)
    {
        var hero = await _context.SuperHeroes.FindAsync(id);
        if (hero == null)
            return null;
        return hero;
    }

    public async Task<List<SuperHero>> UpdateHero(int id, SuperHero request)
    {
        var hero = await _context.SuperHeroes.FindAsync(id);
        if (hero == null)
            return null;

        hero.Name = request.Name;
        hero.FirstName = request.FirstName;
        hero.LastName = request.LastName;
        hero.Place = request.Place;

        return await _context.SuperHeroes.ToListAsync();
    }
}
