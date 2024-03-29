﻿using Microsoft.AspNetCore.Mvc;
using TodoApp.ViewModels;
using TodoApp.Extensions;
using TodoApp.Domain;
using SecureIdentity.Password;
using Microsoft.EntityFrameworkCore;
using TodoApp.Services;
using TodoApp.Repositories;

namespace TodoApp.Controllers;

[Route("account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IRepository<User> _userRepository;
    private readonly TokenService _tokenService;

    public AccountController(IRepository<User> userRepository, TokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserCreateViewModel input)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.GetErrors());

        if ((await _userRepository.GetQueryable().AsNoTracking().CountAsync()) >= 100)
            return BadRequest("The limit of users from the application was reached.");

        var passwordHash = PasswordHasher.Hash(input.Password);
        var user = new User() { Name = input.Name, PasswordHash = passwordHash };

        if (_userRepository.GetQueryable().AsNoTracking().Any(x => x.Slug == user.Slug))
            return BadRequest("This name is already taken, choose another.");

        user.TodoGroups.Add(new TodoGroup() { Name = "Todos", Slug = "todos" });
        var created = await _userRepository.InsertAsync(user);

        return Ok(created);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserCreateViewModel input)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.GetErrors());

        var user = await _userRepository.GetQueryable()
            .AsNoTracking().FirstOrDefaultAsync(x => x.Name == input.Name);

        if (user == null) return NotFound("Invalid user or password.");

        if (!PasswordHasher.Verify(user.PasswordHash, input.Password))
            return BadRequest("Invalid user or password.");

        var token = _tokenService.GenerateToken(user);

        return Ok(token);
    }
}