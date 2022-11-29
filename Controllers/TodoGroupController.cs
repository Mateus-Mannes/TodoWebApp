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

        [HttpPost]
        public async Task<IActionResult> CreateAsync(string name)
        {
            if(await (_context.TodoGroups.AsNoTracking().Include(x => x.User)
                .AnyAsync(x => x.Name == name && x.User.Slug == User.Identity.Name)))
            {
                return BadRequest("A list with this name already exists, try another one");
            }

            if (await (_context.TodoGroups.AsNoTracking().Include(x => x.User).CountAsync()) >= 10)
            {
                return BadRequest("Todo Lists limit reached");
            }

            var user = await _context.Users.Where(x => x.Slug == User.Identity.Name).FirstOrDefaultAsync();

            try
            {
                var created = await _context.TodoGroups.AddAsync(new TodoGroup(name, user.Id));
                await _context.SaveChangesAsync();
                return Ok(created.Entity);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
