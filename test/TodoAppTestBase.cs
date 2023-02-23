using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Controllers;
using TodoApp.Data;
using TodoApp.Domain;
using TodoApp.Extensions;
using TodoApp.Repositories;
using TodoApp.Services;

namespace TodoApp.Tests
{
    public class TodoAppTestBase : IDisposable
    {
        protected User User = new User()
        {
            Id = 1,
            Name = "User",
            Slug = "user",
            PasswordHash = "123",
            TodoGroups = new List<TodoGroup>() { new TodoGroup() { Name = "Todos", Slug = "todos" } }
        };
        private readonly SqliteConnection connection;
        private IServiceCollection Services = new ServiceCollection();
        private HttpContextAccessor HttpContextAccessor;
        public TodoAppTestBase()
        {
            HttpContextAccessor = new HttpContextAccessor() { HttpContext = new DefaultHttpContext() { User = CreateUser() } };
            this.connection = new SqliteConnection("DataSource=:memory:");
            this.connection.Open();
            ConfigureDb();
            ConfigureServices();
            ConfigureControllers();
        }
        public void Dispose() => this.connection.Dispose();

        public T GetRequiredService<T>()
        {
            using(var provider = Services.BuildServiceProvider())
                return provider.GetRequiredService<T>();
        }

        public object GetRequiredService(Type type)
        {
            using (var provider = Services.BuildServiceProvider())
                return provider.GetRequiredService(type);
        }

        private void ConfigureDb()
        {
            var dbContext = new TodoAppDbContext(new DbContextOptionsBuilder<TodoAppDbContext>()
                .UseSqlite(this.connection)
                .Options, User.Id );
            dbContext.Database.EnsureCreated();
            dbContext.SeedTests(User);
            Services.AddRepositories(dbContext);
        }

        private void ConfigureServices()
        {
            Services.AddMapper();
            Services.AddTransient<TokenService>();
        }

        private void ConfigureControllers()
        {
            var controllers = typeof(Controller).Assembly.GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Controller)));
            var construtors = controllers.Select(x => x.GetConstructors()[0]);
            foreach(var constructor in construtors)
            {
                var parameters = constructor.GetParameters();
                var dependencies = parameters.Select(x => GetRequiredService(x.ParameterType)).ToArray();
                var obj = constructor.Invoke(dependencies);

                var controllerContext = obj.GetType().GetProperties()
                    .First(x => x.Name == "ControllerContext").GetValue(obj);
                controllerContext.GetType().GetProperties().First(x => x.Name == "HttpContext")
                    .SetValue(controllerContext, HttpContextAccessor.HttpContext);
                Services.AddTransient(constructor.DeclaringType, x => obj);
            }
        }

        private ClaimsPrincipal CreateUser()
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>() {
                    new Claim(ClaimTypes.Name, User.Slug),
                    new Claim("UserId", User.Id.ToString()),
                    new Claim(ClaimTypes.Role, "user")
                }));
        }
    }
}
