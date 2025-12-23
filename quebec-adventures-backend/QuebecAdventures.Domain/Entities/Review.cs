using System;

namespace QuebecAdventures.Domain.Entities
{
	public class Review
	{
		public Guid Id { get; set; }

		// Infos de l'utilisateur (simplifié, pourrait être un User object plus tard)
		public string UserId { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;

		public int Rating { get; set; } // Note sur 10
		public string Comment { get; set; } = string.Empty;
		public DateTime Date { get; set; } = DateTime.UtcNow;

		// Clé étrangère vers Activity
		public Guid ActivityId { get; set; }

		public virtual Activity? Activity { get; set; }
	}
}
