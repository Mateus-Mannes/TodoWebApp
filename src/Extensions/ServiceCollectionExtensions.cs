using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TodoApp.Data;
using TodoApp.Repositories;

namespace TodoApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services, TodoAppDbContext context)
        {
            var dbSets = context.GetDbSets();
            foreach (var dbSet in dbSets)
            {
                var setType = dbSet.PropertyType.GenericTypeArguments[0];

                var repositoryInterface = typeof(IRepository<>).MakeGenericType(setType);
                var repositoryImplementation = typeof(Repository<>).MakeGenericType(setType);
                var constructor = repositoryImplementation.GetConstructors()[0];

                services.AddScoped(repositoryInterface, x => constructor.Invoke(new object[] { context }));
            }
        }
    }
}
