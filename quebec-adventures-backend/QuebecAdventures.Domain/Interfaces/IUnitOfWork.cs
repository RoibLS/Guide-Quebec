namespace QuebecAdventures.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
    }
}
