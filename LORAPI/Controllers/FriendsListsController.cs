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
    [Route("api/FriendsList")]
    [ApiController]
    public class FriendsListsController : ControllerBase
    {
        private readonly LORContext _context;

        public FriendsListsController(LORContext context)
        {
            _context = context;
        }

        // GET: api/FriendsLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FriendsList>>> GetFriendsLists()
        {
            return await _context.FriendsLists.ToListAsync();
        }

        // GET: api/FriendsLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FriendsList>> GetFriendsList(int id)
        {
            var friendsList = await _context.FriendsLists.FindAsync(id);

            if (friendsList == null)
            {
                return NotFound();
            }

            return friendsList;
        }

        // PUT: api/FriendsLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriendsList(int id, FriendsList friendsList)
        {
            if (id != friendsList.UserFriendID)
            {
                return BadRequest();
            }

            _context.Entry(friendsList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendsListExists(id))
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

        // POST: api/FriendsLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FriendsList>> PostFriendsList(FriendsList friendsList)
        {
            _context.FriendsLists.Add(friendsList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriendsList", new { id = friendsList.UserFriendID }, friendsList);
        }

        // DELETE: api/FriendsLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriendsList(int id)
        {
            var friendsList = await _context.FriendsLists.FindAsync(id);
            if (friendsList == null)
            {
                return NotFound();
            }

            _context.FriendsLists.Remove(friendsList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FriendsListExists(int id)
        {
            return _context.FriendsLists.Any(e => e.UserFriendID == id);
        }
    }
}
