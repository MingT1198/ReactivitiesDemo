using Service.Interfaces;
using Microsoft.Extensions.Logging;
using Repository.DbContexts;
using Microsoft.EntityFrameworkCore;
using Model;
using Repository.Interfaces;
using Repository.ViewModels;

namespace Service
{
    public class ActivityService : IActivityService
    {
        private readonly ILogger<ActivityService> _logger;
        private readonly SqliteDataContext _dbContext;
        private readonly IActivityRepostory _repostory;

        public ActivityService(ILogger<ActivityService> logger, SqliteDataContext dbContext, IActivityRepostory repostory)
        {
            _repostory = repostory;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<ActivityViewModel>> GetAsync()
        {
            //用entity
            // return await _dbContext.Activitys.ToListAsync();

            //用dapper
            return await _repostory.GetAsync();
        }

        public async Task<ActivityViewModel> GetByIdAsync(Guid id)
        {
            //用entity
            // return await _dbContext.Activitys.SingleOrDefaultAsync(x => x.Id == id);

            //用dapper
            return await _repostory.GetByIdAsync(id);
        }
    }
}