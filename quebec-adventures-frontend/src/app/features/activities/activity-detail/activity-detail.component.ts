import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivityApiService } from '../services/activity-api.service';
import { Activity, CreateReviewDto } from '../../../core/models/activity.model';
import { Observable, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-activity-detail',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './activity-detail.component.html',
  styleUrls: ['./activity-detail.component.scss']
})
export class ActivityDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private activityApi = inject(ActivityApiService);
  private fb = inject(FormBuilder);

  activity$: Observable<Activity> | null = null;
  activityId: string | null = null;

  // Formulaire d'avis
  reviewForm = this.fb.group({
    userName: ['', Validators.required],
    rating: [10, [Validators.required, Validators.min(1), Validators.max(10)]],
    comment: ['', [Validators.required, Validators.minLength(3)]]
  });

  ngOnInit() {
    // Récupère l'ID et charge l'activité
    this.activity$ = this.route.paramMap.pipe(
      switchMap(params => {
        this.activityId = params.get('id');
        return this.activityApi.getById(this.activityId!);
      })
    );
  }

  submitReview() {
    if (this.reviewForm.invalid || !this.activityId) return;

    const review: CreateReviewDto = {
      userName: this.reviewForm.value.userName!,
      rating: this.reviewForm.value.rating!,
      comment: this.reviewForm.value.comment!
    };

    this.activityApi.addReview(this.activityId, review).subscribe({
      next: () => {
        alert('Avis ajouté !');
        this.reviewForm.reset({ rating: 10 });
        // Recharger l'activité pour voir le nouvel avis (si le back renvoie les avis)
        // this.activity$ = ... (reload logic)
        window.location.reload(); // Solution brute pour l'instant
      },
      error: (err) => console.error('Erreur review', err)
    });
  }
}
