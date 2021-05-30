import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RacunWindowComponent } from './racun-window/racun-window.component';


const routes: Routes = [
  { path: 'racun/:id', component: RacunWindowComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RacunRoutingModule { }
