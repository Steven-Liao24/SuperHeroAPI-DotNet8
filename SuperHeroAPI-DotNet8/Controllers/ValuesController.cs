using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet8.Data;
using SuperHeroAPI_DotNet8.Enitites;

namespace SuperHeroAPI_DotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();

            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHeroe(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if(hero is null)
                return NotFound("Hero not found.");

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
          
            return Ok(await _context.SuperHeroes.ToListAsync());
        }


        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updatedHero)
        {
            var dbhero = await _context.SuperHeroes.FindAsync(updatedHero.Id);
            if (dbhero is null)
                return NotFound("Hero not found.");

            dbhero.Name = updatedHero.Name;
            dbhero.FirstName = updatedHero.FirstName;
            dbhero.LastName = updatedHero.LastName;
            dbhero.Place = updatedHero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }


        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbhero = await _context.SuperHeroes.FindAsync(id);
            if (dbhero is null)
                return NotFound("Hero not found.");

            _context.SuperHeroes.Remove(dbhero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
