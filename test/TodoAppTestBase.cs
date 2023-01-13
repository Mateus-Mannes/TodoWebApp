using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Domain;
using TodoApp.Extensions;
using TodoApp.Repositories;

namespace TodoApp.Tests
{
    public class TodoAppTestBase : IDisposable
    {
        private readonly SqliteConnection connection;
        private IServiceCollection Services = new ServiceCollection();
        public TodoAppTestBase()
        {
            this.connection = new SqliteConnection("DataSource=:memory:");
            this.connection.Open();
            ConfigureDb();

        }
        public void Dispose() => this.connection.Dispose();

        public T GetRequiredService<T>()
        {
            using(var provider = Services.BuildServiceProvider())
                return provider.GetRequiredService<T>();
        }

        private void ConfigureDb()
        {
            var dbContext = new TodoAppDbContext(new DbContextOptionsBuilder<TodoAppDbContext>()
                .UseSqlite(this.connection)
                .Options);
            dbContext.Database.EnsureCreated();
            
            Services.AddRepositories(dbContext);
        }
    }
}
