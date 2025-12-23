import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ActivityApiService } from '../services/activity-api.service';
import { of, switchMap } from 'rxjs';
import { ActivityType, Region, PriceRange, Difficulty } from '../../../core/models/enums';


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

  isEditMode = false;
  activityId: string | null = null;

  // Options correspondants aux Enums C# (PascalCase)
  types = Object.values(ActivityType);
  regions = Object.values(Region);
  prices = Object.values(PriceRange);
  difficulties = Object.values(Difficulty);

  form = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
    type: ['ParcNational', Validators.required],
    region: ['Montreal', Validators.required],
    city: ['', Validators.required],
    priceRange: ['Modere'],
    difficulty: ['Facile'], // Optionnel, selon le type d'activité
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
        // Remplir le formulaire en cas d'édition
        this.form.patchValue({
          title: activity.title,
          description: activity.description,
          type: activity.type as string, // Cast simple car le back renvoie la string de l'enum
          region: activity.region as string,
          city: activity.city,
          priceRange: activity.priceRange as string,
          difficulty: activity.difficulty as string,
          rating: activity.rating,
          coverImage: activity.coverImage,
          website: activity.website,
          duration: activity.duration
        });
      }
    });
  }

  onSubmit() {
    if (this.form.invalid) return;

    const val = this.form.value;

    // Construction du DTO
    const dto: any = {
      title: val.title!,
      description: val.description!,
      type: val.type,
      region: val.region,
      city: val.city!,
      priceRange: val.priceRange,
      difficulty: val.difficulty, // Peut être null côté back si non pertinent
      
      rating: val.rating || 0,
      coverImage: val.coverImage || '',
      website: val.website || null,
      duration: val.duration || '2h',

      // Valeurs par défaut pour les champs non gérés dans ce formulaire simple
      season: ['Ete'], 
      distanceFromMontreal: 0,
      images: [],
      tags: []
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

  onCancel() {
    this.router.navigate(['/activities']);
  }
}
