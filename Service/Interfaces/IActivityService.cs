using Model;

namespace Service.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityModel>> GetAsync();
        Task<ActivityModel> GetByIdAsync(Guid id);
        Task AddAsync(ActivityModel model);
        Task PutByIdAsync(Guid id, ActivityModel mode);
    }
}