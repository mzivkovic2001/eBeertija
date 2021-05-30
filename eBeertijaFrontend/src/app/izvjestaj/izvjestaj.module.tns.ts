import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { IzvjestajRoutingModule } from '@src/app/izvjestaj/izvjestaj-routing.module';
import { NativeScriptCommonModule } from 'nativescript-angular/common';
import { RacunIzvjestajComponent } from '@src/app/izvjestaj/racun-izvjestaj/racun-izvjestaj.component';
import { StavkaRacunaDetailPopUpComponent } from '@src/app/izvjestaj/stavka-racuna-detail-pop-up/stavka-racuna-detail-pop-up.component';


@NgModule({
  declarations: [RacunIzvjestajComponent, StavkaRacunaDetailPopUpComponent],
  imports: [
    IzvjestajRoutingModule,
    NativeScriptCommonModule
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class IzvjestajModule { }
