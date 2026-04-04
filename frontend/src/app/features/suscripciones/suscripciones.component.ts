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
    this.suscripcionService.getAll().subscribe({
      next: (data) => this.suscripciones = data,
      error: () => console.error('Error')
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
      alert('Seleccione un cliente');
      return;
    }

    this.suscripcionService.create(this.formulario).subscribe({
      next: () => {
        alert('✅ Suscripción creada');
        this.cargarSuscripciones();
        this.formulario.clienteId = '';
      },
      error: () => alert('❌ Error al crear suscripción')
    });
  }
}
