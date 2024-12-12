namespace bootcamp_pr_kreitefy_api.Application.Dtos
{
    public class MostPlayedSongsDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ArtistName { get; set; }
        public string AlbumImage { get; set; }
        public required int TotalPlayCount { get; set; }
    }
}
