using bootcamp_framework.Application.Services;
using bootcamp_pr_kreitefy_api.Application.Dtos;

namespace bootcamp_pr_kreitefy_api.Application.Services
{
    public interface IScoreService : IGenericService<ScoreDto>
    {
        ScoreDto AddScore(long userId, long songId, int stars);
        void UpdateSongAverageScore(long songId);
    }
}
