import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NewRacunDto } from '@src/app/Models/NewRacunDto';
import { RacunDto } from '@src/app/Models/RacunDto';
import { getServerBaseAdress } from '../Globals';

@Injectable({
  providedIn: 'root'
})
export class RacunService {

  constructor(private http: HttpClient) { }

  getNewRacunWindow(stolId: number): Observable<NewRacunDto> {
    return this.http.get<NewRacunDto>(getServerBaseAdress + 'racun/' + stolId);
  }

  createRacun(racun: RacunDto): Observable<RacunDto> {
    return this.http.post<RacunDto>(getServerBaseAdress + 'racun', racun);
  }

  stornoRacuna(id: number): Observable<any> {
    return this.http.delete<any>(getServerBaseAdress + 'racun/' + id);
  }

}
