import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent)
  },
  {
    path: 'clientes',
    loadComponent: () => import('./features/clientes/clientes.component').then(m => m.ClientesComponent)
  },
  {
    path: 'suscripciones',
    loadComponent: () => import('./features/suscripciones/suscripciones.component').then(m => m.SuscripcionesComponent)
  },
  {
    path: 'ingresos',
    loadComponent: () => import('./features/ingresos/ingresos.component').then(m => m.IngresosComponent)
  },
  {
    path: 'casilleros',
    loadComponent: () => import('./features/casilleros/casilleros.component').then(m => m.CasillerosComponent)
  },
  {
    path: 'reportes',
    loadComponent: () => import('./features/reportes/reportes.component').then(m => m.ReportesComponent)
  },
  {
    path: 'admin',
    loadComponent: () => import('./features/admin/admin.component').then(m => m.AdminComponent)
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  }
];
