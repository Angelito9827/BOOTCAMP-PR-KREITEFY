using bootcamp_framework.Application;
using bootcamp_framework.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Domain.Entities;

namespace bootcamp_pr_kreitefy_api.Domain.Persistence
{
    public interface IHistoryRepository : IGenericRepository<History>
    {
        IEnumerable<History> GetAllByUserId(long userId);
        History? GetByUserAndSong(long userId, long songId);
        IEnumerable<Song> GetRecommendedSongsByStyles(List<long> topTwoStyles);
        PagedList<History> GetHistorySongs(long userId, PaginationParameters paginationParameters);
    }
}
