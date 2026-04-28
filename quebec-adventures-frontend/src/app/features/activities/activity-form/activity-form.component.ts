import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ActivityApiService } from '../services/activity-api.service';
import { debounceTime, distinctUntilChanged, of, Subject, switchMap } from 'rxjs';
import {ActivityType, Region, PriceRange, Difficulty, Duration, Season} from '../../../core/models/enums';
import {GeocodingResult, GeocodingService} from '../../../core/services/geocoding.service';
import {CreateActivityDto} from '../../../core/models/activity.model';


@Component({
  selector: 'app-activity-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './activity-form.component.html',
  styleUrls: ['./activity-form.component.scss']
})
export class ActivityFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private activityApi = inject(ActivityApiService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private geocoding = inject(GeocodingService);

  isEditMode = false;
  activityId: string | null = null;

  citySuggestions: GeocodingResult[] = [];
  showSuggestions = false;
  private searchTerms = new Subject<string>();

  types = Object.values(ActivityType);
  regions = Object.values(Region);
  prices = Object.values(PriceRange);
  difficulties = Object.values(Difficulty);
  duration = Object.values(Duration);

  form = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
    type: ['ParcNational', Validators.required],
    region: ['Montreal', Validators.required],
    city: ['', Validators.required],
    priceRange: ['Modere'],
    difficulty: ['Facile'],
    season: ['Ete', Validators.required],
    rating: [0, [Validators.min(0), Validators.max(10)]],
    coverImage: [''],
    website: [''],
    duration: ['2h']
  });

  ngOnInit() {
    this.route.paramMap.pipe(
      switchMap(params => {
        const id = params.get('id');
        if (id) {
          this.isEditMode = true;
          this.activityId = id;
          return this.activityApi.getById(id);
        }
        return of(null);
      })
    ).subscribe(activity => {
      if (activity) {
        this.form.patchValue({
          title: activity.title,
          description: activity.description,
          type: activity.type as string,
          region: activity.region as string,
          city: activity.city,
          priceRange: activity.priceRange as string,
          season: activity.season as string,
          difficulty: activity.difficulty as string,
          rating: activity.rating,
          coverImage: activity.coverImage,
          website: activity.website,
          duration: activity.duration
        });
      }
    });

    // Gestion de la recherche de villes
    // Setup de l'autocomplete
    this.searchTerms.pipe(
      debounceTime(300), // Attendre 300ms après la frappe
      distinctUntilChanged(), // Ne pas chercher si le texte est le même
      switchMap(term => {
        if (term.length < 3) return of([]); // Chercher seulement si > 2 lettres
        return this.geocoding.searchCity(term);
      })
    ).subscribe(results => {
      this.citySuggestions = results;
      this.showSuggestions = true;
    });
  }

  onSubmit() {
    if (this.form.invalid) return;

    const val = this.form.value;

    const dto: CreateActivityDto = {
      title: val.title!,
      description: val.description!,
      type: val.type as ActivityType,
      region: val.region as Region,
      city: val.city!,
      priceRange: val.priceRange as PriceRange,
      difficulty: val.difficulty as Difficulty,
      season: val.season as Season,
      distanceFromMontreal: 0,
      images: [],
      tags: [],
      coverImage: val.coverImage || '',
      website: val.website || undefined,
      duration: val.duration as Duration
    };

    if (this.isEditMode && this.activityId) {
      this.activityApi.update(this.activityId, dto).subscribe({
        next: () => this.router.navigate(['/activities']),
        error: (err) => console.error('Erreur update', err)
      });
    } else {
      this.activityApi.create(dto).subscribe({
        next: () => this.router.navigate(['/activities']),
        error: (err) => console.error('Erreur create', err)
      });
    }
  }

  onCityInput(event: Event) {
    const val = (event.target as HTMLInputElement).value;
    this.searchTerms.next(val);
  }

  selectCity(suggestion: any) {
    this.form.patchValue({
      city: suggestion.name,
    });

    this.showSuggestions = false;
    this.citySuggestions = [];
  }

  closeSuggestions() {
    setTimeout(() => this.showSuggestions = false, 200);
  }

  onCancel() {
    this.router.navigate(['/activities']);
  }
}
