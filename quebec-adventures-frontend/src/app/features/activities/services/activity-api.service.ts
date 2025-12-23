import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Activity, CreateActivityDto, CreateReviewDto } from '../../../core/models/activity.model';

export interface ActivityFilters {
  search?: string;
  type?: string;     // Ou ActivityType si tu utilises l'enum
  region?: string;   // Ou Region
  priceRange?: string;
}

@Injectable({ providedIn: 'root' })
export class ActivityApiService {
  private readonly API_URL = 'https://localhost:7274/api/activities';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Activity[]> {
    return this.http.get<Activity[]>(this.API_URL);
  }

  getActivities(filters?: ActivityFilters): Observable<Activity[]> {
  let params = new HttpParams();

  if (filters) {
    if (filters.search) params = params.set('search', filters.search);
    if (filters.type) params = params.set('type', filters.type);
    if (filters.region) params = params.set('region', filters.region);
    if (filters.priceRange) params = params.set('priceRange', filters.priceRange);
  }

  return this.http.get<Activity[]>(this.API_URL, { params });
  }

  getById(id: string): Observable<Activity> {
    return this.http.get<Activity>(`${this.API_URL}/${id}`);
  }

  create(dto: CreateActivityDto): Observable<Activity> {
    return this.http.post<Activity>(this.API_URL, dto);
  }

  update(id: string, dto: CreateActivityDto): Observable<void> {
    return this.http.put<void>(`${this.API_URL}/${id}`, dto);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.API_URL}/${id}`);
  }

  addReview(activityId: string, review: CreateReviewDto): Observable<any> {
    return this.http.post(`${this.API_URL}/${activityId}/reviews`, review);
  }
}
