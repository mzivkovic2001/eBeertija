import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StavkaCjenikaDto } from '@src/app/Models/StavkaCjenikaDto';
import { Observable } from 'rxjs';
import { getServerBaseAdress } from '../Globals';

@Injectable({
  providedIn: 'root'
})
export class StavkaCjenikaService {

constructor(private http: HttpClient) { }

getStavkeCjenikaNaSnazi(): Observable<StavkaCjenikaDto[]> {
  return this.http.get<StavkaCjenikaDto[]>(getServerBaseAdress + 'stavkaCjenika');
}

dodajStavku(stavka: StavkaCjenikaDto): Observable<StavkaCjenikaDto> {
  return this.http.post<StavkaCjenikaDto>(getServerBaseAdress + 'stavkaCjenika', stavka);
}

updateStavke(stavka: StavkaCjenikaDto): Observable<StavkaCjenikaDto> {
  return this.http.put<StavkaCjenikaDto>(getServerBaseAdress + 'stavkaCjenika', stavka);
}

deleteStavka(id: number): Observable<any> {
  return this.http.delete(getServerBaseAdress + 'stavkaCjenika/' + id);
}

}
