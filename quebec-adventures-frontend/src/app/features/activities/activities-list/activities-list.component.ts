import { Component, OnInit } from '@angular/core';
import { Observable, catchError, of } from 'rxjs';
import { Activity } from '../../../core/models/activity.model';
import { ActivityApiService } from '../services/activity-api.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-activities-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './activities-list.component.html',
  styleUrls: ['./activities-list.component.scss']
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
