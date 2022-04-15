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
    public class UnitedStatesController : ControllerBase
    {
        private readonly StoreContext _context;

        public UnitedStatesController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/UnitedStates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitedStates>>> GetUnitedStates()
        {
            return await _context.UnitedStates.ToListAsync();
        }

        // GET: api/UnitedStates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitedStates>> GetUnitedStates(int id)
        {
            var unitedStates = await _context.UnitedStates.FindAsync(id);

            if (unitedStates == null)
            {
                return NotFound();
            }

            return unitedStates;
        }

        // PUT: api/UnitedStates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnitedStates(int id, UnitedStates unitedStates)
        {
            if (id != unitedStates.Id)
            {
                return BadRequest();
            }

            _context.Entry(unitedStates).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitedStatesExists(id))
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

        // POST: api/UnitedStates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnitedStates>> PostUnitedStates(UnitedStates unitedStates)
        {
            _context.UnitedStates.Add(unitedStates);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnitedStates", new { id = unitedStates.Id }, unitedStates);
        }

        // DELETE: api/UnitedStates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitedStates(int id)
        {
            var unitedStates = await _context.UnitedStates.FindAsync(id);
            if (unitedStates == null)
            {
                return NotFound();
            }

            _context.UnitedStates.Remove(unitedStates);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnitedStatesExists(int id)
        {
            return _context.UnitedStates.Any(e => e.Id == id);
        }
    }
}
