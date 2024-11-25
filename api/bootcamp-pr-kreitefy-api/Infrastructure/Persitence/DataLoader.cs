using bootcamp_pr_kreitefy_api.Infrastructure.Persistence;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Persitence
{
    public class DataLoader
    {
        private readonly ApplicationContext _applicationContext;

        public DataLoader(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void LoadData()
        {
        }
    }
}
