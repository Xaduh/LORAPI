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
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LORContext _context;

        public UsersController(LORContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]       // Works
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]   // Works
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            User user = await _context.Users.FindAsync(id);

            UserDTO safeUser = new UserDTO();
            safeUser.Username = user.Username;
            safeUser.Email = user.Email;

            if (user == null)
            {
                return NotFound();
            }

            return safeUser;
        }

        //[HttpGet("{username}/{password}")]
        //public async Task<ActionResult<User>> GetLogin(string username, string password)
        //{
        //    Database db = new Database();
        //    // SALT password
        //    var user = await _context.Users.FindAsync(db.UserLogin(username, Convert.ToBase64String(SALT.GenerateSalt(password))));

        //    if (user == null)
        //    {
        //        return BadRequest();
        //    }

        //    return user;
        //}

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Update, username skal checkes efter dupletter. Der skal laves SALT på password.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            Database db = new Database();
            User activeUser = db.OldUserInfo(id);
            if(user.Username == "" || user.Username == null)
            {
                user.Username = activeUser.Username;
            }
            if (user.Password == "" || user.Password == null)
            {
                user.Password = activeUser.Password;
            }
            else
            {
                user.Password = Convert.ToBase64String(SALT.GenerateSalt(user.Password));
            }
            if (user.Email == "" || user.Email == null)
            {
                user.Email = activeUser.Email;
            }
            user.Role = activeUser.Role;
            user.UserID = id;
            
            if (!UsernameExists(user.Username) || activeUser.Username == user.Username)   
            {                
                _context.Entry(user).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(id))
                    {
                        return NotFound();                        
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return BadRequest("Username already exists");
            }            
            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Create, username skal checke efter dupletter. Der skal laves SALT på password.
        [HttpPost]  
        public async Task<ActionResult<User>> PostUser(User user)
        {
            User newUser = user;
            if (user.Username == "" || user.Password == "" || user.Email == "" || user.Role == "")
            {
                // Forbidden.
                return StatusCode(403);
            }
            else if (UsernameExists(user.Username))
            {
                return BadRequest();
            }
            else
            {
                newUser.Password = Convert.ToBase64String(SALT.GenerateSalt(newUser.Password));
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = newUser.UserID }, newUser);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<int>> Login(User user)
        {
            Database db = new Database();
            // SALT password
            var foundUser = await _context.Users.FindAsync(db.UserLogin(user.Username, Convert.ToBase64String(SALT.GenerateSalt(user.Password))));

            if (foundUser == null)
            {
                return NotFound();
            }

            return foundUser.UserID.Value;
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]    // Works
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }

        private bool UsernameExists(string username)
        {
            return _context.Users.Any(e => e.Username == username);
        }
    }
}
