using bootcamp_framework.Domain.Persistence;
using bootcamp_framework.Infraestructure.Persistence;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Persitence
{
    public class SongRepository : GenericRepository<Song>, ISongRepository
    {
        ApplicationContext _applicationContext;
        public SongRepository(ApplicationContext context) : base(context)
        {
            _applicationContext = context;
        }

        public List<SongDto> GetAllSongs()
        {
            return _applicationContext.Songs
                .Select(i => new SongDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    AlbumId = i.AlbumId,
                    AlbumName = i.Album.Name,
                    AlbumImage = Convert.ToBase64String(i.Album.Image),
                    ArtistId = i.ArtistId,
                    ArtistName = i.Artist.Name,
                    StyleId = i.StyleId,
                    StyleName = i.Style.Name,
                    Duration = (i.Duration),
                    TotalPlayCount = i.TotalPlayCount,
                    AverageScore = i.AverageScore,
                    CreatedAt = i.CreatedAt
                })
                .ToList();
        }

        public override Song GetById(long id)
        {
            var song = _applicationContext.Songs
                .Include(i => i.Album)
                .Include(i => i.Artist)
                .Include(i => i.Style)
                .SingleOrDefault(i => i.Id == id);
            if (song == null)
            {
                throw new ElementNotFoundException();
            }

            return song;
        }

        public IEnumerable<RecentSongDto> GetRecentSongs(int count = 5)
        {
            var songs = _applicationContext.Songs
                .OrderByDescending(song => song.CreatedAt)
                .Take(count)
                .Select(i => new RecentSongDto()
                {
                    Id = i.Id,
                    Name = i.Name,
                    ArtistName = i.Artist.Name,
                    AlbumImage = Convert.ToBase64String(i.Album.Image),
                });
            return songs;
        }

        public override Song Insert(Song song)
        {
            _applicationContext.Songs.Add(song);
            _applicationContext.SaveChanges();
            _applicationContext.Entry(song).Reference(i => i.Style).Load();
            _applicationContext.Entry(song).Reference(i => i.Album).Load();
            _applicationContext.Entry(song).Reference(i => i.Artist).Load();
            return song;
        }

        public override Song Update(Song song)
        {
            _applicationContext.Songs.Update(song);
            _applicationContext.SaveChanges();
            _applicationContext.Entry(song).Reference(i => i.Style).Load();
            _applicationContext.Entry(song).Reference(i => i.Album).Load();
            _applicationContext.Entry(song).Reference(i => i.Artist).Load();
            return song;
        }
    }
}
