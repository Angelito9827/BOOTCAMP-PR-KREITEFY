using bootcamp_framework.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Domain.Entities;

namespace bootcamp_pr_kreitefy_api.Domain.Persistence
{
    public interface IScoreRepository : IGenericRepository<Score>
    {
        Score? GetByUserAndSong(long userId, long songId);
        List<Score> GetScoresBySong(long songId);
        void Add(Score score);
    }
}
