using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TodoApp.Data;

namespace TodoApp.Extensions
{
    public static class DbContextExtensions
    {
        public static List<PropertyInfo> GetDbSets(this TodoAppDbContext context)
        {
            return context.GetType().GetProperties()
                .Where(x =>
                x.PropertyType.IsGenericType 
                && x.PropertyType.Name.StartsWith("DbSet"))
                .ToList();
        }
    }
}
