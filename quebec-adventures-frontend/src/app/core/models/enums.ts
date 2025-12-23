// Types d'activités
export enum ActivityType {
  RESTAURANT = 'Restaurant',
  BRUNCH = 'Brunch',
  SKI_RANDO = 'SkiRandonnee',
  SKI_ALPIN = 'SkiAlpin',
  SKI_FOND = 'SkiFond',
  PATINAGE = 'Patinage',
  RANDONNEE = 'Randonnee',
  CAMPING = 'Camping',
  VILLE = 'Ville',
  MUSEE = 'Musee',
  FESTIVAL = 'Festival',
  PARC = 'Parc',
  HEBERGEMENT = 'Hebergement',
  SPA = 'Spa',
  VELO = 'Velo',
  KAYAK = 'Kayak',
  ACTIVITENAUTIQUE = 'ActivitesNautiques',
  AUTRE = 'Autre',
}

// Saisons
export enum Season {
  HIVER = 'Hiver',
  PRINTEMPS = 'Printemps',
  ETE = 'Ete',
  AUTOMNE = 'Automne',
  TOUTE_ANNEE = 'TouteAnnee'
}

// Durée
export enum Duration {
  DEMI_JOURNEE = 'DemiJournee',
  JOURNEE = 'Journee',
  WEEKEND = 'Weekend',
  SEJOUR = 'Sejour'
}

// Régions géographiques
export enum Region {
  MONTREAL = 'Montreal',
  QUEBEC = 'Quebec',
  LAURENTIDES = 'Laurentides',
  CANTONS_EST = 'CantonsDeLest',
  CHARLEVOIX = 'Charlevoix',
  GASPESIE = 'Gaspesie',
  VERMONT = 'Vermont',
  NEW_YORK = 'NewYork',
  ONTARIO = 'Ontario',
  AUTRE = 'Autre'
}

// Gamme de prix
export enum PriceRange {
  GRATUIT = 'Gratuit',
  ECONOMIQUE = 'Economique',      // $
  MODERE = 'Modere',              // $$
  CHER = 'Cher',                  // $$$
  TRES_CHER = 'TresCher'         // $$$$
}
// Niveau de difficulté (pour activités sportives)
export enum Difficulty {
  FACILE = 'Facile',
  MOYEN = 'Moyen',
  DIFFICILE = 'Difficile',
  EXPERT = 'Expert',
  PASAPPLICABLE = 'PasApplicable'
}