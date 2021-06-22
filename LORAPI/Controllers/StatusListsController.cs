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
    [Route("api/StatusLists")] // Skal ikke bruges? hvis vi i clientside kode aligevel bare indsætter id. & når der skal tjekkes status, gøres det via Database klassen.
    [ApiController]
    public class StatusListsController : ControllerBase
    {
        private readonly LORContext _context;

        public StatusListsController(LORContext context)
        {
            _context = context;
        }

        // GET: api/StatusLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusList>>> GetStatus()
        {
            return await _context.Status.ToListAsync();
        }

        // GET: api/StatusLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusList>> GetStatusList(int id)
        {
            var statusList = await _context.Status.FindAsync(id);

            if (statusList == null)
            {
                return NotFound();
            }

            return statusList;
        }

        // PUT: api/StatusLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusList(int id, StatusList statusList)
        {
            if (id != statusList.StatusID)
            {
                return BadRequest();
            }

            _context.Entry(statusList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusListExists(id))
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

        // POST: api/StatusLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatusList>> PostStatusList(StatusList statusList)
        {
            _context.Status.Add(statusList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatusList", new { id = statusList.StatusID }, statusList);
        }

        // DELETE: api/StatusLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusList(int id)
        {
            var statusList = await _context.Status.FindAsync(id);
            if (statusList == null)
            {
                return NotFound();
            }

            _context.Status.Remove(statusList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusListExists(int id)
        {
            return _context.Status.Any(e => e.StatusID == id);
        }
    }
}
