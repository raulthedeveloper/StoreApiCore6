#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApiCore.Models;

namespace StoreApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoursController : ControllerBase
    {
        private readonly StoreContext _context;

        public HoursController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/Hours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoursModel>>> Gethours()
        {
            return await _context.hours.ToListAsync();
        }

        // GET: api/Hours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HoursModel>> GetHoursModel(int id)
        {
            var hoursModel = await _context.hours.FindAsync(id);

            if (hoursModel == null)
            {
                return NotFound();
            }

            return hoursModel;
        }

        // PUT: api/Hours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHoursModel(int id, HoursModel hoursModel)
        {
            if (id != hoursModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(hoursModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoursModelExists(id))
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

        // POST: api/Hours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HoursModel>> PostHoursModel(HoursModel hoursModel)
        {
            _context.hours.Add(hoursModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHoursModel", new { id = hoursModel.Id }, hoursModel);
        }

        // DELETE: api/Hours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoursModel(int id)
        {
            var hoursModel = await _context.hours.FindAsync(id);
            if (hoursModel == null)
            {
                return NotFound();
            }

            _context.hours.Remove(hoursModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HoursModelExists(int id)
        {
            return _context.hours.Any(e => e.Id == id);
        }
    }
}
