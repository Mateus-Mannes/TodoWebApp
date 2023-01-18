using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TodoApp.Controllers;
using TodoApp.Domain;
using TodoApp.Repositories;
using TodoApp.ViewModels;

namespace TodoApp.Tests;

[TestClass]
public class TodoAppTests : TodoAppTestBase
{
    private readonly TodoController _todoController;
    private readonly TodoGroupController _todoGroupController;
    private readonly IRepository<Todo> _todoRepository;
    private readonly IRepository<TodoGroup> _todoGroupRepository;

    public TodoAppTests()
    {
        _todoRepository = GetRequiredService<IRepository<Todo>>();
        _todoGroupRepository = GetRequiredService<IRepository<TodoGroup>>();
        _todoController = GetRequiredService<TodoController>();
        _todoGroupController = GetRequiredService<TodoGroupController>();
    }


    [TestMethod]
    public async Task Should_Create_Todo()
    {
        await _todoController.CreateAsync(new TodoCreateViewModel { Description = "test", TodoGroupId = 1 });
        Assert.IsTrue(_todoRepository.GetQueryable().Count() == 1);
    }

    [TestMethod]
    public async Task Should_Create_Group()
    {
        await _todoGroupController.CreateAsync("New Group");
        Assert.IsTrue(_todoGroupRepository.GetQueryable().Count() == 2);
    }


}