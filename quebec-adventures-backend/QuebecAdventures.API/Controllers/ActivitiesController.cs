using Microsoft.AspNetCore.Mvc;
using QuebecAdventures.Application.Dto;
using QuebecAdventures.Application.Interfaces;
using QuebecAdventures.Domain.Entities;
using QuebecAdventures.Domain.Enums;

namespace QuebecAdventures.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly IReviewService _reviewService;
        private readonly IImageService _imageService;

        public ActivitiesController(IActivityService activityService, IImageService imageService, IReviewService reviewService)
        {
            _activityService = activityService;
            _imageService = imageService;
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] ActivityType? type,
            [FromQuery] Region? region,
            [FromQuery] PriceRange? priceRange)
        {
            var activities = await _activityService.GetAllAsync(search, type, region, priceRange);
            return Ok(activities);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Activity>> GetById(Guid id)
        {
            var activity = await _activityService.GetByIdAsync(id);
            if (activity == null) return NotFound();
            return Ok(activity);
        }

        // --- NOUVEAU : Endpoint pour servir l'image ---
        [HttpGet("{id:guid}/cover")]
        public async Task<IActionResult> GetCoverImage(Guid id)
        {
            var activity = await _activityService.GetByIdAsync(id);
            
            if (activity == null || activity.CoverImageContent == null)
            {
                // Retourne une image par défaut ou 404
                return NotFound("Aucune image de couverture");
            }

            return File(activity.CoverImageContent, activity.CoverImageMimeType ?? "image/jpeg");
        }
        // ----------------------------------------------

        [HttpPost]
        public async Task<ActionResult<Activity>> Create(CreateActivityDto dto)
        {
            var activity = await _activityService.AddActivityAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = activity.Id }, activity);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, CreateActivityDto dto)
        {
            try
            {
                await _activityService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _activityService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/reviews")]
        public async Task<ActionResult<Review>> AddReview(Guid id, CreateReviewDto reviewDto)
        {
            try
            {
                var review = await _reviewService.AddReviewAsync(id, reviewDto);
                return CreatedAtAction(nameof(GetById), new { id = id }, review);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Activité introuvable");
            }
        }

        [HttpPost("{id}/upload-cover")]
        public async Task<IActionResult> UploadCover(Guid id, IFormFile file)
        {
            try
            {
                // 1. Conversion en bytes via ImageService
                var (content, contentType) = await _imageService.ProcessImageAsync(file);
                
                // 2. Sauvegarde en DB via ActivityService
                await _activityService.UpdateCoverImageAsync(id, content, contentType);

                // 3. Retourne l'URL de l'endpoint d'image pour mise à jour du front
                var imageUrl = $"/api/activities/{id}/cover";
                return Ok(new { url = imageUrl });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Activité introuvable");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
