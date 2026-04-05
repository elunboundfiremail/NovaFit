import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CasilleroService } from '../../core/services/casillero.service';
import { IngresoService } from '../../core/services/ingreso.service';
import { Casillero, Ingreso, PrestamoCasillero } from '../../core/models/models';

@Component({
  selector: 'app-casilleros',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './casilleros.component.html',
  styleUrls: ['./casilleros.component.css']
})
export class CasillerosComponent implements OnInit {
  casilleros: Casillero[] = [];
  prestamosActivos: PrestamoCasillero[] = [];
  ingresosActivos: Ingreso[] = [];
  
  filtroTipo: string = 'TODOS';
  casilleroSeleccionado: Casillero | null = null;
  prestamoForm = {
    ingresoId: '',
    numeroTicket: '',
    ciDepositado: null as number | null
  };

  constructor(
    private casilleroService: CasilleroService,
    private ingresoService: IngresoService
  ) {}

  ngOnInit() {
    this.cargarCasilleros();
    this.cargarPrestamos();
    this.cargarIngresosActivos();
  }

  cargarCasilleros() {
    this.casilleroService.getAll().subscribe({
      next: (data) => this.casilleros = data,
      error: () => console.error('Error al cargar casilleros')
    });
  }

  cargarPrestamos() {
    this.casilleroService.getPrestamosActivos().subscribe({
      next: (data) => this.prestamosActivos = data,
      error: () => console.error('Error al cargar préstamos')
    });
  }

  cargarIngresosActivos() {
    this.ingresoService.getAll().subscribe({
      next: (data) => this.ingresosActivos = data.filter(i => !i.salidaRegistrada),
      error: () => console.error('Error al cargar ingresos activos')
    });
  }

  get casillerosFiltrados() {
    if (this.filtroTipo === 'TODOS') return this.casilleros;
    return this.casilleros.filter(c => c.tipo === this.filtroTipo);
  }

  getClaseEstado(estado: string): string {
    return {
      'DISPONIBLE': 'estado-disponible',
      'OCUPADO': 'estado-ocupado',
      'MANTENIMIENTO': 'estado-mantenimiento',
      'EN_MANTENIMIENTO': 'estado-mantenimiento'
    }[estado] || '';
  }

  seleccionarCasillero(casillero: Casillero) {
    if (casillero.estado !== 'DISPONIBLE') {
      return;
    }

    this.casilleroSeleccionado = casillero;
    this.prestamoForm = {
      ingresoId: '',
      numeroTicket: '',
      ciDepositado: null
    };
  }

  asignarCasillero() {
    if (!this.casilleroSeleccionado) {
      alert('Seleccione un casillero disponible');
      return;
    }

    if (!this.prestamoForm.ingresoId) {
      alert('Seleccione un ingreso activo');
      return;
    }

    this.casilleroService.prestar({
      casilleroId: this.casilleroSeleccionado.id,
      ingresoId: this.prestamoForm.ingresoId,
      numeroTicket: this.prestamoForm.numeroTicket || undefined,
      ciDepositado: this.prestamoForm.ciDepositado ?? undefined
    }).subscribe({
      next: () => {
        alert('Casillero asignado correctamente');
        this.casilleroSeleccionado = null;
        this.cargarCasilleros();
        this.cargarPrestamos();
        this.cargarIngresosActivos();
      },
      error: (err) => {
        const mensaje = typeof err.error === 'string'
          ? err.error
          : err.error?.message || err.message || 'No se pudo asignar el casillero';
        alert(`Error al asignar casillero: ${mensaje}`);
      }
    });
  }

  devolverCasillero(prestamoId: string) {
    this.casilleroService.devolver(prestamoId).subscribe({
      next: () => {
        this.cargarCasilleros();
        this.cargarPrestamos();
        this.cargarIngresosActivos();
      },
      error: () => alert('Error al devolver casillero')
    });
  }
}
