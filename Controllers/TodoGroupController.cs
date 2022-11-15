using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("todo-group")]
    [Authorize(Roles = "user")]
    public class TodoGroupController : ControllerBase
    {
        private readonly TodoAppDbContext _context;

        public TodoGroupController(TodoAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            try
            {
                var lists = await _context.TodoGroups.AsNoTracking()
                    .Include(x => x.Todos)
                    .Include(x => x.User)
                    .Where(x => x.User.Slug == User.Identity.Name)
                    .ToListAsync();

                return Ok(lists);
            } catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
