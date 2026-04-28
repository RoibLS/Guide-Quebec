import { ActivityType, Season, Duration, Region, Difficulty, PriceRange } from './enums';

export interface Activity {
  id: string;
  title: string;
  description: string;
  type: ActivityType;
  region: Region;

  // Localisation
  city: string;
  address?: string;
  distanceFromMontreal?: number;

  //Statut journal
  isVisited?: boolean;
  visitedAt?: Date;
  isFavorite: boolean;
  wishlistNote?: string;

  //Caractéristiques
  season: Season;
  duration: Duration;
  priceRange?: PriceRange;
  difficulty?: Difficulty;

  // Évaluation
  rating: number; // Note sur 10
  reviews: Review[];

  // Médias
  coverImage: string;
  images: string[];

  // Informations pratiques
  website?: string;
  tags: string[];

  // Métadonnées
  createdBy: string;
  createdAt: Date;
  updatedAt: Date;
}

// Interface pour les avis/commentaires
export interface Review {
  id: string;
  activityId: string;
  userId: string;
  userName: string;
  rating: number;
  comment: string;
  date: Date;
  images?: string[];
}

// Interface pour les filtres
export interface ActivityFilters {
  type?: ActivityType[];
  season?: Season[];
  duration?: Duration[];
  region?: Region[];
  minRating?: number;
  favoritesOnly?: boolean;
  searchTerm?: string;
}

// Interface pour créer/modifier une activité (DTO)
export interface CreateActivityDto {
  title: string;
  description: string;
  type: ActivityType;
  season: Season;
  duration: Duration;
  region: Region;
  city: string;
  address?: string;
  website?: string;
  phone?: string;
  priceRange?: PriceRange;
  difficulty?: Difficulty;
  tags: string[];
  images: string[];
  coverImage: string;
  distanceFromMontreal? : number;
}

export interface CreateReviewDto {
  userName: string;
  rating: number;
  comment: string;
}
