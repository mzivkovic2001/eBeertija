import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { LogoutComponent } from './logout/logout.component';
import { SharedModule } from '../shared/shared.module';
import { AdministratorPageComponent } from './administrator-page/administrator-page.component';
import { UserPageComponent } from './user-page/user-page.component';
import { MaterialModule } from '../material/material.module';
import { MatInputModule, MatTableModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RegistracijaPopUpComponent } from './registracija-pop-up/registracija-pop-up.component';

@NgModule({
  declarations: [
    LoginComponent,
    LogoutComponent,
    AdministratorPageComponent,
    UserPageComponent,
    RegistracijaPopUpComponent
   ],
  imports: [
    CommonModule,
    SharedModule,
    UsersRoutingModule,
    ReactiveFormsModule,
    MatTableModule,
    MaterialModule,
    FormsModule,
    MatInputModule
  ],
  exports: [
    MatTableModule, MaterialModule, MatInputModule
  ],
  entryComponents: [
    RegistracijaPopUpComponent
  ]
})
export class UsersModule { }
