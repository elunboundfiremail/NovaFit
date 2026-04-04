import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Ingreso } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class IngresoService {
  private apiUrl = `${environment.apiUrl}/ingresos`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Ingreso[]> {
    return this.http.get<Ingreso[]>(this.apiUrl);
  }

  registrarIngreso(ci: number): Observable<Ingreso> {
    return this.http.post<Ingreso>(`${this.apiUrl}/registrar`, { ci });
  }

  registrarSalida(ingresoId: string): Observable<Ingreso> {
    return this.http.post<Ingreso>(`${this.apiUrl}/${ingresoId}/salida`, {});
  }

  getByCliente(clienteId: string): Observable<Ingreso[]> {
    return this.http.get<Ingreso[]>(`${this.apiUrl}/cliente/${clienteId}`);
  }

  getByFecha(fecha: string): Observable<Ingreso[]> {
    return this.http.get<Ingreso[]>(`${this.apiUrl}/fecha/${fecha}`);
  }
}
