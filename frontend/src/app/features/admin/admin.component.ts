import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {
  autenticado = false;
  usuario = '';
  contrasena = '';
  seccionActiva = 'clientes';

  verificarAcceso() {
    if (this.usuario === 'admin' && this.contrasena === 'admin') {
      this.autenticado = true;
      this.usuario = '';
      this.contrasena = '';
    } else {
      alert('❌ Credenciales incorrectas');
      this.contrasena = '';
    }
  }

  cerrarSesion() {
    this.autenticado = false;
  }
}
