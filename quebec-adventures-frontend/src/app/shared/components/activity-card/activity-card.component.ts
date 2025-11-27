import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Activity } from '../../../core/models/activity.model';

@Component({
  selector: 'app-activity-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './activity-card.component.html',
  styleUrls: ['./activity-card.component.scss']
})
export class ActivityCardComponent {
  @Input({ required: true }) activity!: Activity;
  @Output() cardClick = new EventEmitter<Activity>();
  @Output() favoriteToggle = new EventEmitter<string>();

  onCardClick(): void {
    this.cardClick.emit(this.activity);
  }

  onFavoriteClick(event: Event): void {
    event.stopPropagation(); // Empêche le clic de se propager à la carte
    this.favoriteToggle.emit(this.activity.id);
  }

  // Afficher la saison principale (première de la liste)
  get mainSeason(): string {
    const seasonLabels: Record<string, string> = {
      'hiver': '❄️ Hiver',
      'printemps': '🌸 Printemps',
      'ete': '☀️ Été',
      'automne': '🍂 Automne',
      'toute_annee': '📅 Toute l\'année'
    };
    return seasonLabels[this.activity.season[0]] || '';
  }

  // Afficher le type d'activité
  get activityTypeLabel(): string {
    const typeLabels: Record<string, string> = {
      'restaurant': '🍽️ Restaurant',
      'brunch': '🥞 Brunch',
      'ski_randonnee': '⛷️ Ski de rando',
      'ski_alpin': '🎿 Ski alpin',
      'randonnee': '🥾 Randonnée',
      'camping': '⛺ Camping',
      'visite_ville': '🏙️ Ville',
      'musee': '🖼️ Musée',
      'festival': '🎉 Festival',
      'parc_national': '🏞️ Parc national',
      'autre': '📍 Autre'
    };
    return typeLabels[this.activity.type] || '📍';
  }

  // Formater la distance
  get distanceLabel(): string {
    if (!this.activity.distanceFromMontreal) return '';
    const distance = this.activity.distanceFromMontreal;
    
    if (distance === 0) return '📍 Montréal';
    if (distance < 50) return `🚗 ${distance} km`;
    if (distance < 150) return `🛣️ ${distance} km`;
    return `🗺️ ${distance} km`;
  }
}
