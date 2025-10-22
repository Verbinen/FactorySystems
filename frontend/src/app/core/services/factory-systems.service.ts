import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { ISystemPayload } from '../../shared/models/system-payload.interface';
import { ISystemPostBody } from '../../shared/models/system-post-body.interface';

@Injectable({
  providedIn: 'root',
})
export class FactorySystemsService {
  private _http = inject(HttpClient);
  private url = environment.API_URL;

  getAllSystems(): Observable<ISystemPayload[]> {
    return this._http.get<ISystemPayload[]>(this.url);
  }

  getSystemById(id: string): Observable<ISystemPayload> {
    return this._http.get<ISystemPayload>(`${this.url}/${id}`);
  }

  postSystem(params: ISystemPostBody): Observable<ISystemPayload> {
    return this._http.post<ISystemPayload>(this.url, params);
  }

  putSystem(id: string, params: ISystemPayload): Observable<ISystemPayload> {
    return this._http.put<ISystemPayload>(`${this.url}/${id}`, params);
  }

  deleteSystem(id: string): Observable<any> {
    return this._http.delete(`${this.url}/${id}`);
  }
}
