using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DRMusic.Models;

namespace DRMusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicsController : ControllerBase
    {
        private readonly MusicContext _context;

        public MusicsController(MusicContext context)
        {
            _context = context;
        }

        // GET: api/Musics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Music>>> GetMusics()
        {
            return await _context.Musics.ToListAsync();
        }

        // GET: api/Musics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Music>> GetMusic(long id)
        {
            var music = await _context.Musics.FindAsync(id);

            if (music == null)
            {
                return NotFound();
            }

            return music;
        }

        // PUT: api/Musics/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMusic(long id, Music music)
        {
            if (id != music.Id)
            {
                return BadRequest();
            }

            _context.Entry(music).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusicExists(id))
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

        // POST: api/Musics
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Music>> PostMusic(Music music)
        {
            _context.Musics.Add(music);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMusic", new { id = music.Id }, music);
        }

        // DELETE: api/Musics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Music>> DeleteMusic(long id)
        {
            var music = await _context.Musics.FindAsync(id);
            if (music == null)
            {
                return NotFound();
            }

            _context.Musics.Remove(music);
            await _context.SaveChangesAsync();

            return music;
        }

        private bool MusicExists(long id)
        {
            return _context.Musics.Any(e => e.Id == id);
        }
    }
}
