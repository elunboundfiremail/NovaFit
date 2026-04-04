import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.getToken();

  if (token && req.url.includes(inject(Environment).apiUrl)) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  return next(req);
};

// Helper para environment injection
import { InjectionToken } from '@angular/core';
import { environment } from '../../../environments/environment';

const Environment = new InjectionToken('Environment', {
  providedIn: 'root',
  factory: () => environment
});
