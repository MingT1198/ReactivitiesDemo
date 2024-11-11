using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Service;
using Repository.DbContexts;
using Repository;
using Dapper;
using API.Handlers;

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

            //因sqlite只有四個型別，要自行加入dapper的SqlMapper TypeHandler
            SqlMapper.AddTypeHandler(new DapperForSqliteTypeHandler<Guid>());
        }
    }
}