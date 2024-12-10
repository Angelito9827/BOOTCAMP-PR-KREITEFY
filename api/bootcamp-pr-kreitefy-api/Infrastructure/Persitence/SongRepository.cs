using bootcamp_framework.Domain.Persistence;
using bootcamp_framework.Infraestructure.Persistence;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Persitence
{
    public class SongRepository : GenericRepository<Song>, ISongRepository
    {
        private readonly ApplicationContext _applicationContext;

        public SongRepository(ApplicationContext context) : base(context)
        {
            _applicationContext = context;
        }

        public List<Song> GetAllSongs()
        {
            return _applicationContext.Songs
                .Include(s => s.Album)
                .Include(s => s.Artist)
                .Include(s => s.Style)
                .ToList();
        }

        public override Song GetById(long id)
        {
            var song = _applicationContext.Songs
                .Include(s => s.Album)
                .Include(s => s.Artist)
                .Include(s => s.Style)
                .SingleOrDefault(s => s.Id == id);
            if (song == null) throw new ElementNotFoundException();
            return song;
        }

        public IEnumerable<Song> GetRecentSongs(int count = 5, long? styleId = null)
        {
            return _applicationContext.Songs
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .Where(song => !styleId.HasValue || song.StyleId == styleId)
                .OrderByDescending(s => s.CreatedAt)
                .Take(count)
                .ToList();
        }

        public override Song Insert(Song song)
        {
            _applicationContext.Songs.Add(song);
            _applicationContext.SaveChanges();
            _applicationContext.Entry(song).Reference(s => s.Album).Load();
            _applicationContext.Entry(song).Reference(s => s.Artist).Load();
            _applicationContext.Entry(song).Reference(s => s.Style).Load();
            return song;
        }

        public override Song Update(Song song)
        {
            _applicationContext.Songs.Update(song);
            _applicationContext.SaveChanges();
            _applicationContext.Entry(song).Reference(s => s.Album).Load();
            _applicationContext.Entry(song).Reference(s => s.Artist).Load();
            _applicationContext.Entry(song).Reference(s => s.Style).Load();
            return song;
        }
    }
}
