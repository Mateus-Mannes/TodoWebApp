using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using Microsoft.EntityFrameworkCore;
using TodoApp.ViewModels;
using TodoApp.Domain;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TodoApp.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("todo")]
    [Authorize(Roles = "user")]
    public class TodoController : ControllerBase
    {
        private readonly TodoAppDbContext _context;
        private readonly IMapper _mapper;

        public TodoController(TodoAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TodoCreateViewModel input)
        {
            if (!ModelState.IsValid) return (BadRequest(ModelState.GetErrors()));

            var count = await _context.Todos.AsNoTracking()
                .Where(x => x.TodoGroupId == input.TodoGroupId).CountAsync();
            if (count >= 20) return BadRequest("Todos limit reached");

            try
            {
                var todo = _mapper.Map<TodoCreateViewModel, Todo>(input);
                var created = await _context.Todos.AddAsync(todo);
                await _context.SaveChangesAsync();
                return Ok(created.Entity);
            } catch { return StatusCode(500, "Internal server error"); }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TodoUpdateViewModel input)
        {
            if (!ModelState.IsValid) return (BadRequest(ModelState.GetErrors()));

            try
            {
                var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == input.Id);
                if (todo == null) return NotFound("Todo not found");

                todo.Description = input.Description;
                todo.DeadLine = input.DeadLine;
                _context.Todos.Update(todo);
                await _context.SaveChangesAsync();
                return Ok(todo);
            } catch { return StatusCode(500, "Internal server error"); }
            
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);
                if (todo == null) return NotFound("Todo not found");

                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
                return Ok(todo);
            }
            catch { return StatusCode(500, "Internal server error"); }
        }
    }
}
