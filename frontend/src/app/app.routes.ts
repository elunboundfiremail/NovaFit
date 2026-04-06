import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    loadComponent: () => import('./features/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'dashboard',
    canActivate: [authGuard],
    loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent)
  },
  {
    path: 'clientes',
    canActivate: [authGuard],
    loadComponent: () => import('./features/clientes/clientes.component').then(m => m.ClientesComponent)
  },
  {
    path: 'suscripciones',
    canActivate: [authGuard],
    loadComponent: () => import('./features/suscripciones/suscripciones.component').then(m => m.SuscripcionesComponent)
  },
  {
    path: 'ingresos',
    canActivate: [authGuard],
    loadComponent: () => import('./features/ingresos/ingresos.component').then(m => m.IngresosComponent)
  },
  {
    path: 'casilleros',
    canActivate: [authGuard],
    loadComponent: () => import('./features/casilleros/casilleros.component').then(m => m.CasillerosComponent)
  },
  {
    path: 'reportes',
    canActivate: [authGuard],
    loadComponent: () => import('./features/reportes/reportes.component').then(m => m.ReportesComponent)
  },
  {
    path: '**',
    redirectTo: '/login'
  }
];
