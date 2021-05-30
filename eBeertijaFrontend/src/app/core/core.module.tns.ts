import { NgModule, NO_ERRORS_SCHEMA, Optional, SkipSelf } from '@angular/core';

import { CoreRoutingModule } from '@src/app/core/core-routing.module';
import { NativeScriptCommonModule } from 'nativescript-angular/common';
import { NavbarComponent } from '@src/app/core/navbar/navbar.component';
import { throwIfAlreadyLoaded } from './Guards/import.guard';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { StavkaCjenikaService } from './Services/StavkaCjenika.service';
import { JwtInterceptor } from './Interceptors/jwt.interceptor';
import { ErrorInterceptor } from './Interceptors/error.interceptor.tns';
import { AlertService } from './Services/alert.service';
import { AuthGuard } from './Guards/auth.guard';


@NgModule({
  declarations: [NavbarComponent],
  imports: [
    CoreRoutingModule,
    NativeScriptCommonModule,
    CommonModule, RouterModule, HttpClientModule,

  ],
  exports: [
    RouterModule, HttpClientModule, NavbarComponent
  ],
  providers: [
    StavkaCjenikaService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    AlertService,
    AuthGuard,
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }
}
