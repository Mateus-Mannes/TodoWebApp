using Microsoft.EntityFrameworkCore;
using TodoApp.Domain;
using TodoApp.Data.Mappings;
using TodoApp.Extensions;

namespace TodoApp.Data;

public class TodoAppDbContext : DbContext
{
    private readonly int _userId;
    public TodoAppDbContext(
        DbContextOptions<TodoAppDbContext> options,
        IHttpContextAccessor httpContextAccessor) : base(options)
    {
        var signedInUser = httpContextAccessor?.HttpContext?.User.UserId();
        if (signedInUser == null) throw new Exception("User not signed in");
        _userId = (int)signedInUser;
    }

    public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Todo> Todos { get; set; }
    public DbSet<TodoGroup> TodoGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TodoGroupMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new TodoMap());

        modelBuilder.Entity<TodoGroup>().HasQueryFilter(x =>
                EF.Property<int>(x, "UserId") == _userId);
    }
}

