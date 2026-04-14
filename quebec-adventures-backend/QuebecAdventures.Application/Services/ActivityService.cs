using QuebecAdventures.Application.Dto;
using QuebecAdventures.Application.Interfaces;
using QuebecAdventures.Domain.Entities;
using QuebecAdventures.Domain.Enums;
using QuebecAdventures.Domain.Interfaces;

namespace QuebecAdventures.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivityService(IActivityRepository activityRepo, IUnitOfWork unitOfWork)
        {
            _activityRepository = activityRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Activity>> GetAllAsync(string? search, ActivityType? type, Region? region, PriceRange? priceRange)
        {
            return await _activityRepository.GetAllWithFiltersAsync(search, type, region, priceRange);
        }

        public async Task<Activity?> GetByIdAsync(Guid id)
        {
            return await _activityRepository.GetByIdIncludingReviewsAsync(id);
        }

        public async Task<Activity> AddActivityAsync(CreateActivityDto dto)
        {
            var activity = new Activity
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Type = dto.Type,
                Region = dto.Region,
                PriceRange = dto.PriceRange,
                Difficulty = dto.Difficulty,
                City = dto.City,
                Address = dto.Address,
                DistanceFromMontreal = dto.DistanceFromMontreal,

                IsVisited = dto.IsVisited,
                VisitedAt = dto.VisitedAt,
                IsFavorite = dto.IsFavorite,
                WishlistNote = dto.WishlistNote,

                Seasons = dto.Seasons,
                Duration = dto.Duration,
                Tags = dto.Tags,
                Images = dto.Images,
                CoverImage = dto.CoverImage,
                Website = dto.Website,
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _activityRepository.AddActivityAsync(activity);
            return activity;
        }

        public async Task UpdateAsync(Guid id, CreateActivityDto dto)
        {
            var activity = await _activityRepository.GetByIdAsync(id)
                           ?? throw new KeyNotFoundException($"Activité {id} introuvable");

            activity.Title = dto.Title;
            activity.Description = dto.Description;
            activity.Type = dto.Type;
            activity.Region = dto.Region;
            activity.PriceRange = dto.PriceRange;
            activity.Difficulty = dto.Difficulty;
            activity.City = dto.City;
            activity.Address = dto.Address;
            activity.DistanceFromMontreal = dto.DistanceFromMontreal;

            activity.IsVisited = dto.IsVisited;
            activity.VisitedAt = dto.VisitedAt;
            activity.IsFavorite = dto.IsFavorite;
            activity.WishlistNote = dto.WishlistNote;

            activity.Seasons = dto.Seasons;
            activity.Duration = dto.Duration;
            activity.Tags = dto.Tags;
            activity.Images = dto.Images;
            activity.CoverImage = dto.CoverImage;
            activity.Website = dto.Website;
            activity.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var activity = await _activityRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Activité {id} introuvable");

            await _activityRepository.DeleteActivityAsync(activity);
        }
    }
}
