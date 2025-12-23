using QuebecAdventures.Domain.Enums;

namespace QuebecAdventures.Domain.Entities
{
    public class Activity
    {
		public Guid Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;

		// --- Enums fortement typés ---
		public ActivityType Type { get; set; }
		public Region Region { get; set; }
		public PriceRange? PriceRange { get; set; }
		public Difficulty? Difficulty { get; set; }


		// --- Listes (PostgreSQL Array de strings) ---
		// Correspond à Season[], Tags[], Images[] du front
		public List<string> Season { get; set; } = new();
		public List<string> Tags { get; set; } = new();
		public List<string> Images { get; set; } = new();

		// --- Localisation ---
		public string City { get; set; } = string.Empty;
		public double? DistanceFromMontreal { get; set; }

		// --- Infos Pratiques ---
		public string Duration { get; set; } = string.Empty; // Ex: "2h", "Journée"
		public string? Website { get; set; }

		// --- Médias ---
		public string CoverImage { get; set; } = string.Empty;

		// --- Scores & Avis ---
		public double Rating { get; set; }
		public bool IsFavorite { get; set; }

		public virtual List<Review> Reviews { get; set; } = new();


		// --- Metadata ---
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public string CreatedBy { get; set; } = "System";
    }
}
