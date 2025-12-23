using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuebecAdventures.Application.Dto;
using QuebecAdventures.Domain.Entities;
using QuebecAdventures.Domain.Enums;
using QuebecAdventures.Infrastructure.Persistence;

namespace QuebecAdventures.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
		private readonly ApplicationDbContext _context;

		public ActivitiesController(ApplicationDbContext context)
		{
			_context = context;
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<Activity>>> GetAll(
			[FromQuery] string? search,
			[FromQuery] ActivityType? type,
			[FromQuery] Region? region,
			[FromQuery] PriceRange? priceRange
		)
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

			if (type.HasValue)
			{
				query = query.Where(a => a.Type == type.Value);
			}

			if (region.HasValue)
			{
				query = query.Where(a => a.Region == region.Value);
			}

			if (priceRange.HasValue)
			{
				query = query.Where(a => a.PriceRange == priceRange.Value);
			}

			return await query.ToListAsync();
		}

		// GET: api/activities/{id}
		[HttpGet("{id:guid}")]
		public async Task<ActionResult<Activity>> GetById(Guid id)
		{
			var activity = await _context.Activities
			   .Include(a => a.Reviews)
			   .FirstOrDefaultAsync(a => a.Id == id);
			if (activity == null) return NotFound();
			return activity;
		}

		// POST: api/activities
		[HttpPost]
		public async Task<ActionResult<Activity>> Create(CreateActivityDto dto)
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

			return CreatedAtAction(nameof(GetById), new { id = activity.Id }, activity);
		}

		// PUT: api/activities/{id}
		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, CreateActivityDto dto)
		{
			var activity = await _context.Activities.FindAsync(id);
			if (activity == null) return NotFound();

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

			await _context.SaveChangesAsync();

			return NoContent();
		}

		// DELETE: api/activities/{id}
		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var activity = await _context.Activities.FindAsync(id);
			if (activity == null) return NotFound();

			_context.Activities.Remove(activity);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpPost("{id}/reviews")]
		public async Task<ActionResult<Review>> AddReview(Guid id, CreateReviewDto reviewDto)
		{
			var activity = await _context.Activities
				.Include(a => a.Reviews)
				.FirstOrDefaultAsync(a => a.Id == id);

			if (activity == null)
			{
				return NotFound("Activité introuvable");
			}

			var review = new Review
			{
				Id = Guid.NewGuid(),
				ActivityId = id,
				UserName = reviewDto.UserName,
				Rating = reviewDto.Rating,
				Comment = reviewDto.Comment,
				Date = DateTime.UtcNow,
				UserId = "Anonymous"
			};

			_context.Reviews.Add(review);

			var currentRatings = activity.Reviews.Select(r => r.Rating).ToList();
			currentRatings.Add(review.Rating);

			activity.Rating = currentRatings.Average();

			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetAll), new { id = id }, review);
		}
	}
}
