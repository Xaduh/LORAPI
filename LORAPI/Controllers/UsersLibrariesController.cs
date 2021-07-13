using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LORAPI.Models;

namespace LORAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersLibrariesController : ControllerBase
    {
        private readonly LORContext _context;

        public UsersLibrariesController(LORContext context)
        {
            _context = context;
        }

        // GET: api/UsersLibraries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersLibrary>>> GetUsersLibrarys()
        {
            return await _context.UsersLibrarys.ToListAsync();
        }

        // GET: api/UsersLibraries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersLibrary>> GetUsersLibrary(int id)
        {
            var usersLibrary = await _context.UsersLibrarys.FindAsync(id);

            if (usersLibrary == null)
            {
                return NotFound();
            }

            return usersLibrary;
        }        

        // PUT: api/UsersLibraries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersLibrary(int id, UsersLibrary usersLibrary)
        {
            if (id != usersLibrary.UserGameID)
            {
                return BadRequest();
            }
            _context.Entry(usersLibrary).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersLibraryExists(id))
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

        // POST: api/UsersLibraries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsersLibrary>> PostUsersLibrary(UsersLibrary usersLibrary)
        {
            // Jeg skal lave et check om den instans eksistere. Hermed give en response tilbage.
            if (!UserGameExists(usersLibrary.UserID, usersLibrary.GameID))
            {
                _context.UsersLibrarys.Add(usersLibrary);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUsersLibrary", new { id = usersLibrary.UserGameID }, usersLibrary);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Owned")]
        public async Task<ActionResult> OwnedGame(UsersLibrary usersLibrary)
        {
            if (!UserGameExists(usersLibrary.UserID, usersLibrary.GameID))
            {
                return NotFound();
            }
            return Ok();
        }

        // Skal lave en Post, som kører på UserID & GameID. Den skal fungere som en GET.

        // DELETE: api/UsersLibraries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersLibrary(int id)
        {
            var usersLibrary = await _context.UsersLibrarys.FindAsync(id);
            if (usersLibrary == null)
            {
                return NotFound();
            }

            _context.UsersLibrarys.Remove(usersLibrary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersLibraryExists(int id)
        {
            return _context.UsersLibrarys.Any(e => e.UserGameID == id);
        }

        private bool UserGameExists(int userid, int gameid)
        {
            return _context.UsersLibrarys.Any(e => e.UserID == userid && e.GameID == gameid);
        }
    }
}
