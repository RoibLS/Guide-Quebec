import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ActivitiesListComponent } from './features/activities/activities-list/activities-list.component';
import { ActivityFormComponent } from './features/activities/activity-form/activity-form.component';
import { ActivityDetailComponent } from './features/activities/activity-detail/activity-detail.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'activities', component: ActivitiesListComponent },
  { path: 'activities/new', component: ActivityFormComponent },
  { path: 'activities/:id/edit', component: ActivityFormComponent },
  { path: 'activities/:id', component: ActivityDetailComponent },
  { path: '**', redirectTo: '' }
];
