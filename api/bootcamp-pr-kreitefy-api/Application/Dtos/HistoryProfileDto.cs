namespace bootcamp_pr_kreitefy_api.Application.Dtos
{
    public class HistoryProfileDto
    {
        public DateTime PlayedAt { get; set; }
        public string PlayedAtFormatted => PlayedAt.ToString("yyyy-MM-dd HH:mm:ss");
        public long SongId { get; set; }
        public string SongName { get; set; }
    }
}
