import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { getServerBaseAdress } from '../Globals';
import { GrafickiPrikazDto } from '@src/app/Models/GrafickiPrikazDto';
import { StolDto } from '@src/app/Models/StolDto';

@Injectable({
  providedIn: 'root'
})
export class GrafickiPrikazService {

constructor(private http: HttpClient) { }

getGrafickiPrikaz(): Observable<GrafickiPrikazDto> {
  return this.http.get<GrafickiPrikazDto>(getServerBaseAdress + 'grafickiPrikaz');
}

saveGrafickiPrikaz(grafickiPrikaz: GrafickiPrikazDto): Observable<any> {
  return this.http.put(getServerBaseAdress + 'grafickiPrikaz', grafickiPrikaz);
}

dodajStol(stol: StolDto): Observable<StolDto> {
  return this.http.post<StolDto>(getServerBaseAdress + 'grafickiPrikaz', stol);
}

}
