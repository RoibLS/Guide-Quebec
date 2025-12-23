import { Component, OnInit } from '@angular/core';
import { Observable, catchError, of } from 'rxjs';
import { Activity } from '../../../core/models/activity.model';
import { ActivityApiService } from '../services/activity-api.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-activities-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
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

  onDelete(id: string, event: Event) {
    event.stopPropagation(); // Empêche le clic de traverser si la carte est cliquable
    
    if (confirm('Êtes-vous sûr de vouloir supprimer cette activité ?')) {
      this.activityApi.delete(id).subscribe({
        next: () => {
          // Astuce pour rafraîchir la liste sans recharger la page :
          // On filtre l'observable ou on recharge
          this.refreshList(); 
        },
        error: (err) => console.error('Erreur suppression', err)
      });
    }
  }

  private refreshList() {
  this.activities$ = this.activityApi.getAll().pipe(
    catchError(err => {
      this.error = true;
      return of([]);
    })
  );
  }
}
