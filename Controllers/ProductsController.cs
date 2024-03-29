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
    public class ProductsController : ControllerBase
    {
        private readonly WebAssign3Context _context;

        public ProductsController(WebAssign3Context context)
        {
            _context = context;
        }

        // GET: Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int? id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        // POST: Products/
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, new { status = 201, message = "Product saved successfully" });
        }


        // PUT: Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProductById(int id, [Bind("ProductId,Description,Image,Pricing,ShippingCost")] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest(new { status = 400, message = "Product ID mismatch" });
            }
            if (ModelState.IsValid)
            {
                if (!ProductExists(product.ProductId))
                {
                    return NotFound(new { status = 404, message = "Product Not Found" });
                }
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            return Ok(new { status = 200, message = "Product updated successfully" }); ;
        }


        // DELETE: Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            if (!ProductExists(id))
            {
                return NotFound(new { status = 404, message = "Product Not Found" });
            }
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return Ok(new { status = 200, message = "Product deleted successfully" }); ;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
