using TodoApp.Data;
using TodoApp.Domain;

namespace TodoApp.Tests;

public static class TodoAppTestsDataSeed
{
    public static void SeedTests(this TodoAppDbContext context, User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
    }
}
