import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { ClienteService } from '../../core/services/cliente.service';
import { IngresoService } from '../../core/services/ingreso.service';
import { SuscripcionService } from '../../core/services/suscripcion.service';
import { CasilleroService } from '../../core/services/casillero.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  menuItems = [
    { path: '/dashboard', icon: '📊', label: 'Dashboard' },
    { path: '/ingresos', icon: '🚪', label: 'Ingresos/Salidas' },
    { path: '/clientes', icon: '👥', label: 'Clientes' },
    { path: '/suscripciones', icon: '💳', label: 'Suscripciones' },
    { path: '/casilleros', icon: '🔒', label: 'Casilleros' },
    { path: '/reportes', icon: '📈', label: 'Reportes' },
    { path: '/admin', icon: '⚙️', label: 'Administración' }
  ];

  currentTime = new Date();
  ingresosHoy = 0;
  clientesActivos = 0;
  suscripcionesVigentes = 0;
  casillerosDisponibles = 0;

  constructor(
    private ingresoService: IngresoService,
    private clienteService: ClienteService,
    private suscripcionService: SuscripcionService,
    private casilleroService: CasilleroService
  ) {}

  ngOnInit() {
    setInterval(() => {
      this.currentTime = new Date();
    }, 1000);

    this.cargarResumen();
  }

  cargarResumen() {
    this.ingresoService.getAll().subscribe({
      next: (ingresos) => {
        const hoy = new Date();
        this.ingresosHoy = ingresos.filter(i => {
          if (!i.fechaIngreso) {
            return false;
          }

          const fechaIngreso = new Date(i.fechaIngreso);
          return this.esMismoDia(fechaIngreso, hoy);
        }).length;
      },
      error: () => this.ingresosHoy = 0
    });

    this.clienteService.getAll().subscribe({
      next: (clientes) => this.clientesActivos = clientes.length,
      error: () => this.clientesActivos = 0
    });

    this.suscripcionService.getAll().subscribe({
      next: (suscripciones) => {
        this.suscripcionesVigentes = suscripciones.filter(s =>
          (s.estaVigente ?? false) || s.estado?.toUpperCase() === 'ACTIVA'
        ).length;
      },
      error: () => this.suscripcionesVigentes = 0
    });

    this.casilleroService.getDisponibles().subscribe({
      next: (casilleros) => this.casillerosDisponibles = casilleros.length,
      error: () => this.casillerosDisponibles = 0
    });
  }

  private esMismoDia(a: Date, b: Date): boolean {
    return a.getFullYear() === b.getFullYear()
      && a.getMonth() === b.getMonth()
      && a.getDate() === b.getDate();
  }
}
