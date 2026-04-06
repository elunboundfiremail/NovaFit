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
  private readonly manualLoginKey = 'novafit_manual_login';

  constructor(private http: HttpClient) {
    const token = this.getStoredToken();
    if (!this.hasManualLogin() && token) {
      this.logout();
      return;
    }

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
      tap(response => this.setSession(response, true))
    );
  }

  ensureAuthenticated(): Observable<boolean> {
    if (!this.hasManualLogin()) {
      this.logout();
      return of(false);
    }

    const token = this.getToken();
    if (token && !this.isTokenExpired(token)) {
      this.scheduleRefresh(token);
      return of(true);
    }

    const refreshToken = this.getStoredRefreshToken();
    if (!refreshToken || this.isTokenExpired(refreshToken)) {
      this.logout();
      return of(false);
    }

    return this.refreshAccessToken().pipe(
      map(() => true),
      catchError(error => {
        console.error('No se pudo renovar la sesion', error);
        this.logout();
        return of(false);
      })
    );
  }

  refreshAccessToken(): Observable<AuthResponse> {
    const refreshToken = this.getStoredRefreshToken();
    if (!refreshToken) {
      return new Observable<AuthResponse>(subscriber => {
        subscriber.error(new Error('No existe refresh token'));
      });
    }

    const params = new URLSearchParams();
    params.set('client_id', environment.keycloakClientId);
    params.set('grant_type', 'refresh_token');
    params.set('refresh_token', refreshToken);

    return this.http.post<AuthResponse>(
      `${environment.keycloakUrl}/realms/${environment.keycloakRealm}/protocol/openid-connect/token`,
      params.toString(),
      { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }
    ).pipe(
      tap(response => this.setSession(response, false))
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem(this.manualLoginKey);
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
    const token = this.getToken();
    return this.hasManualLogin() && !!token && !this.isTokenExpired(token);
  }

  private hasManualLogin(): boolean {
    return localStorage.getItem(this.manualLoginKey) === 'true';
  }

  private setSession(response: AuthResponse, markManualLogin: boolean): void {
    localStorage.setItem('token', response.access_token);
    if (response.refresh_token) {
      localStorage.setItem('refresh_token', response.refresh_token);
    }
    if (markManualLogin) {
      localStorage.setItem(this.manualLoginKey, 'true');
    }
    this.tokenSubject.next(response.access_token);
    this.scheduleRefresh(response.access_token);
  }

  private getStoredToken(): string | null {
    return localStorage.getItem('token');
  }

  private getStoredRefreshToken(): string | null {
    return localStorage.getItem('refresh_token');
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
      const padded = normalized.padEnd(Math.ceil(normalized.length / 4) * 4, '=');
      const decoded = JSON.parse(atob(padded));
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
      this.refreshAccessToken().subscribe({
        error: (error) => {
          console.error('No se pudo renovar el token', error);
          this.logout();
        }
      });
    }, refreshInMs);
  }
}
