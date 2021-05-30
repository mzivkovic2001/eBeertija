import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { StorageService } from './Storage.service';
import { getServerBaseAdress } from '../Globals';
import { UserDto } from '@src/app/Models/UserDto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

constructor(private http: HttpClient, private storage: StorageService) { }

  login(username: string, password: string) {
    console.log('startinglogin..' + getServerBaseAdress);
    return this.http.post<any>(getServerBaseAdress + 'user/authenticate', { username: username, password: password })
        .pipe(map(user => {
            if (user && user.token) {
              this.storage.getStorage().setItem('currentUser', JSON.stringify(user));
            }
            return user;
          }));
  }

  logout() {
    console.log('logout');
    // remove user from local storage to log user out
    this.storage.getStorage().removeItem('currentUser');
  // this.comm.authenticationEvent(false);
  }

  getUser(): UserDto {
    return JSON.parse(this.storage.getStorage().getItem('currentUser'));
  }

  isLoggedin(): boolean {
    return this.getUser() !== null;
  }

  getById(id: number): Observable<UserDto> {
    return this.http.get<UserDto>(getServerBaseAdress + 'user/' + id);
  }

  update(id: number, user: UserDto): Observable<UserDto> {
    return this.http.put<UserDto>(getServerBaseAdress + 'user/' + id, user);
  }

  delete(id: number): Observable<UserDto> {
    return this.http.delete<UserDto>(getServerBaseAdress + 'user/' + id);
  }

}
