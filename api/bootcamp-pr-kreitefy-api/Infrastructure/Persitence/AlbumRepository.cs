using bootcamp_framework.Infraestructure.Persistence;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Infrastructure.Persistence;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Persitence
{
    public class AlbumRepository : GenericRepository<Album>, IAlbumRepository
    {
        private ApplicationContext _applicationContext;
        public AlbumRepository(ApplicationContext context) : base(context)
        {
            _applicationContext = context;
        }
    }
}
