import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ActivityService } from '../../core/services/activity.service';
import { Activity } from '../../core/models/activity.model';
import { ActivityCardComponent } from '../../shared/components/activity-card/activity-card.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, ActivityCardComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
  stats = [
    { value: '150+', label: 'Activités', icon: '🎯' },
    { value: '4', label: 'Saisons', icon: '🍁' },
    { value: '5+', label: 'Régions', icon: '🗺️' },
  ];

  categories = [
    {
      title: 'Recommandations Montréal',
      icon: '🍁',
      description: 'Nos coups de cœur dans la métropole',
      color: '#dc3545',
    },
    {
      title: 'Restaurants, brunchs et cafés',
      icon: '🍽️',
      description: 'Les meilleures tables du Québec',
      color: '#e67e22',
    },
    {
      title: 'Activités extérieures',
      icon: '🏕️',
      description: 'Randonnées, trails, ski et aventures',
      color: '#28a745',
    },
    {
      title: 'Grandes villes',
      icon: '🏙️',
      description: 'Découvrez les villes de la côte Est',
      color: '#3498db',
    },
  ];

  topActivities: Activity[] = [];

  constructor(private router: Router, public activityService: ActivityService) {}

  ngOnInit(): void {
    this.activityService.loadActivities().subscribe((activities) => {
      console.log('Activités chargées:', activities);
      this.topActivities = this.activityService.getTopRatedActivities(3);
      console.log('Top 3 activités:', this.topActivities);
    });
  }

  // Navigation vers la liste des activités (à implémenter plus tard)
  exploreActivities(): void {
    console.log('Navigation vers les activités');
    // this.router.navigate(['/activities']);
  }

  // Navigation vers une catégorie spécifique
  navigateToCategory(category: string): void {
    console.log(`Navigation vers: ${category}`);
    // this.router.navigate(['/activities'], { queryParams: { category } });
  }
  onActivityClick(activity: Activity): void {
    console.log('Activité cliquée:', activity);
  }

  onToggleFavorite(activityId: string): void {
    this.activityService.toggleFavorite(activityId);
    console.log('Favori togglé pour:', activityId);
  }
}
