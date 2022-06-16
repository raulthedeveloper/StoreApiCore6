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
    public class CartsController : ControllerBase
    {

        private readonly DateTime date = DateTime.Now;

        private readonly StoreContext _context;

        public CartsController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> Getcart()
        {
            return await _context.cart.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await _context.cart.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, Cart cart)
        {
            if (id != cart.id)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Cart>> PostCart(Cart cart)
        //{
        //    _context.cart.Add(cart);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCart", new { id = cart.id }, cart);
        //}

        [HttpPost]
        public ActionResult PostCart(Cart[] cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            Random rnd = new Random();
            int number = rnd.Next(1000000, 3000000);
            string n = this.date.ToString("yyyyMMddHH");

            foreach (Cart item in cart)
            {
                item.cartId = item.custId + n + number;

                item.date = this.date;

                _context.cart.Add(item);
                _context.SaveChanges();


            }

            return StatusCode(201);



        }

        [HttpGet("customer_cart/{id}")]
        public  IQueryable<object> CustomerCart(int id)
        {
            var query = from customer in _context.customer
                        join cart in _context.cart on customer.id equals cart.id
                        where customer.id == id
                        select new { Customer = customer, Cart = cart };


            return query;
        }



        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.cart.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return _context.cart.Any(e => e.id == id);
        }
    }
}
