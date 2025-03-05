using Model;

namespace Repository.Interfaces
{
    public interface IActivityRepostory
    {
        Task<IEnumerable<ActivityModel>> GetAsync();
        Task<ActivityModel> GetByIdAsync(Guid id);
        Task<int> AddAsync(ActivityModel model);
        Task<bool> PutByIdAsync(ActivityModel model);
    }
}