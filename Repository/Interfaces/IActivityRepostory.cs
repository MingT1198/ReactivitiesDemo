using Repository.ViewModels;

namespace Repository.Interfaces
{
    public interface IActivityRepostory
    {
        Task<IEnumerable<ActivityViewModel>> GetAsync();
        Task<ActivityViewModel> GetByIdAsync(Guid id);
    }
}