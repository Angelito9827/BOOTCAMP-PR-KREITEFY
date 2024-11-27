using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bootcamp_pr_kreitefy_api.Domain.Entities
{
    [Table("albums")]
    public class Album
    {
        public long Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        [MinLength(3)]
        [MaxLength(100)]
        [Required]
        public required string Name { get; set; }

        [Required]
        public required byte[] Image { get; set; }
    }
}
