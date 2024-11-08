
using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository.DbContexts
{
    public class SqliteDataContext : DbContext
    {
        public SqliteDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ActivityModel> Activitys { get; set; }
    }
}