import { ActivityType, Season, Duration, Region, Difficulty, PriceRange } from './enums';


// Interface principale pour une activité
export interface Activity {
  id: string;
  title: string;
  description: string;
  type: ActivityType;
  season: Season[];
  duration: Duration;
  region: Region;
  
  // Localisation
  address?: string;
  city: string;
  
  // Distance depuis Montréal (en km)
  distanceFromMontreal?: number;
  
  // Évaluation
  rating: number; // Note sur 10
  isFavorite: boolean;
  reviews: Review[];
  
  // Médias
  images: string[];
  coverImage: string;
  
  // Informations pratiques
  website?: string;
  phone?: string;
  priceRange?: PriceRange;
  difficulty?: Difficulty; // Pour activités sportives
  
  // Métadonnées
  createdAt: Date;
  updatedAt: Date;
  createdBy: string; // ID de l'utilisateur
  tags: string[];
}

// Interface pour les avis/commentaires
export interface Review {
  id: string;
  activityId: string;
  userId: string;
  userName: string;
  rating: number; // Note sur 10
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
  season: Season[];
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
}

export interface CreateReviewDto {
  userName: string;
  rating: number;
  comment: string;
}
