import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Activity, CreateActivityDto } from '../../../core/models/activity.model';
@Injectable({ providedIn: 'root' })
export class ActivityApiService {
  private readonly API_URL = 'http://localhost:5237/api/activities';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Activity[]> {
    return this.http.get<Activity[]>(this.API_URL);
  }

  getById(id: string): Observable<Activity> {
    return this.http.get<Activity>(`${this.API_URL}/${id}`);
  }

  create(activity: CreateActivityDto): Observable<Activity> {
    return this.http.post<Activity>(this.API_URL, activity);
  }

  update(id: string, activity: CreateActivityDto): Observable<void> {
    return this.http.put<void>(`${this.API_URL}/${id}`, activity);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.API_URL}/${id}`);
  }
}
