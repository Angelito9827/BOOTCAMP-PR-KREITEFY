using bootcamp_framework.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Domain.Entities;

namespace bootcamp_pr_kreitefy_api.Domain.Persistence
{
    public interface IHistoryRepository : IGenericRepository<History>
    {
        void IncrementPlayCount(long userID, long SongId);
    }
}
