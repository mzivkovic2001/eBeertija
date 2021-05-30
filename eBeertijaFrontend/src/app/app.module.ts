import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from '@src/app/app-routing.module';
import { AppComponent } from '@src/app/app.component';
import { HomeComponent } from '@src/app/home/home.component';
import { CoreModule } from '@src/app/core/core.module';
import { SharedModule } from '@src/app/shared/shared.module';
import { UsersModule } from '@src/app/users/users.module';
import { MaterialModule } from '@src/app/material/material.module';
import { CjenikModule } from '@src/app/cjenik/cjenik.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { AddStolComponent } from '@src/app/add-stol/add-stol.component';
import { RacunModule } from '@src/app/racun/racun.module';
import { IzvjestajModule } from '@src/app/izvjestaj/izvjestaj.module';
import { DataTablesModule } from 'angular-datatables';
import { NarudzbaComponent } from '@src/app/narudzba/narudzba.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AddStolComponent,
    NarudzbaComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    DragDropModule,
    CoreModule,
    SharedModule,
    BrowserModule,
    BrowserAnimationsModule,
    UsersModule,
    MaterialModule,
    CjenikModule,
    RacunModule,
    IzvjestajModule,
    DataTablesModule.forRoot()
  ],
  exports: [
    MaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [AddStolComponent]
})
export class AppModule { }
