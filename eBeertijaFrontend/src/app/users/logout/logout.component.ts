import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '@src/app/core/Services/authentication.service';

@Component({
  selector: 'usr-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  
  constructor(  
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.authenticationService.logout();
  }

}
