import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { IngresoService } from '../../core/services/ingreso.service';
import { ClienteService } from '../../core/services/cliente.service';
import { Cliente, Ingreso } from '../../core/models/models';

@Component({
  selector: 'app-ingresos',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './ingresos.component.html',
  styleUrls: ['./ingresos.component.css']
})
export class IngresosComponent implements OnInit {
  ciBusqueda = '';
  ingresos: Ingreso[] = [];
  clientes: Cliente[] = [];
  loading = false;
  mensaje = '';

  constructor(
    private ingresoService: IngresoService,
    private clienteService: ClienteService
  ) {}

  ngOnInit() {
    this.cargarClientes();
    this.cargarIngresos();
  }

  get ingresosActivos(): Ingreso[] {
    return this.ingresos.filter(i => !i.salidaRegistrada);
  }

  get clienteSeleccionado(): Cliente | undefined {
    const ci = this.ciNormalizado;
    if (!ci) {
      return undefined;
    }

    return this.clientes.find(cliente => cliente.ci === ci);
  }

  get ciNormalizado(): number | null {
    const texto = this.ciBusqueda.trim();
    if (!texto) {
      return null;
    }

    const match = texto.match(/\d+/);
    if (!match) {
      return null;
    }

    const ci = Number(match[0]);
    return Number.isFinite(ci) && ci > 0 ? ci : null;
  }

  cargarClientes() {
    this.clienteService.getAll().subscribe({
      next: (clientes) => {
        this.clientes = clientes.sort((a, b) => a.ci - b.ci);
      },
      error: () => {
        this.mensaje = 'Error al cargar clientes para autocompletado';
      }
    });
  }

  cargarIngresos() {
    this.loading = true;
    this.ingresoService.getAll().subscribe({
      next: (data) => {
        this.ingresos = data;
        this.loading = false;
      },
      error: () => {
        this.mensaje = 'Error al cargar ingresos';
        this.loading = false;
      }
    });
  }

  registrarIngreso() {
    const ci = this.ciNormalizado;
    if (!ci) {
      this.mensaje = 'Ingrese un CI valido';
      return;
    }

    this.loading = true;
    this.ingresoService.registrarIngreso(ci).subscribe({
      next: (ingreso) => {
        this.mensaje = `Ingreso registrado: ${ingreso.nombreCliente}`;
        this.ciBusqueda = '';
        this.cargarIngresos();
        this.loading = false;
        setTimeout(() => this.mensaje = '', 3000);
      },
      error: (err) => {
        this.mensaje = `Error: ${err.error?.message || 'Cliente no encontrado'}`;
        this.loading = false;
      }
    });
  }

  registrarSalida(ingresoId: string) {
    this.loading = true;
    this.ingresoService.registrarSalida(ingresoId).subscribe({
      next: () => {
        this.mensaje = 'Salida registrada';
        this.cargarIngresos();
        this.loading = false;
        setTimeout(() => this.mensaje = '', 3000);
      },
      error: (err) => {
        const detalle = typeof err.error === 'string' ? err.error : err.error?.message;
        this.mensaje = detalle || 'Error al registrar salida';
        this.loading = false;
      }
    });
  }
}
