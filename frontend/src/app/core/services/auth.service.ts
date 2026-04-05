import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, map, of, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AuthResponse } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenSubject = new BehaviorSubject<string | null>(this.getStoredToken());
  public token$ = this.tokenSubject.asObservable();
  private refreshTimeoutId: ReturnType<typeof setTimeout> | null = null;

  constructor(private http: HttpClient) {
    const token = this.getStoredToken();
    if (token && !this.isTokenExpired(token)) {
      this.scheduleRefresh(token);
    }
  }

  login(username: string, password: string): Observable<AuthResponse> {
    const params = new URLSearchParams();
    params.set('client_id', environment.keycloakClientId);
    params.set('grant_type', 'password');
    params.set('username', username);
    params.set('password', password);

    return this.http.post<AuthResponse>(
      `${environment.keycloakUrl}/realms/${environment.keycloakRealm}/protocol/openid-connect/token`,
      params.toString(),
      { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }
    ).pipe(
      tap(response => {
        this.setToken(response.access_token);
      })
    );
  }

  ensureAuthenticated(): Observable<boolean> {
    const token = this.getToken();

    if (token && !this.isTokenExpired(token)) {
      this.scheduleRefresh(token);
      return of(true);
    }

    return this.login(environment.authUsername, environment.authPassword).pipe(
      tap(response => this.scheduleRefresh(response.access_token)),
      map(() => true),
      catchError(error => {
        console.error('No se pudo autenticar automáticamente', error);
        return of(false);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    this.tokenSubject.next(null);
    if (this.refreshTimeoutId) {
      clearTimeout(this.refreshTimeoutId);
      this.refreshTimeoutId = null;
    }
  }

  getToken(): string | null {
    return this.tokenSubject.value;
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  private setToken(token: string): void {
    localStorage.setItem('token', token);
    this.tokenSubject.next(token);
    this.scheduleRefresh(token);
  }

  private getStoredToken(): string | null {
    return localStorage.getItem('token');
  }

  private isTokenExpired(token: string): boolean {
    const exp = this.getTokenExpiration(token);
    if (!exp) {
      return true;
    }

    return Date.now() >= exp - 60_000;
  }

  private getTokenExpiration(token: string): number | null {
    try {
      const payload = token.split('.')[1];
      if (!payload) {
        return null;
      }

      const normalized = payload.replace(/-/g, '+').replace(/_/g, '/');
      const decoded = JSON.parse(atob(normalized));
      return typeof decoded.exp === 'number' ? decoded.exp * 1000 : null;
    } catch {
      return null;
    }
  }

  private scheduleRefresh(token: string): void {
    const exp = this.getTokenExpiration(token);
    if (!exp) {
      return;
    }

    const refreshInMs = Math.max(30_000, exp - Date.now() - 60_000);

    if (this.refreshTimeoutId) {
      clearTimeout(this.refreshTimeoutId);
    }

    this.refreshTimeoutId = setTimeout(() => {
      this.login(environment.authUsername, environment.authPassword).subscribe({
        error: (error) => console.error('No se pudo renovar el token', error)
      });
    }, refreshInMs);
  }
}
