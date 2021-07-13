using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LORAPI.Models;
using Microsoft.AspNetCore.Cors;

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
        [HttpGet("Friends/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetFriendsList(int id)
        {
            Database db = new Database();
            List<User> friendsList = await db.UsersFriendsList(id);

            if (friendsList == null)
            {
                return NotFound();
            }

            return friendsList;
        }

        [HttpGet("PendingIn/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetPendingInList(int id)
        {
            Database db = new Database();
            List<User> friendsList = await db.UsersPendingInList(id);

            if (friendsList == null)
            {
                return NotFound();
            }
            return friendsList;
        }

        [HttpGet("PendingOut/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetPendingOutList(int id)
        {
            Database db = new Database();
            List<User> friendsList = await db.UsersPendingOutList(id);

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

        [HttpPost("{friendsname}")]
        public async Task<ActionResult<FriendsList>> AddFriend(string friendsName, FriendsList friendsList)
        {
            Database db = new Database();                 
            // User activeUser = await _context.Users.FindAsync(friendsList.UserID);

            if (db.CheckUsername(friendsName) > 0)
            {
                return BadRequest("User doesn't exist");
            }
            else
            {
                friendsList.FriendID = db.CheckUsername(friendsName);
                if (AsUserPendingExists(friendsList.UserID, friendsList.FriendID))
                {
                    return BadRequest("User already has a pending friend request from you");
                }
                else if (AsFriendPendingExists(friendsList.UserID, friendsList.FriendID))
                {
                    friendsList.StatusID = 1;
                    _context.FriendsLists.Add(friendsList);
                    await _context.SaveChangesAsync();
                }
                else if (AsFriendFriendsExists(friendsList.UserID, friendsList.FriendID) || AsUserFriendsExists(friendsList.UserID, friendsList.FriendID))
                {
                    return BadRequest("You are already friends");
                }
            }
            return CreatedAtAction("GetFriendsList", new { id = friendsList.UserFriendID }, friendsList);
        }

        // DELETE: api/FriendsLists/5
        [HttpDelete("{userid}/{friendid}")]
        public async Task<IActionResult> DeleteFriendsList(int userid, int friendid)
        {
            Database db = new Database();
            var friendsList = await _context.FriendsLists.FindAsync(db.deleteFriend(userid, friendid));
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

        private bool AsUserPendingExists(int id, int oppositeuserid)
        {
            // StatusID 2 = Pending, StatusID 1 = Friends.
            return _context.FriendsLists.Any(e => e.UserID == id && e.FriendID == oppositeuserid && e.StatusID == 2);
        }

        private bool AsFriendPendingExists(int id, int oppositeuserid)
        {
            return _context.FriendsLists.Any(e => e.FriendID == id && e.UserID == oppositeuserid && e.StatusID == 2);
        }

        private bool AsUserFriendsExists(int id, int oppositeuserid)
        {
            // StatusID 2 = Pending, StatusID 1 = Friends.
            return _context.FriendsLists.Any(e => e.UserID == id && e.FriendID == oppositeuserid && e.StatusID == 1);
        }

        private bool AsFriendFriendsExists(int id, int oppositeuserid)
        {
            return _context.FriendsLists.Any(e => e.FriendID == id && e.UserID == oppositeuserid && e.StatusID == 1);
        }

        private bool UsernameExists(string username)
        {
            return _context.Users.Any(e => e.Username == username);
        }
    }
}
