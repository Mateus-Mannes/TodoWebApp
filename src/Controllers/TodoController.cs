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
using TodoApp.Repositories;

namespace TodoApp.Controllers
{
    [Route("todo")]
    [Authorize(Roles = "user")]
    public class TodoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Todo> _todoRepository;

        public TodoController(IMapper mapper, IRepository<Todo> todoRepository)
        {
            _mapper = mapper;
            _todoRepository = todoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TodoCreateViewModel input)
        {
            if (!ModelState.IsValid) return (BadRequest(ModelState.GetErrors()));

            var count = await _todoRepository.GetQueryable().AsNoTracking()
                .Where(x => x.TodoGroupId == input.TodoGroupId).CountAsync();
            if (count >= 30) return BadRequest("Todos limit reached");

            Todo todo = _mapper.Map<TodoCreateViewModel, Todo>(input);
            var created = await _todoRepository.InsertAsync(todo);
            return Ok(created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TodoUpdateViewModel input)
        {
            if (!ModelState.IsValid) return (BadRequest(ModelState.GetErrors()));

            var todo = await _todoRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == input.Id);
            if (todo == null) return NotFound("Todo not found");

            todo.Description = input.Description;
            todo.DeadLine = input.DeadLine;
            await _todoRepository.UpdateAsync(todo);
            return Ok(todo);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var todo = await _todoRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == id);
            if (todo == null) return NotFound("Todo not found");

            await _todoRepository.DeleteAsync(todo.Id);
            return Ok(todo);
        }
    }
}
