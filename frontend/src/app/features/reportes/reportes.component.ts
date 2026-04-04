import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

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
            <button class="btn btn-primary">Generar</button>
          </div>
          <div class="reporte-card">
            <h4>💰 Ingresos Económicos</h4>
            <p>Resumen financiero de suscripciones</p>
            <button class="btn btn-primary">Generar</button>
          </div>
          <div class="reporte-card">
            <h4>👥 Clientes Activos</h4>
            <p>Lista de clientes con suscripciones vigentes</p>
            <button class="btn btn-primary">Generar</button>
          </div>
          <div class="reporte-card">
            <h4>🔒 Uso de Casilleros</h4>
            <p>Estadísticas de ocupación de casilleros</p>
            <button class="btn btn-primary">Generar</button>
          </div>
        </div>
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
  `]
})
export class ReportesComponent {}
