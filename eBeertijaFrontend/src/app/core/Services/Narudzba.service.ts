import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NarudzbaDto } from '@src/app/Models/NarudzbaDto';
import { getServerBaseAdress } from '../Globals';

@Injectable({
  providedIn: 'root'
})
export class NarudzbaService {

constructor(private http: HttpClient) { }

getNarudzbaById(id: number): Observable<NarudzbaDto> {
  return this.http.get<NarudzbaDto>(getServerBaseAdress + 'narudzba/' + id);
}

stvoriRacun(id: number, narudzba: NarudzbaDto): Observable<any> {
  return this.http.post<any>(getServerBaseAdress + 'racun/narudzba/' + id, narudzba);
}

deleteNarudzba(id: number): Observable<any> {
  return this.http.delete<any>(getServerBaseAdress + 'narudzba/' + id);
}

}
