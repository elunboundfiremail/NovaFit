import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username = '';
  password = '';
  loading = false;
  error = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {
    if (this.authService.isAuthenticated()) {
      this.router.navigateByUrl('/dashboard');
    }
  }

  ingresar() {
    if (!this.username.trim() || !this.password) {
      this.error = 'Ingrese usuario y contrasena';
      return;
    }

    this.loading = true;
    this.error = '';

    this.authService.login(this.username.trim(), this.password).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigateByUrl('/dashboard');
      },
      error: () => {
        this.loading = false;
        this.password = '';
        this.error = 'Credenciales incorrectas o servicio no disponible';
      }
    });
  }
}
