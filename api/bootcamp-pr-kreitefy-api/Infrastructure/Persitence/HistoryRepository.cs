using bootcamp_framework.Application;
using bootcamp_framework.Infraestructure.Persistence;
using bootcamp_pr_kreitefy_api.Application.Dtos;
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

    public IEnumerable<RecommendedSongDto> GetRecommendedSongs(long userId)
    {

        //var histories = _applicationContext.Histories
        //    .Where(h => h.UserId == userId)
        //    .Include(h => h.Song)
        //        .ThenInclude(s => s.Artist)
        //    .Include(h => h.Song)
        //        .ThenInclude(s => s.Album)
        //    .ToList();

        //var topTwoStyles = histories
        //    .GroupBy(h => h.Song.StyleId)
        //    .Select(g => new
        //    {
        //        StyleId = g.Key,
        //        MyPlayCount = g.Sum(h => h.MyPlayCount)
        //    })
        //    .OrderByDescending(g => g.MyPlayCount)
        //    .Take(2)
        //    .Select(g => g.StyleId)
        //    .ToList();

        //return _applicationContext.Songs
        //    .Include(s => s.Artist)
        //    .Include(s => s.Album)
        //    .Include(s => s.Style)
        //    .Where(s => topTwoStyles.Contains(s.StyleId) && s.AverageScore > 3.0)
        //    .OrderByDescending(s => s.TotalPlayCount)
        //    .Take(5)
        //    .ToList();

        var topGenres = _applicationContext.Histories
           .Where(us => us.UserId == userId)
           .GroupBy(us => us.Song.Style.Name)
           .Select(g => new
           {
               StyleName = g.Key,
               MyPlayCount = g.Sum(us => us.MyPlayCount)
           })
           .OrderByDescending(g => g.MyPlayCount)
           .Take(2)
           .Select(g => g.StyleName)
           .ToList();

        var filteredSongs = _applicationContext.Histories
            .Where(us => us.UserId == userId && topGenres.Contains(us.Song.Style.Name) && us.Song.AverageScore >= 3)
            .OrderByDescending(us => us.Song.TotalPlayCount)
            .Take(5).Select(us => new RecommendedSongDto
            {
                Id = us.Song.Id,
                Name = us.Song.Name,
                ArtistName = us.Song.Artist.Name,
                AlbumImage = Convert.ToBase64String(us.Song.Album.Image),
                StyleName = us.Song.Style.Name,
                TotalPlayCount = us.Song.TotalPlayCount
            }).ToList();

        return filteredSongs;
    }

    public PagedList<History> GetHistorySongs(long userId, PaginationParameters paginationParameters)
    {
        var history = _applicationContext.Histories
            .Include(h => h.Song)
            .Where(h => h.UserId == userId)
        .OrderByDescending(h => h.PlayedAt);

        return PagedList<History>.ToPagedList(history, paginationParameters.PageNumber, paginationParameters.PageSize);
    }

}
