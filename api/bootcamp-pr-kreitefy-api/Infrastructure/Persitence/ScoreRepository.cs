using bootcamp_framework.Infraestructure.Persistence;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Infrastructure.Persistence;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Persitence
{
    public class ScoreRepository : GenericRepository<Score>, IScoreRepository
    {
        private readonly ApplicationContext _applicationContext;

        public ScoreRepository(ApplicationContext context) : base(context)
        {
            _applicationContext = context;
        }

        public Score? GetByUserAndSong(long userId, long songId)
        {
            return _applicationContext.Scores
               .FirstOrDefault(s => s.UserId == userId && s.SongId == songId);
        }

        public List<Score> GetScoresBySong(long songId)
        {
            return _applicationContext.Scores
                .Where(s => s.SongId == songId)
                .ToList();
        }
        public void Add(Score score)
        {
            _applicationContext.Scores.Add(score);
            _applicationContext.SaveChanges();
        }
    }
}
