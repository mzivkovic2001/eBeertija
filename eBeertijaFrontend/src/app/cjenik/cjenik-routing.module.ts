import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CjenikPrikazComponent } from './cjenik-prikaz/cjenik-prikaz.component';

const routes: Routes = [
  { path: 'cjenici', component: CjenikPrikazComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CjenikRoutingModule { }
