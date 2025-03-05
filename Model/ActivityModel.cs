
using Dapper.Contrib.Extensions;

namespace Model
{
    [Table("Activity")]
    public class ActivityModel
    {
        [Computed]
        private string _Id{ get; set; }

        [ExplicitKey]
        public Guid? Id {
            get => string.IsNullOrWhiteSpace(_Id) ? null : Guid.Parse(_Id);
            set => _Id = value.Value.ToString();
        }

        public required string Title { get; set; }

        [Computed]
        private string? _Date{ get; set; }
        public DateTime? Date {
            get => string.IsNullOrWhiteSpace(_Date) ? null : DateTime.Parse(_Date);
            set => _Date = value.Value.ToString();
        }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public string? City { get; set; }

        public string? Venue { get; set; }

    }
}