namespace bootcamp_framework.Domain.Persistence
{
    public interface IUnitOfWork
    {
        IWork Init();
    }
}
