import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RacunIzvjestajDto } from '@src/app/Models/RacunIzvjestajDto';
import { getServerBaseAdress } from '../Globals';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IzvjestajService {

constructor(private http: HttpClient) { }

getIzvjestaj(date: string): Observable<RacunIzvjestajDto[]> {
    return this.http.get<RacunIzvjestajDto[]>(getServerBaseAdress + 'izvjestaj/' + date);
 }

 getIzvjestajNaDanasnjiDatum(): Observable<RacunIzvjestajDto[]> {
    return this.http.get<RacunIzvjestajDto[]>(getServerBaseAdress + 'izvjestaj');
 }

}
