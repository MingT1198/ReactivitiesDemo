using System.Data;
using Dapper.Contrib.Extensions;
using Repository.Interfaces;
using Repository.ViewModels;

namespace Repository
{
    public class ActivityRepostory : IActivityRepostory
    {
        private readonly IDbConnection _connection;
        public ActivityRepostory(IDbConnection connection)
        {
            _connection = connection;
            
        }
        public Task<IEnumerable<ActivityViewModel>> GetAsync()
        {
            return _connection.GetAllAsync<ActivityViewModel>();
        }

        public Task<ActivityViewModel> GetByIdAsync(Guid id)
        {
            return _connection.GetAsync<ActivityViewModel>(id);
        }
    }
}