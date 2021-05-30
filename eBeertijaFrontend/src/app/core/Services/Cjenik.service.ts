import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CjenikDto } from '@src/app/Models/CjenikDto';
import { getServerBaseAdress } from '../Globals';

@Injectable({
  providedIn: 'root'
})
export class CjenikService {

constructor(private http: HttpClient) { }

getCjenici(): Observable<CjenikDto[]> {
  return this.http.get<CjenikDto[]>(getServerBaseAdress + 'cjenik');
}

dodajCjenik(cjenik: CjenikDto): Observable<CjenikDto> {
  return this.http.post<CjenikDto>(getServerBaseAdress + 'cjenik', cjenik);
}

updateCjenik(cjenik: CjenikDto): Observable<CjenikDto> {
  return this.http.put<CjenikDto>(getServerBaseAdress + 'cjenik/', cjenik);
}

deleteCjenik(id: number): Observable<any> {
    return this.http.delete(getServerBaseAdress + 'cjenik/' + id);
}

}
