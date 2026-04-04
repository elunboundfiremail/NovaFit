import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CasilleroService } from '../../core/services/casillero.service';
import { Casillero, PrestamoCasillero } from '../../core/models/models';

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
  
  filtroTipo: string = 'TODOS';

  constructor(private casilleroService: CasilleroService) {}

  ngOnInit() {
    this.cargarCasilleros();
    this.cargarPrestamos();
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

  get casillerosFiltrados() {
    if (this.filtroTipo === 'TODOS') return this.casilleros;
    return this.casilleros.filter(c => c.tipo === this.filtroTipo);
  }

  getClaseEstado(estado: string): string {
    return {
      'DISPONIBLE': 'estado-disponible',
      'OCUPADO': 'estado-ocupado',
      'MANTENIMIENTO': 'estado-mantenimiento'
    }[estado] || '';
  }

  devolverCasillero(prestamoId: string) {
    this.casilleroService.devolver(prestamoId).subscribe({
      next: () => {
        this.cargarCasilleros();
        this.cargarPrestamos();
      },
      error: () => alert('Error al devolver casillero')
    });
  }
}
