namespace QuebecAdventures.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // On exposera les repos spécifiques ici si besoin, ou on utilise GetRepository<T>
        Task<int> CompleteAsync();
    }
}
