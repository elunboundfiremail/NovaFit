import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IngresoService } from '../../core/services/ingreso.service';
import { ClienteService } from '../../core/services/cliente.service';
import { Ingreso } from '../../core/models/models';

@Component({
  selector: 'app-ingresos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './ingresos.component.html',
  styleUrls: ['./ingresos.component.css']
})
export class IngresosComponent implements OnInit {
  ci: number = 0;
  ingresos: Ingreso[] = [];
  loading = false;
  mensaje = '';

  constructor(
    private ingresoService: IngresoService,
    private clienteService: ClienteService
  ) {}

  ngOnInit() {
    this.cargarIngresos();
  }

  // Getter para filtrar ingresos activos (sin salida)
  get ingresosActivos(): Ingreso[] {
    return this.ingresos.filter(i => !i.salidaRegistrada);
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
    if (!this.ci || this.ci === 0) {
      this.mensaje = 'Ingrese un CI válido';
      return;
    }

    this.loading = true;
    this.ingresoService.registrarIngreso(this.ci).subscribe({
      next: (ingreso) => {
        this.mensaje = `✅ Ingreso registrado: ${ingreso.nombreCliente}`;
        this.ci = 0;
        this.cargarIngresos();
        this.loading = false;
        setTimeout(() => this.mensaje = '', 3000);
      },
      error: (err) => {
        this.mensaje = `❌ Error: ${err.error?.message || 'Cliente no encontrado'}`;
        this.loading = false;
      }
    });
  }

  registrarSalida(ingresoId: string) {
    this.loading = true;
    this.ingresoService.registrarSalida(ingresoId).subscribe({
      next: () => {
        this.mensaje = '✅ Salida registrada';
        this.cargarIngresos();
        this.loading = false;
        setTimeout(() => this.mensaje = '', 3000);
      },
      error: () => {
        this.mensaje = '❌ Error al registrar salida';
        this.loading = false;
      }
    });
  }
}
