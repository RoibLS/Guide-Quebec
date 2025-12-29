using QuebecAdventures.Application.Dto;
using QuebecAdventures.Application.Interfaces;
using QuebecAdventures.Domain.Entities;
using QuebecAdventures.Domain.Enums;
using QuebecAdventures.Domain.Interfaces;

namespace QuebecAdventures.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IRepository<Activity> _context;

        public ActivityService(IRepository<Activity> context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Activity>> GetAllAsync(string? search, ActivityType? type, Region? region, PriceRange? priceRange)
        {
            var query = _context.Activities.Include(a => a.Reviews).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                string s = search.ToLower();
                query = query.Where(a =>
                    a.Title.ToLower().Contains(s) ||
                    a.Description.ToLower().Contains(s) ||
                    a.City.ToLower().Contains(s));
            }

            if (type.HasValue) query = query.Where(a => a.Type == type.Value);
            if (region.HasValue) query = query.Where(a => a.Region == region.Value);
            if (priceRange.HasValue) query = query.Where(a => a.PriceRange == priceRange.Value);

            return await query.ToListAsync();
        }

        public async Task<Activity?> GetByIdAsync(Guid id)
        {
            return await _context.Activities
               .Include(a => a.Reviews)
               .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Activity> CreateAsync(CreateActivityDto dto)
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
                DistanceFromMontreal = dto.DistanceFromMontreal,
                Season = dto.Season,
                Duration = dto.Duration,
                Tags = dto.Tags,
                Images = dto.Images,
                CoverImage = dto.CoverImage,
                Website = dto.Website,
                Rating = dto.Rating,
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
            return activity;
        }

        public async Task UpdateAsync(Guid id, CreateActivityDto dto)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null) throw new KeyNotFoundException($"Activité {id} introuvable");

            activity.Title = dto.Title;
            activity.Description = dto.Description;
            activity.Type = dto.Type;
            activity.Season = dto.Season;
            activity.Duration = dto.Duration;
            activity.Region = dto.Region;
            activity.City = dto.City;
            activity.DistanceFromMontreal = dto.DistanceFromMontreal;
            activity.Rating = dto.Rating;
            activity.PriceRange = dto.PriceRange;
            activity.CoverImage = dto.CoverImage;
            activity.Images = dto.Images;
            activity.Tags = dto.Tags;
            activity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null) throw new KeyNotFoundException($"Activité {id} introuvable");

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
        }

        public async Task<Review> AddReviewAsync(Guid activityId, CreateReviewDto reviewDto)
        {
            var activity = await _context.Activities
                .Include(a => a.Reviews)
                .FirstOrDefaultAsync(a => a.Id == activityId);

            if (activity == null) throw new KeyNotFoundException($"Activité {activityId} introuvable");

            var review = new Review
            {
                Id = Guid.NewGuid(),
                ActivityId = activityId,
                UserName = reviewDto.UserName,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                Date = DateTime.UtcNow,
                UserId = "Anonymous"
            };

            _context.Reviews.Add(review);

            // Recalcul de la moyenne
            var currentRatings = activity.Reviews.Select(r => r.Rating).ToList();
            currentRatings.Add(review.Rating);
            activity.Rating = currentRatings.Average();

            await _context.SaveChangesAsync();
            return review;
        }

        public async Task UpdateCoverImageAsync(Guid id, string imageUrl)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null) throw new KeyNotFoundException($"Activité {id} introuvable");

            activity.CoverImage = imageUrl;
            await _context.SaveChangesAsync();
        }
    }
}
