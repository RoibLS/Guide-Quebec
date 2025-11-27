// Types d'activités
export enum ActivityType {
  RESTAURANT = 'restaurant',
  BRUNCH = 'brunch',
  SKI_RANDO = 'ski_randonnee',
  SKI_ALPIN = 'ski_alpin',
  RANDONNEE = 'randonnee',
  CAMPING = 'camping',
  VILLE = 'visite_ville',
  MUSEE = 'musee',
  FESTIVAL = 'festival',
  PARC = 'parc_national',
  AUTRE = 'autre'
}

// Saisons
export enum Season {
  HIVER = 'hiver',
  PRINTEMPS = 'printemps',
  ETE = 'ete',
  AUTOMNE = 'automne',
  TOUTE_ANNEE = 'toute_annee'
}

// Durée
export enum Duration {
  DEMI_JOURNEE = 'demi_journee',
  JOURNEE = 'journee',
  WEEKEND = 'weekend',
  SEJOUR = 'sejour'
}

// Régions géographiques
export enum Region {
  MONTREAL = 'montreal',
  LAURENTIDES = 'laurentides',
  CANTONS_EST = 'cantons_est',
  CHARLEVOIX = 'charlevoix',
  QUEBEC_CITY = 'quebec_city',
  GASPESIE = 'gaspesie',
  VERMONT = 'vermont',
  NEW_YORK = 'new_york',
  AUTRE = 'autre'
}

// Niveau de difficulté (pour activités sportives)
export enum Difficulty {
  FACILE = 'facile',
  MOYEN = 'moyen',
  DIFFICILE = 'difficile',
  EXPERT = 'expert'
}