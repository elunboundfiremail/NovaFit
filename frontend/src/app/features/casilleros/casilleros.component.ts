import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { CasilleroService } from '../../core/services/casillero.service';
import { IngresoService } from '../../core/services/ingreso.service';
import { ClienteService } from '../../core/services/cliente.service';
import { Casillero, Cliente, Ingreso, PrestamoCasillero } from '../../core/models/models';

@Component({
  selector: 'app-casilleros',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './casilleros.component.html',
  styleUrls: ['./casilleros.component.css']
})
export class CasillerosComponent implements OnInit {
  casilleros: Casillero[] = [];
  prestamosActivos: PrestamoCasillero[] = [];
  ingresosActivos: Ingreso[] = [];
  clientes: Cliente[] = [];
  mensaje = '';
  
  filtroTipo: string = 'TODOS';
  casilleroSeleccionado: Casillero | null = null;
  editandoCasilleroId: string | null = null;
  modalCasilleroAbierto = false;
  modalPrestamoAbierto = false;
  modalTicketAbierto = false;
  prestamoCasilleroForm = {
    ciCliente: '',
    ciDepositado: null as number | null
  };
  ticketForm = {
    ciCliente: '',
    descripcion: ''
  };
  casilleroForm = {
    numero: 0,
    tipo: 'TEMPORAL' as 'FIJO' | 'TEMPORAL' | 'ESTANTE_RECEPCION',
    estado: 'DISPONIBLE' as 'DISPONIBLE' | 'MANTENIMIENTO',
    ubicacion: ''
  };

  constructor(
    private casilleroService: CasilleroService,
    private ingresoService: IngresoService,
    private clienteService: ClienteService
  ) {}

