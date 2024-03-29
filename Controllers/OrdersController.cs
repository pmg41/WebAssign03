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
    public class OrdersController : ControllerBase
    {
        private readonly WebAssign3Context _context;

        public OrdersController(WebAssign3Context context)
        {
            _context = context;
        }

        // GET: Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllItemsFromOrder()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetItemFromOrderById(int? id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        // POST: Orders/
        [HttpPost]
        public async Task<ActionResult<Order>> AddItemInOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItemFromOrderById), new { id = order.OrderId }, new { status = 201, message = "Order saved successfully" });
        }


        // PUT: Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrderById(int id, [Bind("OrderId,UserId,ProductId,Quantity,OrderDate")] Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest(new { status = 400, message = "Order ID mismatch" });
            }
            if (ModelState.IsValid)
            {
                if (!OrderExists(order.OrderId))
                {
                    return NotFound(new { status = 404, message = "Item in Order Not Found" });
                }
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            return Ok(new { status = 200, message = "Item in Order updated successfully" }); ;
        }


        // DELETE: Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemFromOrderById(int id)
        {

            if (!OrderExists(id))
            {
                return NotFound(new { status = 404, message = "Item in Order Not Found" });
            }
            var order = await _context.Orders.FindAsync(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }

            return Ok(new { status = 200, message = "Item in Order deleted successfully" }); ;
        }


        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
