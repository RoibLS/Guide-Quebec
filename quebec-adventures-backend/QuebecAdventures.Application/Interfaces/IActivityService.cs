using System;
using System.Collections.Generic;
using System.Text;
using QuebecAdventures.Application.Dto;
using QuebecAdventures.Domain.Entities;
using QuebecAdventures.Domain.Enums;

namespace QuebecAdventures.Application.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<Activity>> GetAllAsync(string? search, ActivityType? type, Region? region, PriceRange? priceRange);
        Task<Activity?> GetByIdAsync(Guid id);
        Task<Activity> CreateAsync(CreateActivityDto dto);
        Task UpdateAsync(Guid id, CreateActivityDto dto);
        Task DeleteAsync(Guid id);

        Task<Review> AddReviewAsync(Guid activityId, CreateReviewDto reviewDto);
        Task UpdateCoverImageAsync(Guid id, string imageUrl);
    }
}
