import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SuscripcionService } from '../../core/services/suscripcion.service';
import { ClienteService } from '../../core/services/cliente.service';
import { Suscripcion, Cliente } from '../../core/models/models';

@Component({
  selector: 'app-suscripciones',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './suscripciones.component.html',
  styleUrls: ['./suscripciones.component.css']
})
export class SuscripcionesComponent implements OnInit {
  suscripciones: Suscripcion[] = [];
  clientes: Cliente[] = [];
  loading = false;
  mensaje = '';
  editandoId: string | null = null;
  
  formulario: {
    clienteId: string;
    tipo: 'CASUAL' | 'MENSUAL' | 'ANUAL';
    precio: number;
  } = {
    clienteId: '',
    tipo: 'MENSUAL',
    precio: 0
  };

  precios = {
    'CASUAL': 25,
    'MENSUAL': 200,
    'ANUAL': 2000
  };

  constructor(
    private suscripcionService: SuscripcionService,
    private clienteService: ClienteService
  ) {}

  ngOnInit() {
    this.cargarSuscripciones();
    this.cargarClientes();
    this.actualizarPrecio();
  }

  cargarSuscripciones() {
    this.loading = true;
    this.suscripcionService.getAll().subscribe({
      next: (data) => {
        this.suscripciones = data;
        this.loading = false;
      },
      error: () => {
        this.mensaje = '❌ Error al cargar suscripciones';
        this.loading = false;
      }
    });
  }

  cargarClientes() {
    this.clienteService.getAll().subscribe({
      next: (data) => this.clientes = data,
      error: () => console.error('Error')
    });
  }

  actualizarPrecio() {
    this.formulario.precio = this.precios[this.formulario.tipo as keyof typeof this.precios] || 0;
  }

  crearSuscripcion() {
    if (!this.formulario.clienteId) {
      this.mensaje = '❌ Seleccione un cliente';
      return;
    }

    const request = this.editandoId
      ? this.suscripcionService.update(this.editandoId, this.formulario)
      : this.suscripcionService.create(this.formulario);

    request.subscribe({
      next: () => {
        this.mensaje = this.editandoId ? '✅ Suscripción actualizada' : '✅ Suscripción creada';
        this.cargarSuscripciones();
        this.resetFormulario();
      },
      error: (err) => {
        const detalle = typeof err.error === 'string' ? err.error : err.error?.message;
        this.mensaje = `❌ ${detalle || 'Error al guardar suscripción'}`;
      }
    });
  }

  editarSuscripcion(suscripcion: Suscripcion) {
    this.editandoId = suscripcion.id;
    this.formulario = {
      clienteId: suscripcion.clienteId,
      tipo: (suscripcion.tipo?.toUpperCase() || 'MENSUAL') as 'CASUAL' | 'MENSUAL' | 'ANUAL',
      precio: suscripcion.precio
    };
    this.actualizarPrecio();
    this.mensaje = `✏️ Editando suscripción ${suscripcion.id.substring(0, 8)}`;
  }

  eliminarSuscripcion(suscripcion: Suscripcion) {
    const confirmar = confirm(`¿Eliminar la suscripción de ${this.obtenerNombreCliente(suscripcion.clienteId)}?`);
    if (!confirmar) {
      return;
    }

    this.suscripcionService.delete(suscripcion.id).subscribe({
      next: () => {
        this.mensaje = '✅ Suscripción eliminada';
        this.cargarSuscripciones();
        if (this.editandoId === suscripcion.id) {
          this.resetFormulario();
        }
      },
      error: () => {
        this.mensaje = '❌ Error al eliminar suscripción';
      }
    });
  }

  cancelarEdicion() {
    this.resetFormulario();
  }

  obtenerNombreCliente(clienteId: string): string {
    const cliente = this.clientes.find(c => c.id === clienteId);
    return cliente ? `${cliente.nombre} ${cliente.apellido}` : `Cliente #${clienteId.substring(0, 8)}`;
  }

  private resetFormulario() {
    this.editandoId = null;
    this.formulario = {
      clienteId: '',
      tipo: 'MENSUAL',
      precio: 0
    };
    this.actualizarPrecio();
  }
}
