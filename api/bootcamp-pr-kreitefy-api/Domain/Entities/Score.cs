using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bootcamp_pr_kreitefy_api.Domain.Entities
{
    [Table("scores")]
    public class Score
    {
        public long Id { get; set; }

        [Required]
        public required int Stars { get; set; }

        [Required]
        public required long UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public required long SongId { get; set; }

        [Required]
        public Song Song { get; set; }
    }
}
