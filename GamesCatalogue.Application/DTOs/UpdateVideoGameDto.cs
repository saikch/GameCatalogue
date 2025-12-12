using System;
using System.ComponentModel.DataAnnotations;

namespace GamesCatalogue.Application.DTOs
{
    public class UpdateVideoGameDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Platform { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Genre { get; set; } = string.Empty;

        public DateTime? ReleaseDate { get; set; }

        [Range(0, 999999)]
        public decimal? Price { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
