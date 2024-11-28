using bootcamp_framework.Infraestructure.Persistence;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Infrastructure.Persistence;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Persitence
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private ApplicationContext _applicationContext;
        public UserRepository(ApplicationContext context) : base(context)
        {
            _applicationContext = context;
        }
    }
}
