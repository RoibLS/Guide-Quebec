import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, map, catchError, of } from 'rxjs';
import { Activity, ActivityFilters } from '../models/activity.model';
import { ActivityType, Region } from '../models/enums';

@Injectable({
  providedIn: 'root',
})
export class ActivityService {
  private readonly DATA_PATH = '/assets/data/activities.json';

  private activitiesSubject = new BehaviorSubject<Activity[]>([]);
  public activities$ = this.activitiesSubject.asObservable();

  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  constructor(private http: HttpClient) {}

  loadActivities(): Observable<Activity[]> {
    this.loadingSubject.next(true);

    return this.http.get<Activity[]>(this.DATA_PATH).pipe(
      map((activities) => {
        // Convertir les dates string en objets Date
        const processedActivities = activities.map((activity) => ({
          ...activity,
          createdAt: new Date(activity.createdAt),
          updatedAt: new Date(activity.updatedAt),
        }));

        this.activitiesSubject.next(processedActivities);
        this.loadingSubject.next(false);
        return processedActivities;
      }),
      catchError((error) => {
        console.error('Erreur lors du chargement des activités:', error);
        this.loadingSubject.next(false);
        return of([]);
      })
    );
  }

  getAllActivities(): Activity[] {
    return this.activitiesSubject.value;
  }

  getActivityById(id: string): Activity | undefined {
    return this.activitiesSubject.value.find((activity) => activity.id === id);
  }

  filterActivities(filters: ActivityFilters): Activity[] {
    let activities = this.getAllActivities();

    // Filtre par type
    if (filters.type && filters.type.length > 0) {
      activities = activities.filter((a) => filters.type!.includes(a.type));
    }

    // Filtre par saison
    if (filters.season && filters.season.length > 0) {
      activities = activities.filter((a) => a.season.some((s) => filters.season!.includes(s)));
    }

    // Filtre par durée
    if (filters.duration && filters.duration.length > 0) {
      activities = activities.filter((a) => filters.duration!.includes(a.duration));
    }

    // Filtre par région
    if (filters.region && filters.region.length > 0) {
      activities = activities.filter((a) => filters.region!.includes(a.region));
    }

    // Filtre par note minimale
    if (filters.minRating !== undefined) {
      activities = activities.filter((a) => a.rating >= filters.minRating!);
    }

    // Filtre favoris uniquement
    if (filters.favoritesOnly) {
      activities = activities.filter((a) => a.isFavorite);
    }

    // Filtre par terme de recherche
    if (filters.searchTerm && filters.searchTerm.trim() !== '') {
      const searchLower = filters.searchTerm.toLowerCase();
      activities = activities.filter(
        (a) =>
          a.title.toLowerCase().includes(searchLower) ||
          a.description.toLowerCase().includes(searchLower) ||
          a.city.toLowerCase().includes(searchLower) ||
          a.tags.some((tag) => tag.toLowerCase().includes(searchLower))
      );
    }
    return activities;
  }

  getFavorites(): Activity[] {
    return this.getAllActivities().filter((a) => a.isFavorite);
  }

  toggleFavorite(activityId: string): void {
    const activities = this.activitiesSubject.value;
    const updatedActivities = activities.map((activity) => {
      if (activity.id === activityId) {
        return { ...activity, isFavorite: !activity.isFavorite };
      }
      return activity;
    });
    this.activitiesSubject.next(updatedActivities);
  }

  getActivitiesByType(type: ActivityType): Activity[] {
    return this.getAllActivities().filter(a => a.type === type);
  }

  getActivitiesByRegion(region: Region): Activity[] {
    return this.getAllActivities().filter(a => a.region === region);
  }

  getTopRatedActivities(limit: number = 5): Activity[] {
    return [...this.getAllActivities()]
      .sort((a, b) => b.rating - a.rating)
      .slice(0, limit);
  }

    getActivitiesWithinDistance(maxDistance: number): Activity[] {
    return this.getAllActivities().filter(a => 
      a.distanceFromMontreal !== undefined && a.distanceFromMontreal <= maxDistance
    );
  }
}
