using System.Linq.Expressions;

namespace QuebecAdventures.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        void Remove(T entity);
        // Note: Update est souvent implicite avec le tracking EF Core, 
        // ou on peut ajouter une méthode Update(T entity)
    }
}
