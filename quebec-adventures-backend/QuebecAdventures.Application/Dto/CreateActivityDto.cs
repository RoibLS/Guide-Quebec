using System.ComponentModel.DataAnnotations;
using QuebecAdventures.Domain.Enums;

namespace QuebecAdventures.Application.Dto;

public class CreateActivityDto
{
    [Required] public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ActivityType Type { get; set; }
    public Region Region { get; set; }
    public PriceRange? PriceRange { get; set; }
    public Difficulty? Difficulty { get; set; }

    // === LOCALISATION ===
    public string City { get; set; } = string.Empty;
    public string? Address { get; set; }
    public double? DistanceFromMontreal { get; set; }  // double? au lieu de int

    // === STATUT JOURNAL / WISHLIST ===
    public bool IsVisited { get; set; } = false;
    public DateTime? VisitedAt { get; set; }
    public bool IsFavorite { get; set; } = false;
    public string? WishlistNote { get; set; }

    // === CARACTÉRISTIQUES ===
    public List<Season> Seasons { get; set; } = [];
    public Duration Duration { get; set; }

    // === MÉDIAS ===
    public string CoverImage { get; set; } = string.Empty;
    public List<string> Images { get; set; } = [];
    public List<string> Tags { get; set; } = [];
    public string? Website { get; set; }
}
