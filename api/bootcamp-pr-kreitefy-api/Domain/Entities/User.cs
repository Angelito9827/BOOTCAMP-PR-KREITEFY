using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bootcamp_pr_kreitefy_api.Domain.Entities
{
    [Table("users")]
    public class User
    {
        public long Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        [MinLength(3)]
        [MaxLength(50)]
        [Required]
        public required string Name { get; set; }

        [Column(TypeName = "varchar(100)")]
        [MinLength(3)]
        [MaxLength(100)]
        [Required]
        public required string LastName { get; set; }

        [MaxLength(50)]
        [MinLength(5)]
        [EmailAddress]
        [Required]
        public required string Email { get; set; }

        [Column(TypeName = "varchar(255)")]
        [MaxLength(255)]
        [MinLength(8)]
        [Required]
        public string Password { get; set; }

        [Required]
        public long RoleId { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
