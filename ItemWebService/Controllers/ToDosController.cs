using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemWebService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItemWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDosController(ToDoContext context)
        {
            _context = context;

            if (_context.ToDoItems.Count() == 0)
            {
                _context.ToDoItems.Add(new ToDoItem {Name = "item 1"});
                _context.SaveChanges();
            }
        }

        // Get api/todo

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetTodoItems()

        {
            return await _context.ToDoItems.ToListAsync();
        }

        // GET api/todo/3

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.ToDoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
    }
}