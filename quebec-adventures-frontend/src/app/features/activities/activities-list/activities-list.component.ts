import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router'; // <--- Ajoute Router ici
import { ActivityApiService } from '../services/activity-api.service';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-activities-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './activities-list.component.html',
  styleUrls: ['./activities-list.component.scss']
})
export class ActivitiesListComponent implements OnInit {
  private activityApi = inject(ActivityApiService);
  private router = inject(Router); // <--- INJECTION DU ROUTER

  activities$ = this.activityApi.getAll().pipe(
    catchError(err => {
      this.error = true;
      return of([]);
    })
  );
  error = false;

  ngOnInit() {}

  // --- LA MÉTHODE À AJOUTER ---
  goToDetail(id: string) {
    this.router.navigate(['/activities', id]);
  }

  // Ta méthode existante
  onDelete(id: string, event: Event) {
    event.stopPropagation();
    if (confirm('Êtes-vous sûr de vouloir supprimer cette activité ?')) {
      this.activityApi.delete(id).subscribe({
        next: () => {
          // Refresh simple (ou recharger l'observable)
          window.location.reload(); 
        },
        error: (err) => console.error('Erreur suppression', err)
      });
    }
  }
}
