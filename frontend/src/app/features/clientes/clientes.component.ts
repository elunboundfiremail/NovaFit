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

    this.clienteService.create(this.formulario).subscribe({
      next: () => {
        this.cargarClientes();
        this.nuevoCliente();
        alert('✅ Cliente creado exitosamente');
      },
      error: (err) => alert(`❌ Error: ${err.error?.message}`)
    });
  }
}