  ngOnInit() {
    this.cargarCasilleros();
    this.cargarPrestamos();
    this.cargarIngresosActivos();
    this.cargarClientes();
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

  cargarClientes() {
    this.clienteService.getAll().subscribe({
      next: (data) => this.clientes = data,
      error: () => console.error('Error al cargar clientes')
    });
  }

  get casillerosFiltrados() {
    const casillerosFisicos = this.casilleros.filter(c => c.tipo !== 'ESTANTE_RECEPCION');
    if (this.filtroTipo === 'TODOS') return casillerosFisicos;
    return casillerosFisicos.filter(c => c.tipo === this.filtroTipo);
  }

  get ticketsRecepcion() {
    return this.prestamosActivos.filter(prestamo => prestamo.tipoResguardo === 'TICKET' && !!prestamo.numeroTicket);
  }

  get clientesSugeridosPrestamo() {
    return this.filtrarClientesPorCi(this.prestamoCasilleroForm.ciCliente);
  }

  get clientesSugeridosTicket() {
    return this.filtrarClientesPorCi(this.ticketForm.ciCliente);
  }

  obtenerPrestamoActivo(casilleroId?: string) {
    return this.prestamosActivos.find(prestamo => prestamo.casilleroId === casilleroId);
  }

  obtenerCasilleroPorId(casilleroId?: string) {
    if (!casilleroId) {
      return null;
    }

    return this.casilleros.find(casillero => casillero.id === casilleroId) ?? null;
  }

  get ingresoPrestamoSeleccionado(): Ingreso | null {
    const ci = this.obtenerCiNumerico(this.prestamoCasilleroForm.ciCliente);
    if (!ci) {
      return null;
    }

    return this.ingresosActivos.find(ingreso => ingreso.ciCliente === ci) ?? null;
  }

  get ingresoTicketSeleccionado(): Ingreso | null {
    const ci = this.obtenerCiNumerico(this.ticketForm.ciCliente);
    if (!ci) {
      return null;
    }

    return this.ingresosActivos.find(ingreso => ingreso.ciCliente === ci) ?? null;
  }

  get clientePrestamoSeleccionado(): Cliente | null {
    const ci = this.obtenerCiNumerico(this.prestamoCasilleroForm.ciCliente);
    if (!ci) {
      return null;
    }

    return this.clientes.find(cliente => cliente.ci === ci) ?? null;
  }

  get clienteTicketSeleccionado(): Cliente | null {
    const ci = this.obtenerCiNumerico(this.ticketForm.ciCliente);
    if (!ci) {
      return null;
    }

    return this.clientes.find(cliente => cliente.ci === ci) ?? null;
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
    this.prestamoCasilleroForm = {
      ciCliente: '',
      ciDepositado: null
    };
  }

  abrirPrestamo(casillero: Casillero) {
    this.seleccionarCasillero(casillero);
    this.modalPrestamoAbierto = true;
    this.mensaje = `🔐 Asignando casillero #${casillero.numero}`;
  }

  cerrarModalPrestamo() {
    this.modalPrestamoAbierto = false;
    this.casilleroSeleccionado = null;
    this.prestamoCasilleroForm = {
      ciCliente: '',
      ciDepositado: null
    };
  }

  abrirModalTicket() {
    this.modalTicketAbierto = true;
    this.ticketForm = {
      ciCliente: '',
      descripcion: ''
    };
  }

  cerrarModalTicket() {
    this.modalTicketAbierto = false;
    this.ticketForm = {
      ciCliente: '',
      descripcion: ''
    };
  }

  devolverPrestamoDeCasillero(casillero: Casillero) {
    const prestamo = this.obtenerPrestamoActivo(casillero.id);
    if (!prestamo) {
      this.mensaje = '❌ No se encontró un préstamo activo para este casillero';
      return;
    }

    this.devolverCasillero(prestamo.id);
  }

  asignarCasillero() {
    if (!this.casilleroSeleccionado) {
      alert('Seleccione un casillero disponible');
      return;
    }

    if (!this.obtenerCiNumerico(this.prestamoCasilleroForm.ciCliente)) {
      alert('Ingrese el CI del cliente');
      return;
    }

    const ingreso = this.ingresoPrestamoSeleccionado;
    if (!ingreso) {
      this.mensaje = this.clientePrestamoSeleccionado
        ? '❌ El cliente existe, pero primero debe registrar su ingreso del día'
        : '❌ El CI no existe en la base de datos. Registre primero al cliente';
      return;
    }

    this.casilleroService.prestar({
      casilleroId: this.casilleroSeleccionado.id,
      ingresoId: ingreso.id,
      numeroLlave: this.casilleroSeleccionado.numero.toString(),
      ciDepositado: this.prestamoCasilleroForm.ciDepositado ?? undefined
    }).subscribe({
      next: () => {
        this.mensaje = '✅ Casillero asignado correctamente';
        this.cerrarModalPrestamo();
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

  registrarTicketRecepcion() {
    if (!this.obtenerCiNumerico(this.ticketForm.ciCliente)) {
      this.mensaje = '❌ Ingrese el CI del cliente';
      return;
    }

    const ingreso = this.ingresoTicketSeleccionado;
    if (!ingreso) {
      this.mensaje = this.clienteTicketSeleccionado
        ? '❌ El cliente existe, pero primero debe registrar su ingreso del día'
        : '❌ El CI no existe en la base de datos. Registre primero al cliente';
      return;
    }

    this.casilleroService.registrarTicketRecepcion({
      ingresoId: ingreso.id,
      descripcion: this.ticketForm.descripcion.trim() || undefined
    }).subscribe({
      next: () => {
        this.mensaje = '✅ Ticket registrado en recepción';
        this.cerrarModalTicket();
        this.cargarPrestamos();
      },
      error: (err) => {
        const mensaje = typeof err.error === 'string'
          ? err.error
          : err.error?.message || err.message || 'No se pudo registrar el ticket';
        this.mensaje = `❌ Error al registrar ticket: ${mensaje}`;
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
    this.modalCasilleroAbierto = true;
    this.mensaje = `✏️ Editando casillero #${casillero.numero}`;
  }

  abrirModalNuevoCasillero() {
    this.editandoCasilleroId = null;
    this.casilleroSeleccionado = null;
    this.modalCasilleroAbierto = true;
    this.casilleroForm = {
      numero: 0,
      tipo: 'TEMPORAL',
      estado: 'DISPONIBLE',
      ubicacion: ''
    };
  }

  cerrarModalCasillero() {
    this.modalCasilleroAbierto = false;
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
        this.cerrarModalCasillero();
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
          this.cerrarModalCasillero();
        }
        this.cargarCasilleros();
      },
      error: (err) => {
        const detalle = typeof err.error === 'string' ? err.error : err.error?.message;
        this.mensaje = `❌ ${detalle || 'No se pudo eliminar el casillero'}`;
      }
    });
  }

  seleccionarClientePrestamo(cliente: Cliente) {
    this.prestamoCasilleroForm.ciCliente = cliente.ci.toString();
  }

  seleccionarClienteTicket(cliente: Cliente) {
    this.ticketForm.ciCliente = cliente.ci.toString();
  }

  private filtrarClientesPorCi(valor: string): Cliente[] {
    const termino = valor.trim();
    if (!termino) {
      return this.clientes.slice(0, 5);
    }

    return this.clientes
      .filter(cliente => cliente.ci.toString().startsWith(termino))
      .slice(0, 5);
  }

  private obtenerCiNumerico(valor: string): number | null {
    const limpio = valor.trim();
    if (!/^\d+$/.test(limpio)) {
      return null;
    }

    const numero = Number(limpio);
    return Number.isFinite(numero) ? numero : null;
  }
}
