import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ClienteService } from '../../core/services/cliente.service';
import { IngresoService } from '../../core/services/ingreso.service';
import { Cliente, Ingreso } from '../../core/models/models';

@Component({
  selector: 'app-clientes',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})
export class ClientesComponent implements OnInit {
  clientes: Cliente[] = [];
  clienteSeleccionado: Cliente | null = null;
  historialIngresos: Ingreso[] = [];
  loading = false;
  mensaje = '';
  busqueda = '';
  
  // Formulario
  modoEdicion = false;
  formulario = {
    ci: 0,
    nombre: '',
    apellido: '',
    email: '',
    telefono: '',
    fechaNacimiento: ''
  };

  constructor(
    private clienteService: ClienteService,
    private ingresoService: IngresoService
  ) {}

  ngOnInit() {
    this.cargarClientes();
  }

  get clientesFiltrados(): Cliente[] {
    const termino = this.busqueda.trim().toLowerCase();
    if (!termino) {
      return this.clientes;
    }

    return this.clientes.filter(cliente =>
      cliente.ci.toString().includes(termino)
      || `${cliente.nombre} ${cliente.apellido}`.toLowerCase().includes(termino)
    );
  }

  cargarClientes() {
    this.loading = true;
    this.clienteService.getAll().subscribe({
      next: (data) => {
        this.clientes = data;
        this.loading = false;
      },
      error: () => {
        this.mensaje = '❌ Error al cargar clientes';
        this.loading = false;
      }
    });
  }

  seleccionarCliente(cliente: Cliente) {
    this.clienteSeleccionado = cliente;
    this.cargarHistorial(cliente.id);
  }

  cargarHistorial(clienteId: string) {
    this.ingresoService.getByCliente(clienteId).subscribe({
      next: (data) => this.historialIngresos = data,
      error: () => console.error('Error al cargar historial')
    });
  }

  nuevoCliente() {
    this.modoEdicion = false;
    this.formulario = {
      ci: 0,
      nombre: '',
      apellido: '',
      email: '',
      telefono: '',
      fechaNacimiento: ''
    };
    this.clienteSeleccionado = null;
    this.mensaje = '';
  }

  editarCliente(cliente: Cliente) {
    this.modoEdicion = true;
    this.clienteSeleccionado = cliente;
    this.formulario = {
      ci: cliente.ci,
      nombre: cliente.nombre,
      apellido: cliente.apellido,
      email: cliente.email || '',
      telefono: cliente.telefono || '',
      fechaNacimiento: cliente.fechaNacimiento ? cliente.fechaNacimiento.slice(0, 10) : ''
    };
    this.mensaje = `✏️ Editando cliente ${cliente.nombre} ${cliente.apellido}`;
  }

  guardarCliente() {
    if (!this.formulario.ci || !this.formulario.nombre || !this.formulario.apellido) {
      this.mensaje = '❌ CI, nombre y apellido son obligatorios';
      return;
    }

    const payload = {
      ci: this.formulario.ci,
      nombre: this.formulario.nombre.trim(),
      apellido: this.formulario.apellido.trim(),
      email: this.formulario.email.trim() || undefined,
      telefono: this.formulario.telefono.trim() || undefined,
      fechaNacimiento: this.formulario.fechaNacimiento || undefined
    };

    const request = this.modoEdicion && this.clienteSeleccionado
      ? this.clienteService.update(this.clienteSeleccionado.id, payload)
      : this.clienteService.create(payload);

    const estabaEditando = this.modoEdicion;

    request.subscribe({
      next: (clienteGuardado) => {
        this.clienteSeleccionado = clienteGuardado;
        this.nuevoCliente();
        this.mensaje = estabaEditando
          ? '✅ Cliente actualizado exitosamente'
          : '✅ Cliente creado exitosamente';
        this.cargarClientes();
      },
      error: (err) => {
        const mensaje = typeof err.error === 'string'
          ? err.error
          : err.error?.message || err.message || 'No se pudo crear el cliente';
        this.mensaje = `❌ Error: ${mensaje}`;
      }
    });
  }

  eliminarCliente(cliente: Cliente) {
    const confirmar = confirm(`¿Eliminar al cliente ${cliente.nombre} ${cliente.apellido}?`);
    if (!confirmar) {
      return;
    }

    this.clienteService.delete(cliente.id).subscribe({
      next: () => {
        if (this.clienteSeleccionado?.id === cliente.id) {
          this.clienteSeleccionado = null;
          this.historialIngresos = [];
        }
        this.nuevoCliente();
        this.mensaje = '✅ Cliente eliminado exitosamente';
        this.cargarClientes();
      },
      error: (err) => {
        const detalle = typeof err.error === 'string'
          ? err.error
          : err.error?.message || 'No se pudo eliminar el cliente';
        this.mensaje = `❌ Error: ${detalle}`;
      }
    });
  }
}
