import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
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
  @ViewChild('casilleroFormCard') casilleroFormCard?: ElementRef<HTMLDivElement>;

  casilleros: Casillero[] = [];
  prestamosActivos: PrestamoCasillero[] = [];
  ingresosActivos: Ingreso[] = [];
  mensaje = '';
  
  filtroTipo: string = 'TODOS';
  casilleroSeleccionado: Casillero | null = null;
  editandoCasilleroId: string | null = null;
  prestamoForm = {
    ingresoId: '',
    numeroTicket: '',
    ciDepositado: null as number | null
  };
  casilleroForm = {
    numero: 0,
    tipo: 'TEMPORAL' as 'FIJO' | 'TEMPORAL' | 'ESTANTE_RECEPCION',
    estado: 'DISPONIBLE' as 'DISPONIBLE' | 'MANTENIMIENTO',
    ubicacion: ''
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
        this.mensaje = '✅ Casillero asignado correctamente';
        this.casilleroSeleccionado = null;
        this.cargarCasilleros();
        this.cargarPrestamos();
        this.cargarIngresosActivos();
      },
      error: (err) => {
        const mensaje = typeof err.error === 'string'
          ? err.error
          : err.error?.message || err.message || 'No se pudo asignar el casillero';
        this.mensaje = `❌ Error al asignar casillero: ${mensaje}`;
      }
    });
  }

  devolverCasillero(prestamoId: string) {
    this.casilleroService.devolver(prestamoId).subscribe({
      next: () => {
        this.mensaje = '✅ Casillero devuelto correctamente';
        this.cargarCasilleros();
        this.cargarPrestamos();
        this.cargarIngresosActivos();
      },
      error: () => this.mensaje = '❌ Error al devolver casillero'
    });
  }

  editarCasillero(casillero: Casillero) {
    this.editandoCasilleroId = casillero.id;
    this.casilleroSeleccionado = null;
    this.casilleroForm = {
      numero: casillero.numero,
      tipo: casillero.tipo,
      estado: casillero.estado === 'MANTENIMIENTO' ? 'MANTENIMIENTO' : 'DISPONIBLE',
      ubicacion: casillero.ubicacion || ''
    };
    this.mensaje = `✏️ Editando casillero #${casillero.numero}`;
    setTimeout(() => {
      this.casilleroFormCard?.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }, 0);
  }

  nuevoCasillero() {
    this.editandoCasilleroId = null;
    this.casilleroForm = {
      numero: 0,
      tipo: 'TEMPORAL',
      estado: 'DISPONIBLE',
      ubicacion: ''
    };
  }

  guardarCasillero() {
    if (!this.casilleroForm.numero) {
      this.mensaje = '❌ Ingrese un número de casillero válido';
      return;
    }

    const payload = {
      numero: this.casilleroForm.numero,
      tipo: this.casilleroForm.tipo,
      estado: this.casilleroForm.estado,
      ubicacion: this.casilleroForm.ubicacion.trim() || undefined
    };

    const request = this.editandoCasilleroId
      ? this.casilleroService.update(this.editandoCasilleroId, payload)
      : this.casilleroService.create(payload);

    request.subscribe({
      next: () => {
        this.mensaje = this.editandoCasilleroId
          ? '✅ Casillero actualizado correctamente'
          : '✅ Casillero creado correctamente';
        this.nuevoCasillero();
        this.cargarCasilleros();
      },
      error: (err) => {
        const detalle = typeof err.error === 'string' ? err.error : err.error?.message;
        this.mensaje = `❌ ${detalle || 'No se pudo guardar el casillero'}`;
      }
    });
  }

  cambiarEstado(casillero: Casillero, estado: 'DISPONIBLE' | 'MANTENIMIENTO') {
    this.casilleroService.update(casillero.id, {
      numero: casillero.numero,
      tipo: casillero.tipo,
      estado,
      ubicacion: casillero.ubicacion
    }).subscribe({
      next: () => {
        this.mensaje = `✅ Casillero #${casillero.numero} actualizado a ${estado}`;
        this.cargarCasilleros();
      },
      error: (err) => {
        const detalle = typeof err.error === 'string' ? err.error : err.error?.message;
        this.mensaje = `❌ ${detalle || 'No se pudo cambiar el estado del casillero'}`;
      }
    });
  }

  eliminarCasillero(casillero: Casillero) {
    const confirmar = confirm(`¿Eliminar el casillero #${casillero.numero}?`);
    if (!confirmar) {
      return;
    }

    this.casilleroService.delete(casillero.id).subscribe({
      next: () => {
        this.mensaje = `✅ Casillero #${casillero.numero} eliminado`;
        if (this.editandoCasilleroId === casillero.id) {
          this.nuevoCasillero();
        }
        this.cargarCasilleros();
      },
      error: (err) => {
        const detalle = typeof err.error === 'string' ? err.error : err.error?.message;
        this.mensaje = `❌ ${detalle || 'No se pudo eliminar el casillero'}`;
      }
    });
  }
}
