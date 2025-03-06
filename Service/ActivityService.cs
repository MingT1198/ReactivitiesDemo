using Service.Interfaces;
using Microsoft.Extensions.Logging;
using Repository.DbContexts;
using Microsoft.EntityFrameworkCore;
using Model;
using Repository.Interfaces;

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


        public Task<IEnumerable<ActivityModel>> GetAsync()
        {
            //用entity
            // return await _dbContext.Activitys.ToListAsync();

            //用dapper
            return _repostory.GetAsync();
        }

        public Task<ActivityModel> GetByIdAsync(Guid id)
        {
            //用entity
            // return await _dbContext.Activitys.SingleOrDefaultAsync(x => x.Id == id);

            //用dapper
            return _repostory.GetByIdAsync(id);
        }

        public Task AddAsync(ActivityModel model)
        {
            model.Id = Guid.NewGuid();
            model.Date = DateTime.Now;
            return _repostory.AddAsync(model);
        }

        public async Task<Task> PutByIdAsync(Guid id, ActivityModel model)
        {
            var orgModel =  await _repostory.GetByIdAsync(id);
            if (orgModel.Id != model.Id)
            {
                throw new Exception("Id不匹配");
            }

            model.Date = DateTime.Now;
            return _repostory.PutByIdAsync(model);
        }
        public async Task<Task> DeleteByIdAsync(Guid id)
        {
            var orgModel =  await _repostory.GetByIdAsync(id);
            if (orgModel == null)
            {
                throw new Exception("Id不匹配");
            }

            return _repostory.DeleteByIdAsync(orgModel);
        }
    }
}