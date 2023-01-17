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
    private readonly IRepository<Todo> _todoRepository;

    public TodoAppTests()
    {
        _todoRepository = GetRequiredService<IRepository<Todo>>();
        _todoController = GetRequiredService<TodoController>();
    }


    [TestMethod]
    public async Task Should_Create_Todo()
    {
        await _todoController.CreateAsync(new TodoCreateViewModel { Description = "test", TodoGroupId = 1 });
        Assert.IsTrue(_todoRepository.GetQueryable().Count() == 1);
    }


}