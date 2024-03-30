using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assign03.Data;

namespace Assign03.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly WebAssign3Context _context;

        public CartsController(WebAssign3Context context)
        {
            _context = context;
        }

        // GET: Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetAllItemsFromCart()
        {
            return await _context.Carts.ToListAsync();
        }


        // GET: Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetItemFromCartById(int? id)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // POST: Carts/
        [HttpPost]
        public async Task<ActionResult<Cart>> AddItemInCart(Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemFromCartById), new { id = cart.CartId }, new { status = 201, message = "Cart saved successfully" });
        }


        // PUT: Carts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCartById(int id, [Bind("CartId,ProductId,Quantity,UserId")] Cart cart)
        {
            if (id != cart.CartId)
            {
                return BadRequest(new { status = 400, message = "Cart ID mismatch" });
            }

            if (ModelState.IsValid)
            {
                if (!CartExists(cart.CartId))
                {
                    return NotFound(new { status = 404, message = "Item in Cart Not Found" });
                }
                _context.Update(cart);
                await _context.SaveChangesAsync();
            }
            return Ok(new { status = 200, message = "Item in Cart updated successfully" }); ;
        }


        // DELETE: Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemFromCartById(int id)
        {
            if (!CartExists(id))
            {
                return NotFound(new { status = 404, message = "Item in Cart Not Found" });
            }
            var cart = await _context.Carts.FindAsync(id);
            
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }

            return Ok(new { status = 200, message = "Item in Cart deleted successfully" }); ;
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }

    }
}
