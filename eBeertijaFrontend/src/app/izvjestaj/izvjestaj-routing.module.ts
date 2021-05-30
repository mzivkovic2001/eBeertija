import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RacunIzvjestajComponent } from './racun-izvjestaj/racun-izvjestaj.component';


const routes: Routes = [
  { path: 'izvjestaj', component: RacunIzvjestajComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IzvjestajRoutingModule { }
