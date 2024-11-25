namespace bootcamp_framework.Domain.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        void Delete(long id);
        List<T> GetAll();
        T GetById(long id);

        T Insert(T entity);
        T Update(T entity);
    }
}