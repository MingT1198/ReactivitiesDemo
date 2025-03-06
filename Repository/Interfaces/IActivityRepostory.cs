using Model;

namespace Repository.Interfaces
{
    public interface IActivityRepostory
    {
        Task<IEnumerable<ActivityModel>> GetAsync();
        Task<ActivityModel> GetByIdAsync(Guid id);
        Task AddAsync(ActivityModel model);
        Task PutByIdAsync(ActivityModel model);
        Task DeleteByIdAsync(ActivityModel model);
    }
}