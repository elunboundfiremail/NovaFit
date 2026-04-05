import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClienteService } from '../../core/services/cliente.service';
import { IngresoService } from '../../core/services/ingreso.service';
import { Cliente, Ingreso } from '../../core/models/models';

@Component({
  selector: 'app-clientes',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})
export class ClientesComponent implements OnInit {
  clientes: Cliente[] = [];
  clienteSeleccionado: Cliente | null = null;
  historialIngresos: Ingreso[] = [];
  
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

  cargarClientes() {
    this.clienteService.getAll().subscribe({
      next: (data) => this.clientes = data,
      error: () => console.error('Error al cargar clientes')
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
  }

  guardarCliente() {
    if (!this.formulario.ci || !this.formulario.nombre || !this.formulario.apellido) {
      alert('CI, nombre y apellido son obligatorios');
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

    this.clienteService.create(payload).subscribe({
      next: (clienteCreado) => {
        this.cargarClientes();
        this.clienteSeleccionado = clienteCreado;
        this.nuevoCliente();
        alert('✅ Cliente creado exitosamente');
      },
      error: (err) => {
        const mensaje = typeof err.error === 'string'
          ? err.error
          : err.error?.message || err.message || 'No se pudo crear el cliente';
        alert(`❌ Error: ${mensaje}`);
      }
    });
  }
}
