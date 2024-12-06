namespace bootcamp_pr_kreitefy_api.Application.Dtos
{
    public class ScoreDto
    {
        public long Id { get; set; }

        public required int Stars { get; set; }

        public required long UserId { get; set; }

        public required long SongId { get; set; }

    }
}
