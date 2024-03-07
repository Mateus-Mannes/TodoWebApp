using System.Reflection;
using TodoApp.Data;

namespace TodoApp.Extensions;

public static class DbContextExtensions
{
    public static List<PropertyInfo> GetDbSets()
    {
        return typeof(TodoAppDbContext).GetProperties()
            .Where(x =>
            x.PropertyType.IsGenericType 
            && x.PropertyType.Name.StartsWith("DbSet"))
            .ToList();
    }
}
