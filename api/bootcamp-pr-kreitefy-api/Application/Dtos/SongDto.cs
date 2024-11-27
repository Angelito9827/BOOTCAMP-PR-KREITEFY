using bootcamp_framework.Application.Dtos;

namespace bootcamp_pr_kreitefy_api.Application.Dtos
{
    public class SongDto : IDto
    {
        public long Id { get; set; }

        public required string Name { get; set; }

        public required DateTime CreatedAt { get; set; }

        public required int TotalPlayCount { get; set; }

        public required TimeSpan Duration { get; set; }

        public required double AverageScore { get; set; }

        public long StyleId { get; set; }

        public required string StyleName { get; set; }

        public long AlbumId { get; set; }

        public required string AlbumName { get; set; }

        public long ArtistId { get; set; }

        public required string ArtistName { get; set; }
    }
}
