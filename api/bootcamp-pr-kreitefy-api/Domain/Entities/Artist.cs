using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bootcamp_pr_kreitefy_api.Domain.Entities
{
    [Table("artists")]
    public class Artist
    {
        public long Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        [MinLength(3)]
        [MaxLength(100)]
        [Required]
        public required string Name { get; set; }
    }
}
