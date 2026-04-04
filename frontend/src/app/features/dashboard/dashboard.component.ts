import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  menuItems = [
    { path: '/dashboard', icon: '📊', label: 'Dashboard' },
    { path: '/ingresos', icon: '🚪', label: 'Ingresos/Salidas' },
    { path: '/clientes', icon: '👥', label: 'Clientes' },
    { path: '/suscripciones', icon: '💳', label: 'Suscripciones' },
    { path: '/casilleros', icon: '🔒', label: 'Casilleros' },
    { path: '/reportes', icon: '📈', label: 'Reportes' },
    { path: '/admin', icon: '⚙️', label: 'Administración' }
  ];

  currentTime = new Date();

  ngOnInit() {
    setInterval(() => {
      this.currentTime = new Date();
    }, 1000);
  }
}
