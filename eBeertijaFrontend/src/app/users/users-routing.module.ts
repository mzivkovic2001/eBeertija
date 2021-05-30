import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { AdministratorPageComponent } from './administrator-page/administrator-page.component';
import { UserPageComponent } from './user-page/user-page.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LogoutComponent },
  { path: 'administrator', component: AdministratorPageComponent },
  { path: 'user', component: UserPageComponent }
 ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
