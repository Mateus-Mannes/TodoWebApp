using System.ComponentModel;
using TodoApp.Controllers;
using TodoApp.Domain;
using TodoApp.Repositories;
using TodoApp.ViewModels;

namespace TodoApp.Tests;

[TestClass]
public class TodoAppTests : TodoAppTestBase
{
    private readonly IRepository<Todo> _todoRepository;

    public TodoAppTests()
    {
        _todoRepository = GetRequiredService<IRepository<Todo>>();
    }


    [TestMethod]
    [Category("Todo")]
    public async Task ShouldCrea()
    {
        var teste = await _todoRepository.GetAllAsync();
        await _todoRepository.InsertAsync(new Todo() { Description = "test" });
        Assert.Fail();
    }

    [TestMethod]
    [Category("Todo")]
    public void ShouldCrea2()
    {
        Assert.Fail();
    }


}