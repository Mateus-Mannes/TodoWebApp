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
        public static void SeedTests(this TodoAppDbContext context, User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
