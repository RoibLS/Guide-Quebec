import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router'; // <--- Ajoute Router ici
import { ActivityApiService, ActivityFilters } from '../services/activity-api.service';
import { catchError, debounceTime, distinctUntilChanged, of, startWith, switchMap } from 'rxjs';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { ActivityType, PriceRange, Region } from '../../../core/models/enums';

@Component({
  selector: 'app-activities-list',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './activities-list.component.html',
  styleUrls: ['./activities-list.component.scss']
})
export class ActivitiesListComponent implements OnInit {
  private activityApi = inject(ActivityApiService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  types = Object.values(ActivityType);
  regions = Object.values(Region);
  prices = Object.values(PriceRange);

  filterForm = this.fb.group({
    search: [''],
    type: [''],
    region: [''],
    priceRange: ['']
  });

 activities$ = this.filterForm.valueChanges.pipe(
    startWith(this.filterForm.value), // Lance une recherche initiale vide
    debounceTime(300), // Attend 300ms que l'utilisateur finisse de taper
    distinctUntilChanged(), // Évite les doublons
    switchMap(filters => {
      // Nettoyage : on enlève les champs vides ('') pour ne pas envoyer ?type=&region=
      const cleanFilters: ActivityFilters = {};
      if (filters.search) cleanFilters.search = filters.search;
      if (filters.type) cleanFilters.type = filters.type as string;
      if (filters.region) cleanFilters.region = filters.region as string;
      if (filters.priceRange) cleanFilters.priceRange = filters.priceRange as string;

      return this.activityApi.getActivities(cleanFilters).pipe(
        catchError(() => {
          this.error = true;
          return of([]);
        })
      );
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
      this.activityApi.delete(id).subscribe(() => {
         this.filterForm.updateValueAndValidity({ onlySelf: false, emitEvent: true });
      });
    }
  }
}
