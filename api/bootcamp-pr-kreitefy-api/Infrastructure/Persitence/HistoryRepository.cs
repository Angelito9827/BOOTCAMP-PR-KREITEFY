using bootcamp_framework.Infraestructure.Persistence;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class HistoryRepository : GenericRepository<History>, IHistoryRepository
{
    private readonly ApplicationContext _applicationContext;

    public HistoryRepository(ApplicationContext context) : base(context)
    {
        _applicationContext = context;
    }

    public IEnumerable<History> GetAllByUserId(long userId)
    {
        return _applicationContext.Histories.Where(h => h.UserId == userId).Include(h => h.Song).ToList();
    }

    public History? GetByUserAndSong(long userId, long songId)
    {
        return _applicationContext.Histories
            .FirstOrDefault(h => h.UserId == userId && h.SongId == songId);
    }

    public IEnumerable<Song> GetRecommendedSongsByStyles(List<long> topTwoStyles)
    {
        return _applicationContext.Songs
            .Include(s => s.Artist)
            .Include(s => s.Album)
            .Include(s => s.Style)
            .Where(s => topTwoStyles.Contains(s.StyleId) && s.AverageScore > 3)
            .OrderByDescending(s => s.TotalPlayCount)
            .Take(5)
            .ToList();
    }
}
