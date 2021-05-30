import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptModule } from 'nativescript-angular/nativescript.module';

import { AppRoutingModule } from '@src/app/app-routing.module';
import { AppComponent } from '@src/app/app.component';
import { HomeComponent } from '@src/app/home/home.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpInterceptorService } from '@src/app/core/Services/http-interceptor.service';
import { NativeScriptCommonModule } from 'nativescript-angular/common';
import { NativeScriptFormsModule } from 'nativescript-angular/forms';
import { CoreModule } from '@src/app/core/core.module.tns';
import { NarudzbaComponent } from '@src/app/narudzba/narudzba.component';


// Uncomment and add to NgModule imports if you need to use two-way binding
// import { NativeScriptFormsModule } from 'nativescript-angular/forms';

// Uncomment and add to NgModule imports  if you need to use the HTTP wrapper
// import { NativeScriptHttpClientModule } from 'nativescript-angular/http-client';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NarudzbaComponent
  ],
  imports: [
    NativeScriptModule,
    AppRoutingModule,
    NativeScriptCommonModule,
    NativeScriptFormsModule,
    CoreModule
  ],
  providers: [ {
    provide: HTTP_INTERCEPTORS,
    useClass: HttpInterceptorService,
    multi: true
  }],
  bootstrap: [AppComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class AppModule { }
