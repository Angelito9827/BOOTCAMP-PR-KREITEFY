using bootcamp_framework.Infraestructure.Persistence;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Infrastructure.Persistence;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Persitence
{
    public class StyleRepository : GenericRepository<Style>, IStyleRepository
    {
        private ApplicationContext _applicationContext;
        public StyleRepository(ApplicationContext context) : base(context)
        {
            _applicationContext = context;
        }
    }
}
