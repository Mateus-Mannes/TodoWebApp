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
        Assert.IsTrue(_todoRepository.GetQueryable().Where(x => x.Description == "test").Count() == 1);
    }

    [TestMethod]
    public async Task Should_Update_Todo()
    {
        var result = (await _todoController.CreateAsync(new TodoCreateViewModel { Description = "test", TodoGroupId = 1 })) as OkObjectResult;
        var todo = result.Value as Todo;
        await _todoController.UpdateAsync(new TodoUpdateViewModel()
            { Id = todo.Id, Description = "updated", DeadLine = DateTime.Now });
        Assert.IsTrue(_todoRepository.GetQueryable().Where(x => x.Description == "updated").Count() == 1);
    }

    [TestMethod]
    public async Task Should_Delete_Todo()
    {
        var result = (await _todoController.CreateAsync(new TodoCreateViewModel { Description = "test", TodoGroupId = 1 })) as OkObjectResult;
        var todo = result.Value as Todo;
        await _todoController.DeleteAsync(todo.Id);
        Assert.IsTrue(_todoRepository.GetQueryable().Where(x => x.Description == "test").Count() == 0);
    }

    [TestMethod]
    public async Task Should_Get_Groups_And_Todos()
    {
        await _todoGroupRepository.InsertAsync(new TodoGroup() { Name = "New Group", Slug = "ng", User = User, 
            Todos = new List<Todo>() { new Todo() { Description = "1" }, new Todo() { Description = "2" } }
        });
        var result = await _todoGroupController.GetListAsync();
        var groups = (result as OkObjectResult).Value as IEnumerable<TodoGroup>;
        Assert.IsTrue(groups.Where(x => x.Name == "New Group").Count() == 1);
        Assert.IsTrue(groups.First(x => x.Name == "New Group").Todos.Count() == 2);
        Assert.IsTrue(groups.Where(x => x.Name == "Todos").Count() == 1);
        Assert.IsTrue(groups.First(x => x.Name == "Todos").Todos.Count() == 0);
    }

    [TestMethod]
    public async Task Should_Create_Group()
    {
        await _todoGroupController.CreateAsync("New Group");
        Assert.IsTrue(_todoGroupRepository.GetQueryable().Where(x => x.Name == "New Group").Count() == 1);
    }

    [TestMethod]
    public async Task Should_Not_Able_Same_Name_Group()
    {
        await _todoGroupController.CreateAsync("New Group");
        var result = await _todoGroupController.CreateAsync("New Group");
        Assert.IsTrue(result.GetType() == typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task Should_Delete_Group()
    {
        var group = await _todoGroupRepository.InsertAsync(new TodoGroup()
        {
            Name = "New Group",
            Slug = "ng",
            User = User,
            Todos = new List<Todo>() { new Todo() { Description = "1" }, new Todo() { Description = "2" } }
        });
        await _todoGroupController.DeleteAsync(group.Id);
        Assert.IsTrue(_todoGroupRepository.GetQueryable().Where(x => x.Name == "New Group").Count() == 0);
        Assert.IsTrue(_todoRepository.GetQueryable().Count() == 0);
    }


}