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

        public IEnumerable<History> GetAllByUserId(long userId)
        {
            return _applicationContext.Histories.Where(h => h.UserId == userId).ToList();
        }

        public History? GetByUserAndSong(long userId, long songId)
        {
            return _applicationContext.Histories.FirstOrDefault(h => h.UserId == userId && h.SongId == songId);
        }
    }
}
