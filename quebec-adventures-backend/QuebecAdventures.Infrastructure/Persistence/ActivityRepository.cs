using Microsoft.EntityFrameworkCore;
using QuebecAdventures.Domain.Enums;
using QuebecAdventures.Domain.Interfaces;
using Activity = QuebecAdventures.Domain.Entities.Activity;

namespace QuebecAdventures.Infrastructure.Persistence
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        public ActivityRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Activity>> GetAllWithFiltersAsync(string? search, ActivityType? type, Region? region, PriceRange? priceRange)
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

        public async Task<Activity?> GetByIdIncludingReviewsAsync(Guid id)
        {
            return await _context.Activities
                         .Include(a => a.Reviews)
                         .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddActivityAsync(Activity activity)
        {
            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteActivityAsync(Activity activity)
        {
            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();
        }

        public void UpdateAverageRating(Activity activity)
        {
            var currentRatings = activity.Reviews.Select(r => r.Rating).ToList();
            activity.Rating = currentRatings.Average();
        }
    }
}
