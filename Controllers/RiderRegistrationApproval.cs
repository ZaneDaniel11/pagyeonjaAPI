using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pagyeonjaAPI.Entities;

namespace pagyeonjaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagyeonjaAPI : ControllerBase
    {
        private readonly HitchContext _context;

        public PagyeonjaAPI(HitchContext context)
        {
            _context = context;
        }

        // GET: api/Riders
        [HttpGet("GetRiders")]
        public async Task<ActionResult<IEnumerable<Rider>>> GetRiders()
        {
            if (_context.Riders == null)
            {
                return NotFound();
            }
            return await _context.Riders.OrderByDescending(a => a.Id).ToListAsync();
        }

        // GET: api/Rider/5
        [HttpGet("GetRider")]
        public async Task<ActionResult<Rider>> GetRider(int id)
        {
            if (_context.Riders == null)
            {
                return NotFound();
            }
            var Rider = await _context.Riders.FindAsync(id);

            if (Rider == null)
            {
                return NotFound();
            }

            return Rider;
        }

        // PUT: api/Rider/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateRider")]
        public async Task<IActionResult> PutRider(int id, Rider Rider)
        {
            if (id != Rider.Id)
            {
                return BadRequest();
            }

            _context.Entry(Rider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiderExists(id))
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

        // POST: api/Rider
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("RegisterRider")]
        public async Task<ActionResult<Rider>> PostRider(Rider Rider)
        {
            Console.WriteLine(Rider);
            if (_context.Riders == null)
            {
                return Problem("Entity set 'GradTrackerContext.Riders'  is null.");
            }


            _context.Riders.Add(Rider);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRider", new { id = Rider.Id }, Rider);
        }

        //   // [HttpPost]
        // public IActionResult PostEmploymentHistory(EmploymentHistory employmentHistory)
        // {
        // if (!RiderExists(employmentHistory.RidersId))
        // {
        //     return NotFound("Rider with the provided RidersId does not exist.");
        // }

        // _context.EmploymentHistories.Add(employmentHistory);
        // _context.SaveChanges();

        // return CreatedAtAction("GetEmploymentHistory", new { id = employmentHistory.Id }, employmentHistory);
        // }

        // DELETE: api/Rider/5
        [HttpDelete("DeleteRider")]
        public async Task<IActionResult> DeleteRider(int id)
        {
            if (_context.Riders == null)
            {
                return NotFound();
            }
            var Rider = await _context.Riders.FindAsync(id);
            if (Rider == null)
            {
                return NotFound();
            }

            _context.Riders.Remove(Rider);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RiderExists(int id)
        {
            return (_context.Riders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}