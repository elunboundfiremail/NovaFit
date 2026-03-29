# Modelo de Entidades (borrador de implementación)

## Cliente

- Id (UUID)
- CI (único)
- Nombres
- ApellidoPaterno
- ApellidoMaterno (nullable)
- FechaRegistro
- Activo

## Membresia

- Id (UUID)
- ClienteId
- TipoPlan (Mensual, Anual, Sesion)
- Costo
- FechaInicio
- FechaFin
- Estado

## Ingreso

- Id (UUID)
- ClienteId
- FechaHoraIngreso
- MensajeAlerta (nullable)
- Permitido

## Casillero

- Id (UUID)
- Numero
- Disponible

## PrestamoCasillero

- Id (UUID)
- CasilleroId
- ClienteId
- DocumentoRetenido
- FechaHoraPrestamo
- FechaHoraDevolucion (nullable)

## PromocionFestiva

- Id (UUID)
- Nombre
- FechaInicio
- FechaFin
- PorcentajeDescuento
- Activo
