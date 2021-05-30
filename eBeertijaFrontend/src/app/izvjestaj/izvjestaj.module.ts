import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IzvjestajRoutingModule } from '@src/app/izvjestaj/izvjestaj-routing.module';
import { RacunIzvjestajComponent } from '@src/app/izvjestaj/racun-izvjestaj/racun-izvjestaj.component';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { DataTablesModule } from 'angular-datatables';
import { StavkaRacunaDetailPopUpComponent } from '@src/app/izvjestaj/stavka-racuna-detail-pop-up/stavka-racuna-detail-pop-up.component';
import { MaterialModule } from '../material/material.module';


@NgModule({
  declarations: [RacunIzvjestajComponent, StavkaRacunaDetailPopUpComponent],
  imports: [
    CommonModule,
    IzvjestajRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MaterialModule,
    DataTablesModule.forRoot()
  ],
  exports: [
    MaterialModule
  ],
  entryComponents: [
    StavkaRacunaDetailPopUpComponent
  ]
})
export class IzvjestajModule { }
