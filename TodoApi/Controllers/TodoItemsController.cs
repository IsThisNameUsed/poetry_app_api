#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/TodoItems")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {       
            _context = context;
            DataManager.SetController(this);
        }

        public TodoContext GetContext()
        {
            return _context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return await _context.PoemItem.Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        [HttpGet("page/{page}")]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItemPage(int page)
        {
            int skipValue = (page - 1) * 5;
            skipValue = Math.Max(skipValue, 0);
            return await _context.PoemItem.Select (x => ItemToDTO(x)).Skip(skipValue).Take(10).ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
        {
            var poemitem = await _context.PoemItem.FindAsync(id);

            if (poemitem == null)
            {
                return NotFound();
            }

            return ItemToDTO(poemitem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /* [HttpPut("{id}")]
         public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO)
         {
             if (id != todoItemDTO.Id)
             {
                 return BadRequest();
             }

             var todoItem = await _context.PoemItem.FindAsync(id);
             if (todoItem == null)
             {
                 return NotFound();
             }

             todoItem.Poem = todoItemDTO.Poem;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
             {
                 return NotFound();
             }

             return NoContent();
         }*/

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new PoemItem
            {
                Poem = todoItemDTO.Poem
            };
            todoItem.dateTime = DateTime.Now.ToString();

            _context.PoemItem.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, ItemToDTO(todoItem));
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }


        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.PoemItem.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.PoemItem.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.PoemItem.Any(e => e.Id == id);
        }

        public static TodoItemDTO ItemToDTO(PoemItem todoItem) =>
           new TodoItemDTO
           {
               Id = todoItem.Id,
               Poem = todoItem.Poem,
               dateTime = todoItem.dateTime
           };
    }
}
