using System.ComponentModel.DataAnnotations;
using QuebecAdventures.Domain.Enums;

namespace QuebecAdventures.Domain.Entities
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ActivityType Type { get; set; }
        public Region Region { get; set; }
        public PriceRange? PriceRange { get; set; }
        public Difficulty? Difficulty { get; set; }
        public string City { get; set; } = string.Empty;
        public double? DistanceFromMontreal { get; set; }
        
        public List<string> Season { get; set; } = new();
        public Duration Duration { get; set; }
        public List<string> Tags { get; set; } = new();
        public List<string> Images { get; set; } = new();
        
        // --- CHANGEMENT ICI : Stockage DB ---
        // On garde CoverImage (string) temporairement si besoin pour la migration,
        // mais pour le stockage binaire :
        public byte[]? CoverImageContent { get; set; } 
        public string? CoverImageMimeType { get; set; } // ex: image/jpeg
        
        // Cette propriété calculée servira d'URL pour le frontend
        // Elle pointera vers notre endpoint API qui servira l'image
        public string? CoverImage => Id != Guid.Empty ? $"/api/activities/{Id}/cover" : null;
        // ------------------------------------

        public string? Website { get; set; }
        public double Rating { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
