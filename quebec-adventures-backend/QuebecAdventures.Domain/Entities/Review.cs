using System;

namespace QuebecAdventures.Domain.Entities
{
	public class Review
	{
		public Guid Id { get; set; }

		// Infos de l'utilisateur (simplifié, pourrait être un User object plus tard)
		public string UserId { get; set; } = string.Empty;

        public double Rating { get; set; }  // double pour permettre les .5
        public string Comment { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
		
		// Clé étrangère vers Activity
		public Guid ActivityId { get; set; }
		public virtual Activity? Activity { get; set; }
	}
}
