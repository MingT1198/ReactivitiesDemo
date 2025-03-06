using Model;

namespace Service.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityModel>> GetAsync();
        Task<ActivityModel> GetByIdAsync(Guid id);
        Task AddAsync(ActivityModel model);
        Task<Task> PutByIdAsync(Guid id, ActivityModel mode);
        Task<Task> DeleteByIdAsync(Guid id);
    }
}