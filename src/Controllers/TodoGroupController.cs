using Microsoft.AspNetCore.Mvc;
using TodoApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TodoApp.Repositories;
using TodoApp.Extensions;

namespace TodoApp.Controllers;

[Route("todo-group")]
[Authorize(Roles = "user")]
public class TodoGroupController : Controller
{
    private readonly IRepository<TodoGroup> _todoGroupRepository;

    public TodoGroupController(IRepository<TodoGroup> todoGroupRepository, IRepository<User> userRepository)
    {
        _todoGroupRepository = todoGroupRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        var lists = await _todoGroupRepository.GetQueryable().AsNoTracking()
            .Include(x => x.Todos)
            .ToListAsync();

        return Ok(lists);
    }

    [HttpPost]
    [Route("{name}")]
    public async Task<IActionResult> CreateAsync([FromRoute]string name)
    {
        if (await (_todoGroupRepository.GetQueryable().AsNoTracking().AnyAsync(x => x.Name == name)))
            return BadRequest("A list with this name already exists, try another one");

        if (await (_todoGroupRepository.GetQueryable().AsNoTracking().CountAsync()) >= 10)
            return BadRequest("Todo Lists limit reached");

        var created = await _todoGroupRepository.InsertAsync(new TodoGroup(name, User.UserId()));
        return Ok(created);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var list = await _todoGroupRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == id);
        if (list == null) return NotFound("List not found");

        await _todoGroupRepository.DeleteAsync(list.Id);
        return Ok(list);
    }
}

