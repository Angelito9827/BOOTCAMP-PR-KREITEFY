using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace bootcamp_pr_kreitefy_api.Domain.Entities
{

    [Table("songs")]
    public class Song
    {
        public long Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        [MinLength(3)]
        [MaxLength(100)]
        [Required]
        public required string Name { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; }

        [Required]
        public required int TotalPlayCount { get; set; }

        [Required]
        public required TimeSpan Duration { get; set; }

        [Required]
        public required double AverageScore { get; set; }

        [Required]
        public long StyleId { get; set; }

        [Required]
        public Style Style { get; set; }

        [Required]
        public long AlbumId { get; set; }

        [Required]
        public Album Album { get; set; }

        [Required]
        public long ArtistId { get; set; }

        [Required]
        public Artist Artist { get; set; }
    }
}