using Microsoft.AspNetCore.Mvc;
using TodoApp.ViewModels;
using TodoApp.Extensions;
using TodoApp.Data;
using TodoApp.Domain;
using AutoMapper;
using SecureIdentity.Password;
using Microsoft.EntityFrameworkCore;
using TodoApp.Services;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly TodoAppDbContext _context;
        private readonly TokenService _tokenService;

        public AccountController(TodoAppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserCreateViewModel input)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState.GetErrors());
        
            if ((await _context.Users.AsNoTracking().CountAsync()) >= 100)
                return BadRequest("The limit of users from the application was reached.");

            var slug = input.Name.Replace(' ', '\0').ToLower();

            if(_context.Users.AsNoTracking().Any(x => x.Slug == slug))
            {
                return BadRequest("This name is already taken, choose another.");
            }

            var user = new User() { Name = input.Name, Slug = slug };
            user.PasswordHash = PasswordHasher.Hash(input.Password);
            user.TodoGroups.Add(new TodoGroup() { Name = "Todos", Slug = "todos" });

            try
            {
                var created = await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return Ok(created.Entity);
            }
            catch { return StatusCode(500, "Internal server error"); }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserCreateViewModel input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrors());

            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Name == input.Name);

            if (user == null)
            {
                return NotFound("Invalid user or password.");
            }

            if (!PasswordHasher.Verify(user.PasswordHash, input.Password))
                return BadRequest("Invalid user or password.");

            try
            {
                var token = _tokenService.GenerateToken(user);
                return Ok(token);
            }
            catch { return StatusCode(500, "Internal server error."); }
        }
    }
}
