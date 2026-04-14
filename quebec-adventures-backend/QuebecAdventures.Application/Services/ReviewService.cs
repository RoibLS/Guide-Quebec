using QuebecAdventures.Application.Dto;
using QuebecAdventures.Application.Interfaces;
using QuebecAdventures.Domain.Entities;
using QuebecAdventures.Domain.Interfaces;

namespace QuebecAdventures.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly IUnitOfWork _context;

        public ReviewService(IReviewRepository reviewRepository, IActivityRepository activityRepository, IUnitOfWork context)
        {
            _reviewRepository = reviewRepository;
            _activityRepository = activityRepository;
            _context = context;
        }

        public async Task<Review> AddReviewAsync(Guid id, CreateReviewDto reviewDto)
        {
            var activity = await _activityRepository.GetByIdIncludingReviewsAsync(id) ?? throw new KeyNotFoundException($"Activité {id} introuvable");
            var review = new Review
            {
                Id = Guid.NewGuid(),
                ActivityId = id,
                UserId = reviewDto.UserId,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                Date = DateTime.UtcNow
            };

            await _reviewRepository.AddReviewAsync(review);
            return review;
        }
    }
}
