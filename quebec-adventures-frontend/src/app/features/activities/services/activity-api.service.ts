import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Activity, CreateActivityDto, CreateReviewDto } from '../../../core/models/activity.model';

@Injectable({ providedIn: 'root' })
export class ActivityApiService {
  private readonly API_URL = 'https://localhost:7274/api/activities';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Activity[]> {
    return this.http.get<Activity[]>(this.API_URL);
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
