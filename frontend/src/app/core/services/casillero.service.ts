import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Casillero, PrestamoCasillero } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class CasilleroService {
  private apiUrl = `${environment.apiUrl}/casilleros`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Casillero[]> {
    return this.http.get<Casillero[]>(this.apiUrl);
  }

  getDisponibles(): Observable<Casillero[]> {
    return this.http.get<Casillero[]>(`${this.apiUrl}/disponibles`);
  }

  create(casillero: Partial<Casillero>): Observable<Casillero> {
    return this.http.post<Casillero>(this.apiUrl, casillero);
  }

  update(id: string, casillero: Partial<Casillero>): Observable<Casillero> {
    return this.http.put<Casillero>(`${this.apiUrl}/${id}`, casillero);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  prestar(data: {
    casilleroId: string;
    ingresoId: string;
    numeroTicket?: string;
    numeroLlave?: string;
    ciDepositado?: number;
  }): Observable<PrestamoCasillero> {
    return this.http.post<PrestamoCasillero>(`${this.apiUrl}/prestar`, data);
  }

  registrarTicketRecepcion(data: { ingresoId: string; descripcion?: string }): Observable<PrestamoCasillero> {
    return this.http.post<PrestamoCasillero>(`${this.apiUrl}/tickets`, data);
  }

  devolver(prestamoId: string): Observable<PrestamoCasillero> {
    return this.http.post<PrestamoCasillero>(`${this.apiUrl}/devolver/${prestamoId}`, {});
  }

  getPrestamosActivos(): Observable<PrestamoCasillero[]> {
    return this.http.get<PrestamoCasillero[]>(`${this.apiUrl}/prestamos/activos`);
  }

  getHistorial(casilleroId: string): Observable<PrestamoCasillero[]> {
    return this.http.get<PrestamoCasillero[]>(`${this.apiUrl}/historial/${casilleroId}`);
  }
}
