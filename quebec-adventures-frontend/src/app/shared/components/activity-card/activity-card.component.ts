import { Component, Input, Output, EventEmitter, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Activity } from '../../../core/models/activity.model';
import { ImageUploadComponent } from '../image-upload/image-upload.component';
import { Difficulty, PriceRange, Duration } from '../../../core/models/enums';

@Component({
  selector: 'app-activity-card',
  standalone: true,
  imports: [CommonModule, ImageUploadComponent, RouterModule],
  templateUrl: './activity-card.component.html',
  styleUrls: ['./activity-card.component.scss']
})
export class ActivityCardComponent {
  @Input({ required: true }) activity!: Activity;
  @Input() showAdminActions = false;

  @Output() cardClick = new EventEmitter<Activity>();
  @Output() favoriteToggle = new EventEmitter<string>();
  @Output() deleteClick = new EventEmitter<string>();

  onCardClick(): void {
    this.cardClick.emit(this.activity);
  }

  onFavoriteClick(event: Event): void {
    event.stopPropagation(); // Empêche le clic de se propager à la carte
    this.favoriteToggle.emit(this.activity.id);
  }

  onImageSelected(file: File): void {
    console.log('Nouvelle image pour activité:', this.activity.title, file);
  }

  onDelete(event: Event): void {
    event.stopPropagation();
    this.deleteClick.emit(this.activity.id);
  }
  
  get priceLabel(): string {
    const map: Record<string, string> = {
      [PriceRange.GRATUIT]: 'Gratuit',
      [PriceRange.ECONOMIQUE]: '$',
      [PriceRange.MODERE]: '$$',
      [PriceRange.CHER]: '$$$',
      [PriceRange.TRES_CHER]: '$$$$'
    };
    return map[this.activity.priceRange || ''] || '';
  }
  
  get difficultyColor(): string {
    const map: Record<string, string> = {
      [Difficulty.FACILE]: '#48bb78',   // Vert
      [Difficulty.MOYEN]: '#ecc94b', // Jaune
      [Difficulty.DIFFICILE]: '#ed8936',   // Orange
      [Difficulty.EXPERT]: '#f56565' // Rouge
    };
    return map[this.activity.difficulty || ''] || '#cbd5e0';
  }
  
  get difficultyLabel(): string {
    const map: Record<string, string> = {
      [Difficulty.FACILE]: 'Facile',
      [Difficulty.MOYEN]: 'Modéré',
      [Difficulty.DIFFICILE]: 'Difficile',
      [Difficulty.EXPERT]: 'Extrême'
    };
    return map[this.activity.difficulty || ''] || '';
  }
  
  get durationLabel(): string {
     const map: Record<string, string> = {
      [Duration.DEMI_JOURNEE]: '½ Journée',
      [Duration.JOURNEE]: 'Journée',
      [Duration.WEEKEND]: 'Week-end',
      [Duration.SEJOUR]: 'Séjour'
    };
    return map[this.activity.duration] || '';
  }

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
  
  openExternalLink(url: string, event: Event): void {
    event.stopPropagation();
    window.open(url, '_blank');
  }


}
