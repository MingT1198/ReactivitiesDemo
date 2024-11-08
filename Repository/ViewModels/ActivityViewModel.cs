using Dapper.Contrib.Extensions;
using Model;

namespace Repository.ViewModels
{
    [Table("Activity")]
    public class ActivityViewModel: ActivityModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}