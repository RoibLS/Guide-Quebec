using QuebecAdventures.Domain.Enums;

namespace QuebecAdventures.Domain.Entities
{
    public class Activity
    {
        public Guid Id { get; set; }

        // === INFORMATIONS PRINCIPALES ===
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ActivityType Type { get; set; }
        public Region Region { get; set; }


        // === LOCALISATION ===
        public string City { get; set; } = string.Empty;
        public string? Address { get; set; }
        public double? DistanceFromMontreal { get; set; }


        // === STATUT JOURNAL / WISHLIST ===
        public bool IsVisited { get; set; } = false;
        public DateTime? VisitedAt { get; set; }
        public bool IsFavorite { get; set; } = false;
        public string? WishlistNote { get; set; }

        // === CARACTÉRISTIQUES ===
        public List<Season> Seasons { get; set; } = new();  // Renommé et typé correctement
        public Duration Duration { get; set; }
        public PriceRange? PriceRange { get; set; }
        public Difficulty? Difficulty { get; set; }

        // === MÉDIAS ===
        public string CoverImage { get; set; } = string.Empty;  // URL externe uniquement
        public List<string> Images { get; set; } = new();

        // === INFOS PRATIQUES ===
        public string? Website { get; set; }
        public List<string> Tags { get; set; } = new();

        // === MÉTADONNÉES ===
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // === RELATIONS ===
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        // === PROPRIÉTÉ CALCULÉE (pas en base) ===
        // Calcule la note moyenne depuis les reviews existantes
        public double? AverageRating => Reviews.Any() 
            ? Reviews.Average(r => r.Rating) 
            : null;
    }
}
