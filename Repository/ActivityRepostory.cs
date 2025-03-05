using System.Data;
using Dapper.Contrib.Extensions;
using Model;
using Repository.Interfaces;

namespace Repository
{
    public class ActivityRepostory : IActivityRepostory
    {
        private readonly IDbConnection _connection;
        public ActivityRepostory(IDbConnection connection)
        {
            _connection = connection;
            
        }


        public Task<IEnumerable<ActivityModel>> GetAsync()
        {
            return _connection.GetAllAsync<ActivityModel>();
        }
        public Task<ActivityModel> GetByIdAsync(Guid id)
        {
            return _connection.GetAsync<ActivityModel>(id);
        }
        public Task<int> AddAsync(ActivityModel model)
        {
            return _connection.InsertAsync(model);
        }
        public Task<bool> PutByIdAsync(ActivityModel model)
        {
            return _connection.UpdateAsync(model);
        }
    }
}