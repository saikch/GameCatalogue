using System;

namespace GamesCatalogue.Application.DTOs
{
    public class VideoGameDto
    {
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Platform { get; init; } = string.Empty;
        public string Genre { get; init; } = string.Empty;
        public DateTime? ReleaseDate { get; init; }
        public decimal? Price { get; init; }
        public bool IsAvailable { get; init; }
    }
}
