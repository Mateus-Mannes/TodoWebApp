using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Domain;

namespace TodoApp.Tests
{
    public static class TodoAppTestsDataSeed
    {
        public static void SeedTests(this TodoAppDbContext context)
        {
            User user = new User() { Name = "User", Slug = "user", PasswordHash = "123", 
                TodoGroups = new List<TodoGroup>() { new TodoGroup() { Name = "Todos", Slug = "todos"} }
                };
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
