using System.Data;
using System.Reflection;
using Dapper;
using Dapper.Contrib.Extensions;
using Model;
using Repository.DbContexts;

namespace Repository.SeedDatas
{
    public class Seed
    {
        // Entity Framework
        public static async Task ActivitySeedData(SqliteDataContext context)
        {
            if (context.Activitys.Any()) return;
            
            var activities = GetData();

            await context.Activitys.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }

        // Dapper
        public static async Task ActivitySeedData(IDbConnection connection)
        {
            var attribute = typeof(ActivityModel).GetCustomAttribute<TableAttribute>();
            if (attribute == null)
            {
                throw new Exception("Can't find Dapper TableAttribute.");
            }
            else
            {
                var tableName = attribute.Name;
                if (connection.ExecuteScalar<int>($"select count(*) from {tableName}") > 0) return;
            }
            
            var activities = GetData();

            foreach (var activity in activities)
            {
                await connection.InsertAsync(activity);
            }
        }

        private static IEnumerable<ActivityModel> GetData(){
            return new List<ActivityModel>
            {
                new ActivityModel
                {
                    Title = "Past Activity 1",
                    Date = DateTime.UtcNow.AddMonths(-2),
                    Description = "Activity 2 months ago",
                    Category = "drinks",
                    City = "London",
                    Venue = "Pub",
                },
                new ActivityModel
                {
                    Title = "Past Activity 2",
                    Date = DateTime.UtcNow.AddMonths(-1),
                    Description = "Activity 1 month ago",
                    Category = "culture",
                    City = "Paris",
                    Venue = "Louvre",
                },
                new ActivityModel
                {
                    Title = "Future Activity 2",
                    Date = DateTime.UtcNow.AddMonths(2),
                    Description = "Activity 2 months in future",
                    Category = "music",
                    City = "London",
                    Venue = "O2 Arena",
                },
                new ActivityModel
                {
                    Title = "Future Activity 7",
                    Date = DateTime.UtcNow.AddMonths(7),
                    Description = "Activity 2 months ago",
                    Category = "travel",
                    City = "London",
                    Venue = "Somewhere on the Thames",
                },
                new ActivityModel
                {
                    Title = "Future Activity 8",
                    Date = DateTime.UtcNow.AddMonths(8),
                    Description = "Activity 8 months in future",
                    Category = "film",
                    City = "London",
                    Venue = "Cinema",
                }
            };
        }
    }
}