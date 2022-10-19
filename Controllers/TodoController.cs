using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using Microsoft.EntityFrameworkCore;
using TodoApp.ViewModels;
using TodoApp.Domain;
using System.Collections.Generic;
using System.Linq;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("todo")]
    public class TodoController : ControllerBase
    {
        private readonly TodoAppDbContext _context;

        public TodoController(TodoAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var todos = await _context.Todos.AsNoTracking().ToListAsync();
            return Ok(new ResultViewModel<List<Todo>>(todos));
        }
    }
}
