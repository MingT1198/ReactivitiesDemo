using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("Activity")]
    public class ActivityModel
    {
        private string _Id;

        [Key]
        public Guid Id { get => Guid.Parse(_Id); set => _Id = value.ToString(); }

        public required string Title { get; set; }

        private string _Date;
        public DateTime? Date { get => string.IsNullOrWhiteSpace(_Date) ? null : DateTime.Parse(_Date); set => _Date = value?.ToString(); }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public string? City { get; set; }

        public string? Venue { get; set; }

    }
}