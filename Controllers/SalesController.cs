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
    public class SalesController : ControllerBase
    {
        private readonly StoreContext _context;

        public SalesController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sales>>> Getsales()
        {
            return await _context.sales.ToListAsync();
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sales>> GetSales(int id)
        {
            var sales = await _context.sales.FindAsync(id);

            if (sales == null)
            {
                return NotFound();
            }

            return sales;
        }

        // PUT: api/Sales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSales(int id, Sales sales)
        {
            if (id != sales.id)
            {
                return BadRequest();
            }

            _context.Entry(sales).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesExists(id))
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

        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sales>> PostSales(Sales sales)
        {
            _context.sales.Add(sales);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSales", new { id = sales.id }, sales);
        }

        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSales(int id)
        {
            var sales = await _context.sales.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }

            _context.sales.Remove(sales);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalesExists(int id)
        {
            return _context.sales.Any(e => e.id == id);
        }
    }
}
