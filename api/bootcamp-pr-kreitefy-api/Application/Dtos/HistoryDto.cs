using bootcamp_framework.Application.Dtos;

namespace bootcamp_pr_kreitefy_api.Application.Dtos
{
    public class HistoryDto : IDto
    {
        public long Id { get; set; }

        public required DateTime PlayedAt { get; set; }

        public required int MyPlayCount { get; set; }

        public required long UserId { get; set; }


        public required long SongId { get; set; }

    }
}
