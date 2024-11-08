using Service.Interfaces;
using Microsoft.Extensions.Logging;
using Repository.Interfaces;
using Repository.ViewModels;

namespace Service
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepostory _activityRepostory;
        private readonly ILogger<ActivityService> _logger;

        public ActivityService(ILogger<ActivityService> logger, IActivityRepostory activityRepostory)
        {
            _logger = logger;
            _activityRepostory = activityRepostory;
        }

        public Task<IEnumerable<ActivityViewModel>> GetAsync()
        {
            return _activityRepostory.GetAsync();
        }

        public Task<ActivityViewModel> GetByIdAsync(Guid id)
        {
            return _activityRepostory.GetByIdAsync(id);
        }
    }
}