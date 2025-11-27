import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
   stats = [
    { value: '150+', label: 'Activités', icon: '🎯' },
    { value: '4', label: 'Saisons', icon: '🍁' },
    { value: '5+', label: 'Régions', icon: '🗺️' }
  ];

   categories = [
    {
      title: 'Ski de randonnée',
      icon: '⛷️',
      description: 'Explorez les sommets enneigés du Québec',
      color: '#2c5f8d'
    },
    {
      title: 'Restaurants',
      icon: '🍽️',
      description: 'Les meilleures tables de Montréal',
      color: '#dc3545'
    },
    {
      title: 'Activités extérieures',
      icon: '🏕️',
      description: 'Randonnées, camping et aventures',
      color: '#28a745'
    },
    {
      title: 'Grandes villes',
      icon: '🏙️',
      description: 'Découvrez les villes de la côte Est',
      color: '#ffc107'
    }
  ];

  constructor(private router: Router) {}

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
}
