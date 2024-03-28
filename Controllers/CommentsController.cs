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
    public class CommentsController : Controller
    {
        private readonly WebAssign3Context _context;

        public CommentsController(WebAssign3Context context)
        {
            _context = context;
        }

        // GET: Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _context.Comments.ToListAsync();
        }

        // GET: Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int? id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // POST: Comments/
        [HttpPost]
        public async Task<ActionResult<Comment>> AddItemInComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCommentById), new { id = comment.CommentId }, new { status = 201, message = "Comment saved successfully" });
        }


        // PUT: Comments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCommentById(int id, [Bind("CommentId,ProductId,UserId,Rating,Images,Text")] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest(new { status = 400, message = "Comment ID mismatch" });
            }

            if (ModelState.IsValid)
            {
                if (!CommentExists(comment.CommentId))
                {
                    return NotFound(new { status = 404, message = "Comment Not Found" });
                }
                _context.Update(comment);
                await _context.SaveChangesAsync();
            }
            return Ok(new { status = 200, message = "Comment updated successfully" }); ;
        }


        // DELETE: Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemFromCommentById(int id)
        {
            if (!CommentExists(id))
            {
                return NotFound(new { status = 404, message = "Comment Not Found" });
            }
            var comment = await _context.Comments.FindAsync(id);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }

            return Ok(new { status = 200, message = "Comment deleted successfully" }); ;
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
    }
}
