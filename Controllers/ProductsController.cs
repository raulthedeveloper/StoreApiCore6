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
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
        }


        [HttpGet("ProductByCategory/{id}")]
        public ActionResult GetProductsByCategory(int id)
        {
            return Ok(_context.products.Where(x => x.catId == id).ToList());
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> Getproducts()
        {
            return await _context.products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var products = await _context.products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, Products products)
        {
            if (id != products.id)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts(Products products)
        {
            _context.products.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = products.id }, products);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            var products = await _context.products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.products.Remove(products);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("product_with_category")]
        public IQueryable<Products> GetProductWithCat()
        {
            return _context.products.Include(e => e.category);
        }


        [HttpGet("ProductsPage/{catId}/{currentPage}")]

        public ActionResult GetProductsPage(int catId, int currentPage)
        {
            // Improve Pagination for category pages
            int pageSize = 3;

            List<Products> products = _context.products.Where(e => e.catId == catId).OrderBy(e => e.id).Skip(pageSize * currentPage).Take(pageSize).ToList();

            if (products.Count() < pageSize)
            {
                products.Count();

                return Ok(_context.products.Where(e => e.catId == catId).OrderBy(e => e.id).ToList().Last());
            }
            else
            {
                products.Count();
                return Ok(products);
            }


        }

        private bool ProductsExists(int id)
        {
            return _context.products.Any(e => e.id == id);
        }
    }
}
