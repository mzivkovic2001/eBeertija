import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from '@src/app/core/core-routing.module';
import { NavbarComponent } from '@src/app/core/navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthenticationService } from './Services/authentication.service';
import { AlertService } from './Services/alert.service';
import { JwtInterceptor } from './Interceptors/jwt.interceptor';
import { ErrorInterceptor } from './Interceptors/error.interceptor';
import { AuthGuard } from './Guards/auth.guard';


@NgModule({
  declarations: [NavbarComponent],
  imports: [
    CommonModule,
    CoreRoutingModule,
    RouterModule,
    HttpClientModule
  ],
  exports: [
    RouterModule,
    HttpClientModule,
    NavbarComponent
  ],
  providers: [
    AuthenticationService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    AlertService,
    AuthGuard,
  ]
})
export class CoreModule { }
