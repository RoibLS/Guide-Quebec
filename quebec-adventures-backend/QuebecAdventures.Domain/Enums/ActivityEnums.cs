namespace QuebecAdventures.Domain.Enums
{
	public enum ActivityType
	{
		Restaurant,
		Brunch,
		SkiRandonnee,
		SkiAlpin,
		SkiFond,
		Randonnee,
		Camping,
		Ville,
		Musee,
		Festival,
		Parc,
		Hebergement,
		Spa,
		Velo,
		Kayak,
		ActivitesNautiques,
		Patinage,
		Autre
	}

	public enum Region
	{
		Montreal,
		Quebec,
		Laurentides,
		CantonsDeLest,
		Charlevoix,
		Gaspesie,
		Vermont,
		NewYork,
		Ontario,
		Mauricie,
		Autre
	}

	public enum PriceRange
	{
		Gratuit,
		Economique, // $
		Modere,     // $$
		Cher,       // $$$
		TresCher    // $$$$
	}

	public enum Difficulty
	{
		Facile,
		Moyen,
		Difficile,
		Expert,
		PasApplicable
	}

	public enum Duration
	{
		DemiJournee,
		Journee,
		Weekend,
		Sejour
	}
}
