
using Dapper.Contrib.Extensions;

namespace Model
{
    [Table("Activity")]
    public class ActivityModel
    {
        [ExplicitKey]
        public Guid? Id { get; set;}

        public required string Title { get; set; }

        public DateTime? Date {get; set;}

        public string? Description { get; set; }

        public string? Category { get; set; }

        public string? City { get; set; }

        public string? Venue { get; set; }

    }
}