using Microsoft.EntityFrameworkCore;
using TodoApp.Domain;
using TodoApp.Data.Mappings;
using TodoApp.Extensions;

namespace TodoApp.Data
{
    public class TodoAppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options, IHttpContextAccessor httpContextAccessor) : base (options) {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<TodoGroup> TodoGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoGroupMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new TodoMap());

            var userId = _httpContextAccessor.HttpContext.User.UserId();
            modelBuilder.Entity<TodoGroup>().HasQueryFilter(x => 
                 EF.Property<int>(x, "UserId") == userId);
        }
    }
}
