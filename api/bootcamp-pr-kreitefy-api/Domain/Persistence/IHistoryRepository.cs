using bootcamp_framework.Application;
using bootcamp_framework.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Domain.Entities;

namespace bootcamp_pr_kreitefy_api.Domain.Persistence
{
    public interface IHistoryRepository : IGenericRepository<History>
    {
        IEnumerable<History> GetAllByUserId(long userId);
        IEnumerable<RecommendedSongDto> GetRecommendedSongs(long userId);
        PagedList<History> GetHistorySongs(long userId, PaginationParameters paginationParameters);
    }
}
