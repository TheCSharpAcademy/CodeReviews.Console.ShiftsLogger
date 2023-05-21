using Microsoft.AspNetCore.Mvc;

namespace WokersAPI.Services.SuperHeroServices;

public interface ISuperHeroService
{
    Task<List<SuperHero>> GetAll();
    Task<SuperHero> Get(int id);
    Task<List<SuperHero>> AddHero(SuperHero hero);
    Task<List<SuperHero>>? UpdateHero(int id, SuperHero request);
    Task<List<SuperHero>> Delete(int id);
}
