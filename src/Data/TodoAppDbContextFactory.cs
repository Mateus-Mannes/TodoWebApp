using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using TodoApp.Extensions;

namespace TodoApp.Data;

public class TodoAppDbContextFactory : ITodoAppDbContextFactory, IDesignTimeDbContextFactory<TodoAppDbContext>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public TodoAppDbContextFactory(IHttpContextAccessor httpContextAccessor) {
        _httpContextAccessor = httpContextAccessor;
    }

    public TodoAppDbContext CreateContext()
    {
        var signedInUser = _httpContextAccessor.HttpContext.User.UserId();
        var options = new DbContextOptionsBuilder<TodoAppDbContext>()
                .UseSqlite("Data Source=app.db")
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine)
                .Options;

        return new TodoAppDbContext(options, signedInUser);
    }

    public TodoAppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<TodoAppDbContext>()
            .UseSqlite("Data Source=app.db")
            .Options;

        return new TodoAppDbContext(options);
    }
}
