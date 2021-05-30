import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject, Observable } from 'rxjs';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class CommponentComunicationService {
    private loginSubject: Subject<boolean>;
    authenticationObservable: Observable<boolean>;

  constructor() {
    this.loginSubject =  new BehaviorSubject(this.isLogedin());
    this.authenticationObservable = this.loginSubject.asObservable();
   }

   authenticationEvent( authentication: boolean) {
     this.loginSubject.next(authentication);
   }

   isLogedin(): boolean {
    if (localStorage.getItem('currentUser')) {
        // logged in so return true
        return true;
    } else {
        return false;
    }
}
}
