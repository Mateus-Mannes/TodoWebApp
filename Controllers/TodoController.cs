using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using Microsoft.EntityFrameworkCore;
using TodoApp.ViewModels;
using TodoApp.Domain;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("todo")]
    public class TodoController : ControllerBase
    {
        private readonly TodoAppDbContext _context;
        private readonly IMapper _mapper;

        public TodoController(TodoAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var todos = await _context.Todos.AsNoTracking().ToListAsync();
            return Ok(new ResultViewModel<List<Todo>>(todos));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TodoCreateViewModel input)
        {
            var todo = _mapper.Map<TodoCreateViewModel, Todo>(input);
            todo.UserId = 1;
            var created = await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return Ok(new ResultViewModel<Todo>(created.Entity));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TodoUpdateViewModel input)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == input.Id);
            todo.Description = input.Description;
            todo.DeadLine = input.DeadLine;
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
            return Ok(new ResultViewModel<Todo>(todo));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return Ok(new ResultViewModel<Todo>(todo));
        }
    }
}
