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
    public class UsersController : ControllerBase
    {
        private readonly WebAssign3Context _context;

        public UsersController(WebAssign3Context context)
        {
            _context = context;
        }

        // GET: Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }


        // GET: Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int? id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: Users/
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, new { status = 201, message = "User saved successfully" });
        }


        // PUT: Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        public async Task<IActionResult> EditUserById(int id, [Bind("UserId,Email,Password,Username,PurchaseHistory,ShippingAddress")] User user)
        {
            if (id != user.UserId)
            {
                return BadRequest(new { status = 400, message = "User ID mismatch" });
            }

            if (ModelState.IsValid)
            {
                if (!UserExists(user.UserId))
                {
                    return NotFound(new { status = 404, message = "User Not Found" });
                }
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            return Ok(new { status = 200, message = "User updated successfully" }); ;
        }


        // DELETE: User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {

            if (!UserExists(id))
            {
                return NotFound(new { status = 404, message = "User Not Found" });
            }
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return Ok(new { status = 200, message = "User deleted successfully" }); ;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
