using bootcamp_framework.Infraestructure.Persistence;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Infrastructure.Persistence;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Persitence
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private ApplicationContext _applicationContext;
        public RoleRepository(ApplicationContext context) : base(context)
        {
            _applicationContext = context;
        }
    }
}
