﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bootcamp_pr_kreitefy_api.Domain.Entities
{
    [Table("histories")]
    public class History
    {
        public long Id { get; set; }

        [Required]
        public required DateTime PlayedAt { get; set; }

        [Required]
        public required int PlayCountById { get; set; }

        [Required]
        public required long UserId { get; set; }

        [Required]
        public required User User { get; set; }

        [Required]
        public required long SongId { get; set; }

        [Required]
        public required Song Song { get; set; }
    }
}