import { Component, OnInit } from '@angular/core';
import { Observable, catchError, of } from 'rxjs';
import { Activity } from '../../core/models/activity.model';
import { ActivityApiService } from './services/activity-api.service';

@Component({
  selector: 'app-activities-list',
  template: `
    <ng-container *ngIf="!error; else errorTpl">
      <div *ngIf="activities$ | async as activities; else loadingTpl">
        <h2>Liste des activités</h2>
        <div *ngFor="let activity of activities" class="activity-card">
          <h3>{{ activity.title }}</h3>
          <p>{{ activity.description }}</p>
          <span>Région : {{ activity.region }}</span>
          <span>Ville : {{ activity.city }}</span>
          <span>Note : {{ activity.rating }}</span>
        </div>
      </div>
    </ng-container>
    <ng-template #loadingTpl>
      <p>Chargement des activités...</p>
    </ng-template>
    <ng-template #errorTpl>
      <p>Erreur lors du chargement des activités.</p>
    </ng-template>
  `,
  styles: [`.activity-card { border: 1px solid #ccc; margin: 1em 0; padding: 1em; border-radius: 8px;}`]
})
export class ActivitiesListComponent implements OnInit {
  activities$!: Observable<Activity[]>;
  error = false;

  constructor(private activityApi: ActivityApiService) {}

  ngOnInit(): void {
    this.activities$ = this.activityApi.getAll().pipe(
      catchError(() => {
        this.error = true;
        return of([]);
      })
    );
  }
}