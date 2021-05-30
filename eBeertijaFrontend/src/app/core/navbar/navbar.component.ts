import { Component, OnInit } from '@angular/core';
import { UserDto } from '@src/app/Models/UserDto';
import { Router, NavigationEnd } from '@angular/router';
import { CommponentComunicationService } from '../Services/commponent-comunication.service';
import { AuthenticationService } from '../Services/authentication.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  user: UserDto;
  isLoaded: boolean;
  title = 'E - Beertija';
  // tslint:disable-next-line:max-line-length
  constructor(private router: Router, private comm: CommponentComunicationService, public auth: AuthenticationService) { }

  ngOnInit() {
    console.log('navbarinit');
    this.router.events.subscribe((val) => {
    if (val instanceof NavigationEnd) {
      this.isLoaded = false;
    if (this.auth.isLoggedin()) {
      this.user = this.auth.getUser();
      this.isLoaded = true;
    } else {
      console.log('NOT loged in');
    }
  }
  });
  }
 logout() {
    this.auth.logout();
    this.isLoaded = false;
    this.router.navigate(['/logout']);
  }

}
