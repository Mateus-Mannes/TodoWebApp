using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TodoApp.Data;
using TodoApp.Repositories;

namespace TodoApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services, TodoAppDbContext context = null)
        {
            var dbSets = context.GetDbSets();
            foreach (var dbSet in dbSets)
            {
                var setType = dbSet.PropertyType.GenericTypeArguments[0];

                var repositoryInterface = typeof(IRepository<>).MakeGenericType(setType);
                var repositoryImplementation = typeof(Repository<>).MakeGenericType(setType);

                if(context != null)
                {
                    var constructor = repositoryImplementation.GetConstructors()[0];
                    services.AddTransient(repositoryInterface, x => constructor.Invoke(new object[] { context }));
                }
                else
                {
                    services.AddTransient(repositoryInterface, repositoryImplementation);
                }
            }
        }

        public static void AddMapper(this IServiceCollection services)
        {
            var mapperCfg = new MapperConfiguration(cfg => { cfg.AddProfile<TodoAppAutoMapperProfile>(); });
            var mapper = mapperCfg.CreateMapper();
            services.AddSingleton<IMapper>(mapper);
        }
    }
}
