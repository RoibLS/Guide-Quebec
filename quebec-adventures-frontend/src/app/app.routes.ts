import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ActivitiesListComponent } from './features/activities/activities-list/activities-list.component';
import { ActivityFormComponent } from './features/activities/activity-form/activity-form.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'activities', component: ActivitiesListComponent },
  { path: 'activities/new', component: ActivityFormComponent },
  { path: '**', redirectTo: '' }
];
