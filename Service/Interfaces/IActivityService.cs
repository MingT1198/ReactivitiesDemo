using Repository.ViewModels;

namespace Service.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityViewModel>> GetAsync();
        Task<ActivityViewModel> GetByIdAsync(Guid id);
    }
}