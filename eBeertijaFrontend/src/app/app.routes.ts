import { Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { NarudzbaComponent } from './narudzba/narudzba.component';

export const routes: Routes = [
  {
      path: '',
      redirectTo: '/home',
      pathMatch: 'full',
  },
  {
      path: 'home',
      component: HomeComponent,
  },
  {
    path: 'narudzba/:id',
    component: NarudzbaComponent,
  },
];
