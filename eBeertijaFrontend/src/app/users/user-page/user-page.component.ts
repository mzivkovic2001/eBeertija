import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '@src/app/core/Services/authentication.service';
import { UserDto } from '@src/app/Models/UserDto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css']
})
export class UserPageComponent implements OnInit {

  constructor(private auth: AuthenticationService, private router: Router) { }
  loggedUser: UserDto;
  userPodaci: UserDto;
  isLoaded: boolean;
  arePasswordsEqual: boolean;
  typingStarted: boolean;

  ngOnInit() {
    this.isLoaded=false;
      if(this.auth.isLoggedin()){
        this.loggedUser = this.auth.getUser();
        this.getUser();
        this.isLoaded = true;
      }   
  }

  getUser() {

    this.userPodaci = new UserDto();
      this.auth.getById(this.loggedUser.id).toPromise()
      .then(
          res => {
              this.userPodaci = res;
          }
      );

      this.userPodaci.password = '';
      this.userPodaci.previousPassword='';
      this.userPodaci.passwordConfirm='';
  }

  passwordChange() {
    this.userPodaci.isPasswordUpdate = true;

    console.log('Tretnutna: ' + this.userPodaci.previousPassword);
    console.log('Nova: ' + this.userPodaci.password);

    this.auth.update(this.userPodaci.id, this.userPodaci).toPromise().then(
        res => {
            this.router.navigate(["/login"]);
            this.auth.logout();
        }
    );
  }

}
