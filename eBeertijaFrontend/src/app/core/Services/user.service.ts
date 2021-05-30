import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserDto } from '../../Models/UserDto';
import { Observable } from 'rxjs';
import { RegistrationUserDto } from '@src/app/Models/RegistrationUserDto';
import { getServerBaseAdress } from '../Globals';


@Injectable({
    providedIn: 'root'
  })
@Injectable()
export class UserService {
    constructor(private http: HttpClient) { }

    /* getAll() {
        return this.http.get<UserDto[]>(getServerAddress() + 'api/users');
    } */

    getAll(): Observable<UserDto[]> {
        return this.http.get<UserDto[]>(getServerBaseAdress + 'user');
      }

    getById(id: number) {
        return this.http.get(`${getServerBaseAdress}/user/` + id);
    }

    register(user: RegistrationUserDto): Observable<RegistrationUserDto> {
        return this.http.post<RegistrationUserDto>( getServerBaseAdress + 'user/register', user);
    }

    update(id: number, user: UserDto): Observable<UserDto> {
        return this.http.put<UserDto>(getServerBaseAdress + 'user/' + user.id, user);
    }

    delete(id: number) {
        return this.http.delete(`${getServerBaseAdress}/user/` + id);
    }
}
