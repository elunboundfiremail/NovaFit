import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { forkJoin } from 'rxjs';
import { CasilleroService } from '../../core/services/casillero.service';
import { ClienteService } from '../../core/services/cliente.service';
import { IngresoService } from '../../core/services/ingreso.service';
import { SuscripcionService } from '../../core/services/suscripcion.service';
import { Casillero, Cliente, Ingreso, PrestamoCasillero, Suscripcion } from '../../core/models/models';

type TipoReporte = 'ingresos-mes' | 'ingresos-economicos' | 'clientes-activos' | 'uso-casilleros' | '';

@Component({
  selector: 'app-reportes',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="page-container">
      <div class="page-header">
        <h2>📈 Reportes y Estadísticas</h2>
      </div>

      <div class="card">
        <h3>Reportes Disponibles</h3>
        <div class="reportes-grid">
          <div class="reporte-card">
            <h4>📊 Ingresos del Mes</h4>
            <p>Total de ingresos registrados este mes</p>
            <button class="btn btn-primary" (click)="generarIngresosMes()" [disabled]="loading">
              Generar
            </button>
          </div>
          <div class="reporte-card">
            <h4>💰 Ingresos Económicos</h4>
            <p>Resumen financiero de suscripciones</p>
            <button class="btn btn-primary" (click)="generarIngresosEconomicos()" [disabled]="loading">
              Generar
            </button>
          </div>
          <div class="reporte-card">
            <h4>👥 Clientes Activos</h4>
            <p>Lista de clientes con suscripciones vigentes</p>
            <button class="btn btn-primary" (click)="generarClientesActivos()" [disabled]="loading">
              Generar
            </button>
          </div>
          <div class="reporte-card">
            <h4>🔒 Uso de Casilleros</h4>
            <p>Estadísticas de ocupación de casilleros</p>
            <button class="btn btn-primary" (click)="generarUsoCasilleros()" [disabled]="loading">
              Generar
            </button>
          </div>
        </div>
      </div>

      <div class="card resultado-card" *ngIf="selectedReport || loading || error">
        <div class="resultado-header">
          <h3>{{ tituloResultado }}</h3>
          <span class="estado" *ngIf="loading">Cargando...</span>
        </div>

        <div class="alert error" *ngIf="error">{{ error }}</div>

        <ng-container *ngIf="selectedReport === 'ingresos-mes' && !loading && !error">
          <div class="stats-row">
            <div class="mini-stat">
              <span>Total del mes</span>
              <strong>{{ ingresosMes.length }}</strong>
            </div>
            <div class="mini-stat">
              <span>Activos hoy</span>
              <strong>{{ ingresosActivosHoy }}</strong>
            </div>
          </div>

          <table class="data-table" *ngIf="ingresosMes.length; else sinIngresosMes">
            <thead>
              <tr>
                <th>Fecha</th>
                <th>CI</th>
                <th>Cliente</th>
                <th>Ingreso</th>
                <th>Estado</th>
              </tr>
            </thead>
            <tbody>
              <tr *ngFor="let ingreso of ingresosMes">
                <td>{{ formatearFecha(ingreso.fechaIngreso) }}</td>
                <td>{{ ingreso.ciCliente }}</td>
                <td>{{ ingreso.nombreCliente }}</td>
                <td>{{ formatearHora(ingreso.horaIngreso) }}</td>
                <td>{{ ingreso.salidaRegistrada ? 'Finalizado' : 'En gimnasio' }}</td>
              </tr>
            </tbody>
          </table>
          <ng-template #sinIngresosMes>
            <div class="empty-state">No hay ingresos registrados este mes.</div>
          </ng-template>
        </ng-container>

        <ng-container *ngIf="selectedReport === 'ingresos-economicos' && !loading && !error">
          <div class="stats-row">
            <div class="mini-stat">
              <span>Total proyectado</span>
              <strong>{{ totalIngresos | number:'1.0-2' }} Bs</strong>
            </div>
            <div class="mini-stat">
              <span>Suscripciones</span>
              <strong>{{ suscripcionesReporte.length }}</strong>
            </div>
          </div>

          <table class="data-table" *ngIf="suscripcionesReporte.length; else sinSuscripciones">
            <thead>
              <tr>
                <th>Cliente</th>
                <th>Tipo</th>
                <th>Precio</th>
                <th>Estado</th>
                <th>Vence</th>
              </tr>
            </thead>
            <tbody>
              <tr *ngFor="let suscripcion of suscripcionesReporte">
                <td>{{ obtenerNombreCliente(suscripcion.clienteId) }}</td>
                <td>{{ suscripcion.tipo }}</td>
                <td>{{ suscripcion.precio }} Bs</td>
                <td>{{ suscripcion.estado }}</td>
                <td>{{ formatearFecha(suscripcion.fechaVencimiento) }}</td>
              </tr>
            </tbody>
          </table>
          <ng-template #sinSuscripciones>
            <div class="empty-state">No hay suscripciones registradas.</div>
          </ng-template>
        </ng-container>

        <ng-container *ngIf="selectedReport === 'clientes-activos' && !loading && !error">
          <div class="stats-row">
            <div class="mini-stat">
              <span>Clientes activos</span>
              <strong>{{ clientesActivos.length }}</strong>
            </div>
          </div>

          <table class="data-table" *ngIf="clientesActivos.length; else sinClientesActivos">
            <thead>
              <tr>
                <th>CI</th>
                <th>Cliente</th>
                <th>Email</th>
                <th>Teléfono</th>
              </tr>
            </thead>
            <tbody>
              <tr *ngFor="let cliente of clientesActivos">
                <td>{{ cliente.ci }}</td>
                <td>{{ cliente.nombre }} {{ cliente.apellido }}</td>
                <td>{{ cliente.email || '-' }}</td>
                <td>{{ cliente.telefono || '-' }}</td>
              </tr>
            </tbody>
          </table>
          <ng-template #sinClientesActivos>
            <div class="empty-state">No hay clientes con suscripciones vigentes.</div>
          </ng-template>
        </ng-container>

        <ng-container *ngIf="selectedReport === 'uso-casilleros' && !loading && !error">
          <div class="stats-row">
            <div class="mini-stat">
              <span>Disponibles</span>
              <strong>{{ casillerosDisponibles }}</strong>
            </div>
            <div class="mini-stat">
              <span>Ocupados</span>
              <strong>{{ casillerosOcupados }}</strong>
            </div>
            <div class="mini-stat">
              <span>Mantenimiento</span>
              <strong>{{ casillerosMantenimiento }}</strong>
            </div>
            <div class="mini-stat">
              <span>Préstamos activos</span>
              <strong>{{ prestamosActivos.length }}</strong>
            </div>
          </div>

          <table class="data-table" *ngIf="prestamosActivos.length; else sinPrestamosActivos">
            <thead>
              <tr>
                <th>Casillero</th>
                <th>Ingreso</th>
                <th>CI Depositado</th>
                <th>Desde</th>
              </tr>
            </thead>
            <tbody>
              <tr *ngFor="let prestamo of prestamosActivos">
                <td>#{{ prestamo.numeroCasillero }}</td>
                <td>{{ obtenerResumenIngreso(prestamo.ingresoId) }}</td>
                <td>{{ prestamo.ciDepositado || '-' }}</td>
                <td>{{ formatearFechaHora(prestamo.fechaPrestamo) }}</td>
              </tr>
            </tbody>
          </table>
          <ng-template #sinPrestamosActivos>
            <div class="empty-state">No hay préstamos activos.</div>
          </ng-template>
        </ng-container>
      </div>
    </div>
  `,
  styles: [`
    .reportes-grid {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
      gap: 20px;
    }

    .reporte-card {
      background: var(--bg-tertiary);
      padding: 20px;
      border-radius: 8px;
      border: 1px solid var(--border-color);
    }

    .reporte-card h4 {
      margin: 0 0 10px;
      color: var(--accent-primary);
    }

    .reporte-card p {
      color: var(--text-secondary);
      font-size: 14px;
      margin-bottom: 15px;
    }

    .resultado-card {
      margin-top: 24px;
    }

    .resultado-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 16px;
    }

    .resultado-header h3 {
      margin: 0;
    }

    .estado {
      color: var(--text-secondary);
      font-size: 14px;
    }

    .stats-row {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(180px, 1fr));
      gap: 16px;
      margin-bottom: 20px;
    }

    .mini-stat {
      background: var(--bg-tertiary);
      border: 1px solid var(--border-color);
      border-radius: 8px;
      padding: 14px 16px;
    }

    .mini-stat span {
      display: block;
      color: var(--text-secondary);
      font-size: 13px;
      margin-bottom: 6px;
    }

    .mini-stat strong {
      font-size: 26px;
      color: var(--accent-primary);
    }

    .data-table {
      width: 100%;
      border-collapse: collapse;
    }

    .data-table th,
    .data-table td {
      padding: 12px 8px;
      text-align: left;
      border-bottom: 1px solid var(--border-color);
    }

    .data-table th {
      color: var(--accent-primary);
      font-weight: 600;
    }

    .empty-state {
      padding: 16px 0;
      color: var(--text-secondary);
    }

    .alert.error {
      background: rgba(255, 71, 87, 0.12);
      color: #ff6b81;
      border: 1px solid rgba(255, 107, 129, 0.3);
      border-radius: 8px;
      padding: 12px 16px;
      margin-bottom: 16px;
    }
  `]
})
export class ReportesComponent {
  loading = false;
  error = '';
  selectedReport: TipoReporte = '';

  clientes: Cliente[] = [];
  ingresosMes: Ingreso[] = [];
  ingresosActivosHoy = 0;
  suscripcionesReporte: Suscripcion[] = [];
  clientesActivos: Cliente[] = [];
  casilleros: Casillero[] = [];
  prestamosActivos: PrestamoCasillero[] = [];

  totalIngresos = 0;
  casillerosDisponibles = 0;
  casillerosOcupados = 0;
  casillerosMantenimiento = 0;

  constructor(
    private ingresoService: IngresoService,
    private suscripcionService: SuscripcionService,
    private clienteService: ClienteService,
    private casilleroService: CasilleroService
  ) {}

  get tituloResultado(): string {
    switch (this.selectedReport) {
      case 'ingresos-mes':
        return 'Reporte: Ingresos del Mes';
      case 'ingresos-economicos':
        return 'Reporte: Ingresos Económicos';
      case 'clientes-activos':
        return 'Reporte: Clientes Activos';
      case 'uso-casilleros':
        return 'Reporte: Uso de Casilleros';
      default:
        return 'Resultado';
    }
  }

  generarIngresosMes(): void {
    this.selectedReport = 'ingresos-mes';
    this.loading = true;
    this.error = '';

    this.ingresoService.getAll().subscribe({
      next: (ingresos) => {
        const ahora = new Date();
        this.ingresosMes = ingresos.filter(i => {
          const fecha = new Date(i.fechaIngreso);
          return fecha.getFullYear() === ahora.getFullYear() && fecha.getMonth() === ahora.getMonth();
        });

        this.ingresosActivosHoy = this.ingresosMes.filter(i => {
          const fecha = new Date(i.fechaIngreso);
          return this.esMismoDia(fecha, ahora) && !i.salidaRegistrada;
        }).length;

        this.loading = false;
      },
      error: () => {
        this.error = 'No se pudo generar el reporte de ingresos del mes';
        this.loading = false;
      }
    });
  }

  generarIngresosEconomicos(): void {
    this.selectedReport = 'ingresos-economicos';
    this.loading = true;
    this.error = '';

    forkJoin({
      suscripciones: this.suscripcionService.getAll(),
      clientes: this.clienteService.getAll()
    }).subscribe({
      next: ({ suscripciones, clientes }) => {
        this.clientes = clientes;
        this.suscripcionesReporte = [...suscripciones].sort((a, b) =>
          new Date(b.fechaInicio).getTime() - new Date(a.fechaInicio).getTime()
        );
        this.totalIngresos = this.suscripcionesReporte.reduce((acc, sus) => acc + (sus.precio || 0), 0);
        this.loading = false;
      },
      error: () => {
        this.error = 'No se pudo generar el reporte económico';
        this.loading = false;
      }
    });
  }

  generarClientesActivos(): void {
    this.selectedReport = 'clientes-activos';
    this.loading = true;
    this.error = '';

    forkJoin({
      clientes: this.clienteService.getAll(),
      suscripciones: this.suscripcionService.getAll()
    }).subscribe({
      next: ({ clientes, suscripciones }) => {
        const idsActivos = new Set(
          suscripciones
            .filter(s => (s.estaVigente ?? false) || s.estado?.toUpperCase() === 'ACTIVA')
            .map(s => s.clienteId)
        );

        this.clientesActivos = clientes.filter(c => idsActivos.has(c.id));
        this.loading = false;
      },
      error: () => {
        this.error = 'No se pudo generar el reporte de clientes activos';
        this.loading = false;
      }
    });
  }

  generarUsoCasilleros(): void {
    this.selectedReport = 'uso-casilleros';
    this.loading = true;
    this.error = '';

    forkJoin({
      casilleros: this.casilleroService.getAll(),
      prestamosActivos: this.casilleroService.getPrestamosActivos(),
      ingresos: this.ingresoService.getAll()
    }).subscribe({
      next: ({ casilleros, prestamosActivos, ingresos }) => {
        this.casilleros = casilleros;
        this.prestamosActivos = prestamosActivos;
        this.ingresosMes = ingresos;
        this.casillerosDisponibles = casilleros.filter(c => c.estado === 'DISPONIBLE').length;
        this.casillerosOcupados = casilleros.filter(c => c.estado === 'OCUPADO').length;
        this.casillerosMantenimiento = casilleros.filter(c => c.estado === 'MANTENIMIENTO').length;
        this.loading = false;
      },
      error: () => {
        this.error = 'No se pudo generar el reporte de casilleros';
        this.loading = false;
      }
    });
  }

  obtenerNombreCliente(clienteId: string): string {
    const cliente = this.clientes.find(c => c.id === clienteId);
    return cliente ? `${cliente.nombre} ${cliente.apellido}` : 'Cliente no encontrado';
  }

  obtenerResumenIngreso(ingresoId: string): string {
    const ingreso = this.ingresosMes.find(i => i.id === ingresoId);
    return ingreso ? `${ingreso.ciCliente} - ${ingreso.nombreCliente}` : 'Ingreso no encontrado';
  }

  formatearFecha(fecha?: string): string {
    return fecha ? new Date(fecha).toLocaleDateString('es-BO') : '-';
  }

  formatearFechaHora(fecha?: string): string {
    return fecha ? new Date(fecha).toLocaleString('es-BO') : '-';
  }

  formatearHora(hora?: string): string {
    return hora ? hora.slice(0, 8) : '-';
  }

  private esMismoDia(a: Date, b: Date): boolean {
    return a.getFullYear() === b.getFullYear()
      && a.getMonth() === b.getMonth()
      && a.getDate() === b.getDate();
  }
}
