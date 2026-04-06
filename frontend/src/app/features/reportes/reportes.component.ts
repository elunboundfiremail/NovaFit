import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { forkJoin } from 'rxjs';
import { CasilleroService } from '../../core/services/casillero.service';
import { ClienteService } from '../../core/services/cliente.service';
import { IngresoService } from '../../core/services/ingreso.service';
import { SuscripcionService } from '../../core/services/suscripcion.service';
import { Casillero, Cliente, Ingreso, PrestamoCasillero, Suscripcion } from '../../core/models/models';

type TipoReporte = 'ingresos-mes' | 'ingresos-diarios' | 'ingresos-anuales' | 'ingresos-economicos' | 'clientes-activos' | 'uso-casilleros' | '';

@Component({
  selector: 'app-reportes',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="page-container">
      <div class="page-header">
        <h2>📈 Reportes y Estadísticas</h2>
        <a routerLink="/dashboard" class="btn btn-secondary btn-back">← Volver</a>
      </div>

      <div class="card">
        <h3>Reportes Disponibles</h3>
        <div class="reportes-grid">
          <div class="reporte-card">
            <h4>📊 Ingresos del Mes</h4>
            <p>Cantidad y detalle de ingresos y egresos del mes</p>
            <button class="btn btn-primary" (click)="generarIngresosMes()" [disabled]="loading">Generar</button>
          </div>
          <div class="reporte-card">
            <h4>🗓️ Ingresos Diarios</h4>
            <p>Detalle de ingresos, egresos y activos del día</p>
            <button class="btn btn-primary" (click)="generarIngresosDiarios()" [disabled]="loading">Generar</button>
          </div>
          <div class="reporte-card">
            <h4>📅 Ingresos Anuales</h4>
            <p>Cantidad y detalle de ingresos y egresos del año</p>
            <button class="btn btn-primary" (click)="generarIngresosAnuales()" [disabled]="loading">Generar</button>
          </div>
          <div class="reporte-card">
            <h4>💰 Ingresos Económicos</h4>
            <p>Pagos del mes por cliente según su suscripción</p>
            <button class="btn btn-primary" (click)="generarIngresosEconomicos()" [disabled]="loading">Generar</button>
          </div>
          <div class="reporte-card">
            <h4>👥 Clientes Activos</h4>
            <p>Cantidad de clientes suscritos, activos y del mes</p>
            <button class="btn btn-primary" (click)="generarClientesActivos()" [disabled]="loading">Generar</button>
          </div>
          <div class="reporte-card">
            <h4>🔒 Uso de Casilleros</h4>
            <p>Estado de casilleros e historial de uso</p>
            <button class="btn btn-primary" (click)="generarUsoCasilleros()" [disabled]="loading">Generar</button>
          </div>
        </div>
      </div>

      <div class="card resultado-card" *ngIf="error">
        <div class="resultado-header">
          <h3>{{ tituloResultado }}</h3>
          <div class="resultado-actions">
          </div>
        </div>

        <div class="alert error" *ngIf="error">{{ error }}</div>
      </div>
    </div>
  `,
  styles: [`
    .page-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      gap: 16px;
      margin-bottom: 30px;
    }

    .btn-back {
      white-space: nowrap;
    }

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

    .resultado-actions {
      display: flex;
      align-items: center;
      gap: 12px;
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
  suscripciones: Suscripcion[] = [];
  ingresos: Ingreso[] = [];
  casilleros: Casillero[] = [];
  prestamosActivos: PrestamoCasillero[] = [];
  historialCasilleros: PrestamoCasillero[] = [];

  ingresosMes: Ingreso[] = [];
  ingresosHoy: Ingreso[] = [];
  ingresosAnio: Ingreso[] = [];
  egresosHoy: Ingreso[] = [];
  egresosAnio: Ingreso[] = [];
  ingresosActivos: Ingreso[] = [];
  mediaUsuariosDia = 0;
  mediaUsuariosMes = 0;
  mediaUsuariosAnio = 0;
  clientesUnicosAnio = 0;

  suscripcionesMes: Suscripcion[] = [];
  suscripcionesHoy: Suscripcion[] = [];
  totalIngresosHoy = 0;
  totalIngresosMes = 0;
  clientesConPagoHoy = 0;
  clientesConPagoMes = 0;

  clientesActivos: Cliente[] = [];
  clientesMes: Cliente[] = [];
  clientesSuscritos: Cliente[] = [];

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
        return 'Reporte: Ingresos y Egresos';
      case 'ingresos-diarios':
        return 'Reporte: Ingresos Diarios';
      case 'ingresos-anuales':
        return 'Reporte: Ingresos Anuales';
      case 'ingresos-economicos':
        return 'Reporte: Ingresos Económicos del Mes';
      case 'clientes-activos':
        return 'Reporte: Clientes Activos y Suscritos';
      case 'uso-casilleros':
        return 'Reporte: Uso de Casilleros';
      default:
        return 'Resultado';
    }
  }

  exportarPdf(): void {
    if (!this.selectedReport) {
      return;
    }

    const contenido = this.generarHtmlReporte();
    const ventana = window.open('', '_blank', 'width=1000,height=800');
    if (!ventana) {
      this.error = 'No se pudo abrir la ventana de impresión';
      return;
    }

    ventana.document.write(contenido);
    ventana.document.close();
    ventana.focus();
    setTimeout(() => ventana.print(), 400);
  }

  generarIngresosMes(): void {
    this.selectedReport = 'ingresos-mes';
    this.loading = true;
    this.error = '';

    this.ingresoService.getAll().subscribe({
      next: (ingresos) => {
        const ahora = new Date();
        this.ingresos = [...ingresos].sort((a, b) =>
          new Date(b.fechaIngreso).getTime() - new Date(a.fechaIngreso).getTime()
        );
        this.ingresosMes = this.ingresos.filter(i => this.esMismoMes(new Date(i.fechaIngreso), ahora));
        this.ingresosHoy = this.ingresos.filter(i => this.esMismoDia(new Date(i.fechaIngreso), ahora));
        this.egresosHoy = this.ingresos.filter(i =>
          i.salidaRegistrada && this.esMismoDia(new Date(i.fechaIngreso), ahora)
        );
        this.ingresosActivos = this.ingresos.filter(i => !i.salidaRegistrada);
        this.mediaUsuariosDia = this.calcularMediaUsuariosDia(this.ingresosHoy, ahora);
        this.mediaUsuariosMes = this.calcularMediaUsuariosMes(this.ingresosMes, ahora);
        this.finalizarReportePdf();
      },
      error: () => {
        this.error = 'No se pudo generar el reporte de ingresos y egresos';
        this.loading = false;
      }
    });
  }

  generarIngresosDiarios(): void {
    this.selectedReport = 'ingresos-diarios';
    this.loading = true;
    this.error = '';

    this.ingresoService.getAll().subscribe({
      next: (ingresos) => {
        const ahora = new Date();
        this.ingresos = [...ingresos].sort((a, b) =>
          new Date(b.fechaIngreso).getTime() - new Date(a.fechaIngreso).getTime()
        );
        this.ingresosHoy = this.ingresos.filter(i => this.esMismoDia(new Date(i.fechaIngreso), ahora));
        this.egresosHoy = this.ingresos.filter(i =>
          i.salidaRegistrada && this.esMismoDia(new Date(i.fechaIngreso), ahora)
        );
        this.ingresosActivos = this.ingresos.filter(i => !i.salidaRegistrada);
        this.mediaUsuariosDia = this.calcularMediaUsuariosDia(this.ingresosHoy, ahora);
        this.finalizarReportePdf();
      },
      error: () => {
        this.error = 'No se pudo generar el reporte diario de ingresos';
        this.loading = false;
      }
    });
  }

  generarIngresosAnuales(): void {
    this.selectedReport = 'ingresos-anuales';
    this.loading = true;
    this.error = '';

    this.ingresoService.getAll().subscribe({
      next: (ingresos) => {
        const ahora = new Date();
        this.ingresos = [...ingresos].sort((a, b) =>
          new Date(b.fechaIngreso).getTime() - new Date(a.fechaIngreso).getTime()
        );
        this.ingresosAnio = this.ingresos.filter(i => this.esMismoAnio(new Date(i.fechaIngreso), ahora));
        this.egresosAnio = this.ingresosAnio.filter(i => i.salidaRegistrada);
        this.clientesUnicosAnio = new Set(this.ingresosAnio.map(i => i.clienteId)).size;
        this.mediaUsuariosAnio = this.calcularMediaUsuariosAnio(this.ingresosAnio, ahora);
        this.finalizarReportePdf();
      },
      error: () => {
        this.error = 'No se pudo generar el reporte anual de ingresos';
        this.loading = false;
      }
    });
  }

  generarIngresosEconomicos(): void {
    this.selectedReport = 'ingresos-economicos';
    this.loading = true;
    this.error = '';

    forkJoin({
      clientes: this.clienteService.getAll(),
      suscripciones: this.suscripcionService.getAll()
    }).subscribe({
      next: ({ clientes, suscripciones }) => {
        const ahora = new Date();
        this.clientes = clientes;
        this.suscripciones = suscripciones;
        this.suscripcionesHoy = [...suscripciones]
          .filter(s => this.esMismoDia(new Date(s.fechaInicio), ahora))
          .sort((a, b) => new Date(b.fechaInicio).getTime() - new Date(a.fechaInicio).getTime());
        this.suscripcionesMes = [...suscripciones]
          .filter(s => this.esMismoMes(new Date(s.fechaInicio), ahora))
          .sort((a, b) => new Date(b.fechaInicio).getTime() - new Date(a.fechaInicio).getTime());

        this.totalIngresosHoy = this.suscripcionesHoy.reduce((total, s) => total + (s.precio || 0), 0);
        this.totalIngresosMes = this.suscripcionesMes.reduce((total, s) => total + (s.precio || 0), 0);
        this.clientesConPagoHoy = new Set(this.suscripcionesHoy.map(s => s.clienteId)).size;
        this.clientesConPagoMes = new Set(this.suscripcionesMes.map(s => s.clienteId)).size;
        this.finalizarReportePdf();
      },
      error: () => {
        this.error = 'No se pudo generar el reporte económico del mes';
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
        const ahora = new Date();
        this.clientes = clientes;
        this.suscripciones = suscripciones;

        const idsSuscritos = new Set(suscripciones.map(s => s.clienteId));
        const idsActivos = new Set(
          suscripciones
            .filter(s => (s.estaVigente ?? false) || s.estado?.toUpperCase() === 'ACTIVA')
            .map(s => s.clienteId)
        );

        this.clientesSuscritos = clientes.filter(c => idsSuscritos.has(c.id));
        this.clientesActivos = clientes.filter(c => idsActivos.has(c.id));
        this.clientesMes = [...clientes]
          .filter(c => this.esMismoMes(new Date(c.fechaRegistro), ahora))
          .sort((a, b) => new Date(b.fechaRegistro).getTime() - new Date(a.fechaRegistro).getTime());

        this.finalizarReportePdf();
      },
      error: () => {
        this.error = 'No se pudo generar el reporte de clientes';
        this.loading = false;
      }
    });
  }

  generarUsoCasilleros(): void {
    this.selectedReport = 'uso-casilleros';
    this.loading = true;
    this.error = '';

    forkJoin({
      ingresos: this.ingresoService.getAll(),
      casilleros: this.casilleroService.getAll(),
      prestamosActivos: this.casilleroService.getPrestamosActivos()
    }).subscribe({
      next: ({ ingresos, casilleros, prestamosActivos }) => {
        this.ingresos = ingresos;
        this.casilleros = casilleros;
        this.prestamosActivos = [...prestamosActivos].sort((a, b) =>
          new Date(b.fechaPrestamo).getTime() - new Date(a.fechaPrestamo).getTime()
        );
        this.casillerosDisponibles = casilleros.filter(c => c.estado === 'DISPONIBLE').length;
        this.casillerosOcupados = casilleros.filter(c => c.estado === 'OCUPADO').length;
        this.casillerosMantenimiento = casilleros.filter(c => c.estado === 'MANTENIMIENTO').length;

        const casillerosFisicos = casilleros.filter(c => c.tipo !== 'ESTANTE_RECEPCION');
        if (casillerosFisicos.length === 0) {
          this.historialCasilleros = [];
          this.finalizarReportePdf();
          return;
        }

        forkJoin(casillerosFisicos.map(c => this.casilleroService.getHistorial(c.id))).subscribe({
          next: (historiales) => {
            this.historialCasilleros = historiales
              .flat()
              .sort((a, b) => new Date(b.fechaPrestamo).getTime() - new Date(a.fechaPrestamo).getTime());
            this.finalizarReportePdf();
          },
          error: () => {
            this.error = 'No se pudo cargar el historial de casilleros';
            this.loading = false;
          }
        });
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

  obtenerTipoSuscripcionActiva(clienteId: string): string {
    const suscripcion = this.suscripciones.find(s =>
      s.clienteId === clienteId && ((s.estaVigente ?? false) || s.estado?.toUpperCase() === 'ACTIVA')
    );
    return suscripcion ? suscripcion.tipo : 'Sin suscripción activa';
  }

  obtenerResumenIngreso(ingresoId: string): string {
    const ingreso = this.ingresos.find(i => i.id === ingresoId);
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

  private esMismoMes(a: Date, b: Date): boolean {
    return a.getFullYear() === b.getFullYear()
      && a.getMonth() === b.getMonth();
  }

  private esMismoAnio(a: Date, b: Date): boolean {
    return a.getFullYear() === b.getFullYear();
  }

  private calcularMediaUsuariosDia(ingresosHoy: Ingreso[], ahora: Date): number {
    const horasTranscurridas = Math.max(1, ahora.getHours() + 1);
    const usuariosUnicosHoy = new Set(ingresosHoy.map(i => i.clienteId)).size;
    return usuariosUnicosHoy / horasTranscurridas;
  }

  private calcularMediaUsuariosMes(ingresosMes: Ingreso[], ahora: Date): number {
    const diasTranscurridos = Math.max(1, ahora.getDate());
    const usuariosUnicosMes = new Set(ingresosMes.map(i => i.clienteId)).size;
    return usuariosUnicosMes / diasTranscurridos;
  }

  private calcularMediaUsuariosAnio(ingresosAnio: Ingreso[], ahora: Date): number {
    const mesesTranscurridos = Math.max(1, ahora.getMonth() + 1);
    const usuariosUnicosAnio = new Set(ingresosAnio.map(i => i.clienteId)).size;
    return usuariosUnicosAnio / mesesTranscurridos;
  }

  private finalizarReportePdf(): void {
    setTimeout(() => {
      this.exportarPdf();
      this.selectedReport = '';
      this.loading = false;
    }, 50);
  }

  private generarHtmlReporte(): string {
    const fechaGeneracion = new Date().toLocaleString('es-BO');
    const titulo = this.tituloResultado;
    const cuerpo = this.generarContenidoReporte();

    return `
      <!DOCTYPE html>
      <html lang="es">
      <head>
        <meta charset="UTF-8">
        <title>${titulo}</title>
        <style>
          body { font-family: Arial, sans-serif; padding: 24px; color: #111; }
          h1 { margin: 0 0 8px; font-size: 24px; }
          .meta { color: #555; margin-bottom: 20px; font-size: 13px; }
          .stats { display: grid; grid-template-columns: repeat(2, minmax(180px, 1fr)); gap: 12px; margin-bottom: 20px; }
          .stat { border: 1px solid #ddd; border-radius: 8px; padding: 12px; }
          .stat-label { color: #555; font-size: 12px; display: block; margin-bottom: 6px; }
          .stat-value { font-size: 22px; font-weight: 700; }
          table { width: 100%; border-collapse: collapse; margin-top: 12px; }
          th, td { border: 1px solid #ddd; padding: 8px; text-align: left; font-size: 12px; }
          th { background: #f3f3f3; }
          .section { margin-bottom: 24px; }
          .empty { color: #666; font-style: italic; }
          @media print {
            body { padding: 0; }
          }
        </style>
      </head>
      <body>
        <h1>${titulo}</h1>
        <div class="meta">Generado: ${fechaGeneracion}</div>
        ${cuerpo}
      </body>
      </html>
    `;
  }

  private generarContenidoReporte(): string {
    switch (this.selectedReport) {
      case 'ingresos-mes':
        return this.generarHtmlIngresos();
      case 'ingresos-diarios':
        return this.generarHtmlIngresosDiarios();
      case 'ingresos-anuales':
        return this.generarHtmlIngresosAnuales();
      case 'ingresos-economicos':
        return this.generarHtmlIngresosEconomicos();
      case 'clientes-activos':
        return this.generarHtmlClientesActivos();
      case 'uso-casilleros':
        return this.generarHtmlUsoCasilleros();
      default:
        return '<p class="empty">No hay reporte seleccionado.</p>';
    }
  }

  private generarHtmlIngresos(): string {
    const filas = this.ingresosMes.map(ingreso => `
      <tr>
        <td>${this.formatearFecha(ingreso.fechaIngreso)}</td>
        <td>${ingreso.ciCliente}</td>
        <td>${ingreso.nombreCliente}</td>
        <td>${this.formatearHora(ingreso.horaIngreso)}</td>
        <td>${this.formatearHora(ingreso.horaSalida)}</td>
        <td>${ingreso.salidaRegistrada ? 'Egresó' : 'En gimnasio'}</td>
      </tr>
    `).join('');

    return `
      <div class="section">
        <div class="stats">
          <div class="stat"><span class="stat-label">Ingresos de hoy</span><span class="stat-value">${this.ingresosHoy.length}</span></div>
          <div class="stat"><span class="stat-label">Egresos de hoy</span><span class="stat-value">${this.egresosHoy.length}</span></div>
          <div class="stat"><span class="stat-label">Ingresos del mes</span><span class="stat-value">${this.ingresosMes.length}</span></div>
          <div class="stat"><span class="stat-label">Activos ahora</span><span class="stat-value">${this.ingresosActivos.length}</span></div>
          <div class="stat"><span class="stat-label">Media usuarios por hora hoy</span><span class="stat-value">${this.mediaUsuariosDia.toFixed(2)}</span></div>
          <div class="stat"><span class="stat-label">Media usuarios por día del mes</span><span class="stat-value">${this.mediaUsuariosMes.toFixed(2)}</span></div>
        </div>
        ${this.ingresosMes.length ? `
          <table>
            <thead>
              <tr><th>Fecha</th><th>CI</th><th>Cliente</th><th>Hora Ingreso</th><th>Hora Salida</th><th>Estado</th></tr>
            </thead>
            <tbody>${filas}</tbody>
          </table>
        ` : '<p class="empty">No hay ingresos registrados este mes.</p>'}
      </div>
    `;
  }

  private generarHtmlIngresosDiarios(): string {
    const filas = this.ingresosHoy.map(ingreso => `
      <tr>
        <td>${ingreso.ciCliente}</td>
        <td>${ingreso.nombreCliente}</td>
        <td>${this.formatearHora(ingreso.horaIngreso)}</td>
        <td>${this.formatearHora(ingreso.horaSalida)}</td>
        <td>${ingreso.salidaRegistrada ? 'Egresó' : 'En gimnasio'}</td>
      </tr>
    `).join('');

    return `
      <div class="section">
        <div class="stats">
          <div class="stat"><span class="stat-label">Ingresos de hoy</span><span class="stat-value">${this.ingresosHoy.length}</span></div>
          <div class="stat"><span class="stat-label">Egresos de hoy</span><span class="stat-value">${this.egresosHoy.length}</span></div>
          <div class="stat"><span class="stat-label">Activos ahora</span><span class="stat-value">${this.ingresosActivos.length}</span></div>
          <div class="stat"><span class="stat-label">Media usuarios por hora hoy</span><span class="stat-value">${this.mediaUsuariosDia.toFixed(2)}</span></div>
        </div>
        ${this.ingresosHoy.length ? `
          <table>
            <thead>
              <tr><th>CI</th><th>Cliente</th><th>Hora Ingreso</th><th>Hora Salida</th><th>Estado</th></tr>
            </thead>
            <tbody>${filas}</tbody>
          </table>
        ` : '<p class="empty">No hay ingresos registrados hoy.</p>'}
      </div>
    `;
  }

  private generarHtmlIngresosAnuales(): string {
    const filas = this.ingresosAnio.map(ingreso => `
      <tr>
        <td>${this.formatearFecha(ingreso.fechaIngreso)}</td>
        <td>${ingreso.ciCliente}</td>
        <td>${ingreso.nombreCliente}</td>
        <td>${this.formatearHora(ingreso.horaIngreso)}</td>
        <td>${this.formatearHora(ingreso.horaSalida)}</td>
        <td>${ingreso.salidaRegistrada ? 'Egresó' : 'En gimnasio'}</td>
      </tr>
    `).join('');

    return `
      <div class="section">
        <div class="stats">
          <div class="stat"><span class="stat-label">Ingresos del año</span><span class="stat-value">${this.ingresosAnio.length}</span></div>
          <div class="stat"><span class="stat-label">Egresos del año</span><span class="stat-value">${this.egresosAnio.length}</span></div>
          <div class="stat"><span class="stat-label">Clientes únicos del año</span><span class="stat-value">${this.clientesUnicosAnio}</span></div>
          <div class="stat"><span class="stat-label">Media usuarios por mes</span><span class="stat-value">${this.mediaUsuariosAnio.toFixed(2)}</span></div>
        </div>
        ${this.ingresosAnio.length ? `
          <table>
            <thead>
              <tr><th>Fecha</th><th>CI</th><th>Cliente</th><th>Hora Ingreso</th><th>Hora Salida</th><th>Estado</th></tr>
            </thead>
            <tbody>${filas}</tbody>
          </table>
        ` : '<p class="empty">No hay ingresos registrados este año.</p>'}
      </div>
    `;
  }

  private generarHtmlIngresosEconomicos(): string {
    const filas = this.suscripcionesMes.map(suscripcion => `
      <tr>
        <td>${this.formatearFecha(suscripcion.fechaInicio)}</td>
        <td>${this.obtenerNombreCliente(suscripcion.clienteId)}</td>
        <td>${suscripcion.tipo}</td>
        <td>${suscripcion.precio} Bs</td>
        <td>${suscripcion.estado}</td>
      </tr>
    `).join('');

    return `
      <div class="section">
        <div class="stats">
          <div class="stat"><span class="stat-label">Ingresos de hoy</span><span class="stat-value">${this.totalIngresosHoy.toFixed(2)} Bs</span></div>
          <div class="stat"><span class="stat-label">Pagos de hoy</span><span class="stat-value">${this.suscripcionesHoy.length}</span></div>
          <div class="stat"><span class="stat-label">Clientes que pagaron hoy</span><span class="stat-value">${this.clientesConPagoHoy}</span></div>
          <div class="stat"><span class="stat-label">Ingresos del mes</span><span class="stat-value">${this.totalIngresosMes.toFixed(2)} Bs</span></div>
          <div class="stat"><span class="stat-label">Suscripciones del mes</span><span class="stat-value">${this.suscripcionesMes.length}</span></div>
          <div class="stat"><span class="stat-label">Clientes que pagaron</span><span class="stat-value">${this.clientesConPagoMes}</span></div>
        </div>
        ${this.suscripcionesMes.length ? `
          <table>
            <thead>
              <tr><th>Fecha</th><th>Cliente</th><th>Tipo</th><th>Pago</th><th>Estado</th></tr>
            </thead>
            <tbody>${filas}</tbody>
          </table>
        ` : '<p class="empty">No hay pagos registrados este mes.</p>'}
      </div>
    `;
  }

  private generarHtmlClientesActivos(): string {
    const filas = this.clientesActivos.map(cliente => `
      <tr>
        <td>${cliente.ci}</td>
        <td>${cliente.nombre} ${cliente.apellido}</td>
        <td>${cliente.email || '-'}</td>
        <td>${cliente.telefono || '-'}</td>
        <td>${this.obtenerTipoSuscripcionActiva(cliente.id)}</td>
      </tr>
    `).join('');

    return `
      <div class="section">
        <div class="stats">
          <div class="stat"><span class="stat-label">Clientes activos</span><span class="stat-value">${this.clientesActivos.length}</span></div>
          <div class="stat"><span class="stat-label">Clientes registrados este mes</span><span class="stat-value">${this.clientesMes.length}</span></div>
          <div class="stat"><span class="stat-label">Clientes con suscripción</span><span class="stat-value">${this.clientesSuscritos.length}</span></div>
        </div>
        ${this.clientesActivos.length ? `
          <table>
            <thead>
              <tr><th>CI</th><th>Cliente</th><th>Email</th><th>Teléfono</th><th>Suscripción</th></tr>
            </thead>
            <tbody>${filas}</tbody>
          </table>
        ` : '<p class="empty">No hay clientes activos.</p>'}
      </div>
    `;
  }

  private generarHtmlUsoCasilleros(): string {
    const filas = this.historialCasilleros.map(prestamo => `
      <tr>
        <td>#${prestamo.numeroCasillero}</td>
        <td>${this.obtenerResumenIngreso(prestamo.ingresoId)}</td>
        <td>${prestamo.ciDepositado || '-'}</td>
        <td>${this.formatearFechaHora(prestamo.fechaPrestamo)}</td>
        <td>${this.formatearFechaHora(prestamo.fechaDevolucion)}</td>
        <td>${prestamo.devuelto ? 'Devuelto' : 'Activo'}</td>
      </tr>
    `).join('');

    return `
      <div class="section">
        <div class="stats">
          <div class="stat"><span class="stat-label">Disponibles</span><span class="stat-value">${this.casillerosDisponibles}</span></div>
          <div class="stat"><span class="stat-label">Ocupados</span><span class="stat-value">${this.casillerosOcupados}</span></div>
          <div class="stat"><span class="stat-label">Mantenimiento</span><span class="stat-value">${this.casillerosMantenimiento}</span></div>
          <div class="stat"><span class="stat-label">Usos históricos</span><span class="stat-value">${this.historialCasilleros.length}</span></div>
        </div>
        ${this.historialCasilleros.length ? `
          <table>
            <thead>
              <tr><th>Casillero</th><th>Ingreso asociado</th><th>CI depositado</th><th>Ingreso</th><th>Devolución</th><th>Estado</th></tr>
            </thead>
            <tbody>${filas}</tbody>
          </table>
        ` : '<p class="empty">No hay historial de uso de casilleros.</p>'}
      </div>
    `;
  }
}

