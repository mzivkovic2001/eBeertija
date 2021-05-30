import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RacunRoutingModule } from '@src/app/racun/racun-routing.module';
import { RacunWindowComponent } from '@src/app/racun/racun-window/racun-window.component';
import { MaterialModule } from '../material/material.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DragDropModule } from '@angular/cdk/drag-drop';


@NgModule({
  declarations: [RacunWindowComponent],
  imports: [
    CommonModule,
    RacunRoutingModule,
    MaterialModule,
    BrowserModule,
    BrowserAnimationsModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    DragDropModule
  ],
  exports: [
    MaterialModule
  ]
})
export class RacunModule { }
