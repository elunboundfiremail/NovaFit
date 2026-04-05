import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Suscripcion } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class SuscripcionService {
  private apiUrl = `${environment.apiUrl}/suscripciones`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Suscripcion[]> {
    return this.http.get<Suscripcion[]>(this.apiUrl);
  }

  getActivas(): Observable<Suscripcion[]> {
    return this.http.get<Suscripcion[]>(`${this.apiUrl}`);
  }

  getVigentes(): Observable<Suscripcion[]> {
    return this.http.get<Suscripcion[]>(`${this.apiUrl}`);
  }

  getByCliente(clienteId: string): Observable<Suscripcion[]> {
    return this.http.get<Suscripcion[]>(`${this.apiUrl}/cliente/${clienteId}`);
  }

  create(suscripcion: Partial<Suscripcion>): Observable<Suscripcion> {
    return this.http.post<Suscripcion>(this.apiUrl, suscripcion);
  }

  update(id: string, suscripcion: Partial<Suscripcion>): Observable<Suscripcion> {
    return this.http.put<Suscripcion>(`${this.apiUrl}/${id}`, suscripcion);
  }

  cancelar(id: string): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/${id}/cancelar`, {});
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
