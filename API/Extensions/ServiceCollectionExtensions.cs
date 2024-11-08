using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Service;
using Repository.DbContexts;
using Repository;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqliteDataContext>(opt => 
            {
                opt.UseSqlite(configuration.GetConnectionString("SqliteConnect"));
            });

            services.AddScoped<IDbConnection>(sq =>
            {
                var contextString = configuration.GetConnectionString("SqliteConnect");
                return new SqliteConnection(contextString);
            });

            services.Scan(scan => scan
                .FromAssemblyOf<ActivityService>()
                .AddClasses(classes => classes
                    .InNamespaceOf<ActivityService>()
                )
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );

            //repository移除因為，使用dapper讀取sqlite會遇到轉型問題，因為sqlite沒有那麼多型別很多直接使用string存
            services.Scan(scan => scan
                .FromAssemblyOf<ActivityRepostory>()
                .AddClasses(classes => classes
                    .InNamespaceOf<ActivityRepostory>()
                    .Where(type =>
                        !type.Namespace.Contains("DbContexts") &&
                        !type.Namespace.Contains("Migrations") &&
                        !type.Namespace.Contains("SeedDatas") &&
                        !type.Namespace.Contains("ViewModels")
                    )
                )
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
        }
    }
}