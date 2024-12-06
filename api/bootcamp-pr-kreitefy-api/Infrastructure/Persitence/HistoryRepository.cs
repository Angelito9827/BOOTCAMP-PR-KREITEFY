using bootcamp_framework.Infraestructure.Persistence;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Infrastructure.Persistence;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Persitence
{
    public class HistoryRepository : GenericRepository<History>, IHistoryRepository
    {
        private readonly ApplicationContext _applicationContext;

        public HistoryRepository(ApplicationContext context) : base(context)
        {
            _applicationContext = context;
        }

        public void IncrementPlayCount(long userId, long songId)
        {

            var userExists = _applicationContext.Users.Any(u => u.Id == userId);
            if (!userExists)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            var history = _applicationContext.Histories
                .FirstOrDefault(h => h.UserId == userId && h.SongId == songId);

            if (history == null)
            {
                history = new History
                {
                    UserId = userId,
                    SongId = songId,
                    MyPlayCount = 1,
                    PlayedAt = DateTime.UtcNow
                };
                _applicationContext.Histories.Add(history);
            }
            else
            {
                history.MyPlayCount++;
                history.PlayedAt = DateTime.UtcNow;
            }

            var song = _applicationContext.Songs.FirstOrDefault(s => s.Id == songId);
            if (song == null)
                throw new KeyNotFoundException($"Song with ID {songId} not found.");

            song.TotalPlayCount++;

            _applicationContext.SaveChanges();
        }
    }
}
