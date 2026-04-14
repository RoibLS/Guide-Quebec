using System;
using System.Collections.Generic;
using System.Text;
using QuebecAdventures.Domain.Entities;
using QuebecAdventures.Domain.Enums;

namespace QuebecAdventures.Domain.Interfaces
{
    public interface IActivityRepository : IRepository<Activity>
    {
        Task<IEnumerable<Activity>> GetAllWithFiltersAsync(string? search, ActivityType? type, Region? region, PriceRange? priceRange);
        Task<Activity?> GetByIdIncludingReviewsAsync(Guid id);
        Task AddActivityAsync(Activity activity);
        Task DeleteActivityAsync(Activity id);
    }
}
