using QuebecAdventures.Domain.Entities;

namespace QuebecAdventures.Domain.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task AddReviewAsync(Review review);
    }
}
