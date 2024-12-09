using bootcamp_framework.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Domain.Entities;

namespace bootcamp_pr_kreitefy_api.Domain.Persistence
{
    public interface ISongRepository : IGenericRepository<Song>
    {
        List<Song> GetAllSongs();
        IEnumerable<Song> GetRecentSongs(int count = 5, long? styleId = null);
    }
}
