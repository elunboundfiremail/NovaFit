// Models matching backend DTOs
export interface Cliente {
  id: string;
  ci: number;
  nombre: string;
  apellido: string;
  email?: string;
  telefono?: string;
  fechaNacimiento?: string;
  fechaRegistro: string;
}

export interface Suscripcion {
  id: string;
  clienteId: string;
  tipo: 'CASUAL' | 'MENSUAL' | 'ANUAL';
  precio: number;
  fechaInicio: string;
  fechaVencimiento: string;
  estado: 'ACTIVA' | 'VENCIDA' | 'CANCELADA';
  casilleroFijoId?: string;
  promocionId?: string;
  descuentoAplicado: number;
  ingresosTotalesUsados: number;
  fechaCreacion: string;
  estaVigente?: boolean;
}

export interface Ingreso {
  id: string;
  clienteId: string;
  suscripcionId?: string;
  fechaIngreso: string;
  horaIngreso: string;
  horaSalida?: string;
  salidaRegistrada: boolean;
  duracionMinutos?: number;
  nombreCliente: string;
  ciCliente: number;
}

export interface Casillero {
  id: string;
  numero: number;
  tipo: 'FIJO' | 'TEMPORAL' | 'ESTANTE_RECEPCION';
  estado: 'DISPONIBLE' | 'OCUPADO' | 'MANTENIMIENTO';
  ubicacion?: string;
}

export interface PrestamoCasillero {
  id: string;
  casilleroId?: string;
  ingresoId: string;
  numeroCasillero: number;
  numeroTicket?: string;
  numeroLlave?: string;
  ciDepositado?: number;
  fechaPrestamo: string;
  fechaDevolucion?: string;
  devuelto: boolean;
  estaActivo: boolean;
}

export interface AuthResponse {
  access_token: string;
  expires_in: number;
  token_type: string;
}

export interface ApiError {
  message: string;
  status: number;
}
