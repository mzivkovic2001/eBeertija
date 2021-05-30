import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { RacunRoutingModule } from '@src/app/racun/racun-routing.module';
import { NativeScriptCommonModule } from 'nativescript-angular/common';
import { RacunWindowComponent } from '@src/app/racun/racun-window/racun-window.component';


@NgModule({
  declarations: [RacunWindowComponent],
  imports: [
    RacunRoutingModule,
    NativeScriptCommonModule
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class RacunModule { }
