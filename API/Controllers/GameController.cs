using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController(AppDbContext context) : ControllerBase
    {
        // GET: api/Game
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> Get()
        {
            return await context.Games.ToListAsync();
        }

        // GET: api/Game/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> Get(int id)
        {
            var game = await context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // PUT: api/Game/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            context.Entry(game).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Game
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> Post(Game game)
        {
            context.Games.Add(game);
            await context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = game.Id }, game);
        }

        // DELETE: api/Game/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            context.Games.Remove(game);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool Exists(int id)
        {
            return context.Games.Any(e => e.Id == id);
        }
    }
}
