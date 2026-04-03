# NovaFit

Sistema de gestion para gimnasio desarrollado como proyecto academico de Programacion Web II.

## Fase Actual

**1ra fase: 40% Backend**

Estado actual:
- API REST funcional con ASP.NET Core 9
- PostgreSQL para persistencia
- Keycloak para autenticacion JWT
- Docker Compose para entorno local
- Coleccion Postman para pruebas manuales

Modulos implementados:
- Clientes
- Membresias
- Ingresos
- Casilleros
- Promociones festivas

## Arquitectura

El proyecto sigue una estructura tipo Clean Architecture:

- `NovaFit.Domain`: entidades del dominio
- `NovaFit.Application`: DTOs, interfaces y servicios
- `NovaFit.Infrastructure`: DbContext, configuraciones EF, repositorios, migraciones y seed
- `NovaFit.WebAPI`: controllers, configuracion HTTP, autenticacion y DI

## Tecnologias

- Backend: .NET 9 / ASP.NET Core Web API
- Base de datos: PostgreSQL 16
- Autenticacion: Keycloak 26
- ORM: Entity Framework Core
- Contenedores: Docker Compose
- Pruebas: Postman

## Estructura del Proyecto

```text
NovaFit/
├── backend/
│   ├── NovaFit.sln
│   ├── database/
│   │   ├── migrations/
│   │   └── schema/
│   │       └── dbml/
│   └── src/
│       ├── NovaFit.Domain/
│       │   └── Entities/
│       ├── NovaFit.Application/
│       │   ├── DTOs/
│       │   ├── Interfaces/
│       │   └── Services/
│       ├── NovaFit.Infrastructure/
│       │   ├── Data/
│       │   │   └── Configurations/
│       │   ├── Migrations/
│       │   └── Repositories/
│       └── NovaFit.WebAPI/
│           ├── Controllers/
│           └── Properties/
├── devops/
│   ├── docker-compose.yml
│   ├── init-db.sql
│   └── keycloak/
│       ├── novafit-realm-export.json
│       └── GUIA-CONFIGURACION-KEYCLOAK.md
├── postman/
│   └── NovaFit-API-Collection.postman_collection.json
└── GUIA-FUNCIONAL-NOVAFIT.md
```

## Funcionalidades Backend

### Clientes
- Listar clientes
- Obtener cliente por ID
- Obtener cliente por CI
- Crear cliente
- Actualizar cliente
- Eliminar cliente

### Membresias
- Listar membresias
- Obtener membresia por ID
- Obtener membresia activa por cliente
- Crear membresia mensual o anual
- Cancelar membresia

### Ingresos
- Listar ingresos
- Obtener ingreso por ID
- Ver historial por cliente
- Registrar ingreso validando membresia vigente
- Ver ingresos rechazados

### Casilleros
- Listar casilleros
- Obtener casillero por ID
- Listar casilleros disponibles
- Prestar casillero
- Devolver casillero
- Ver prestamos activos
- Ver historial por casillero

## Requisitos Previos

- .NET 9 SDK
- Docker Desktop
- Postman
- Puertos libres:
  - `5432`
  - `8080`
  - `5000`

## Como Levantar el Proyecto

### Opción A: Con Script de Inicio (Recomendado)

```powershell
.\scripts\start.ps1
```

Este script automáticamente:
- Verifica Docker
- Levanta servicios (PostgreSQL + Keycloak)
- Aplica migraciones
- Carga seed data
- Inicia la API

### Opción B: Manual

Desde `devops`:

```powershell
docker-compose up -d
docker-compose ps
```

Servicios esperados:
- `novafit_postgres`
- `novafit_keycloak`
- `novafit_keycloak_db`

### 2. Configurar Keycloak

Abrir:

```text
http://localhost:8080
```

Credenciales:
- usuario: `admin`
- password: `Upds123`

Importar el realm desde:

```text
devops/keycloak/novafit-realm-export.json
```

Verificar:
- Realm `novafit-realm`
- Client `novafit-api`
- Usuarios `admin_novafit` y `empleado_novafit`

### 3. Obtener JWT

**Opción rápida con script**:
```powershell
.\scripts\get-token.ps1
```

**Opción manual** - Endpoint:

```text
POST http://localhost:8080/realms/novafit-realm/protocol/openid-connect/token
```

Body `x-www-form-urlencoded`:
- `grant_type=password`
- `client_id=novafit-api`
- `username=admin_novafit`
- `password=Upds123`

### 4. Aplicar migraciones

Desde `backend`:

```powershell
dotnet ef database update --project src/NovaFit.Infrastructure --startup-project src/NovaFit.WebAPI
```

### 5. Ejecutar la API

```powershell
dotnet run --project src/NovaFit.WebAPI --urls "http://localhost:5000"
```

### 6. Carga automatica de datos

La API ejecuta seed de datos al iniciar si la tabla de clientes esta vacia.

Datos de prueba cargados:
- clientes
- membresias
- ingresos
- casilleros
- prestamos
- promociones

## Coleccion Postman

Archivo:

```text
postman/NovaFit-API-Collection.postman_collection.json
```

Variables recomendadas:
- `baseUrl = http://localhost:5000`
- `jwt_token = <access_token>`
- `clienteId`
- `clienteCi`
- `membresiaId`
- `ingresoId`
- `casilleroId`
- `prestamoId`

## Flujo de Pruebas

Orden recomendado:

1. `GET /api/clientes`
2. `GET /api/membresias`
3. `GET /api/ingresos/rechazados`
4. `GET /api/casilleros/disponibles`
5. `POST /api/clientes`
6. `PUT /api/clientes/{id}`
7. `POST /api/membresias`
8. `POST /api/ingresos/registrar`
9. `POST /api/casilleros/prestar`
10. `POST /api/casilleros/devolver/{prestamoId}`

## Evidencia Funcional

Estado validado manualmente:
- autenticacion JWT funcionando
- API respondiendo en `http://localhost:5000`
- migraciones aplicadas correctamente
- seed cargando datos de prueba
- pruebas `GET`, `POST` y `PUT` ejecutadas en Postman

Sugerencia para el reporte final:
- agregar una captura del `GET /api/clientes` funcionando
- agregar una captura del token generado en Keycloak/Postman
- agregar una captura de Swagger o Postman con respuesta `200 OK`

## Hallazgos y Correcciones Relevantes

- Se corrigio la configuracion de Keycloak en `docker-compose.yml`
- Se corrigieron relaciones EF Core para evitar claves foraneas sombra
- Se regenero la migracion inicial del proyecto
- Se conecto el seed automatico al arranque de la API

Pendiente conocido:
- el `DELETE /api/clientes/{id}` todavia requiere ajuste a soft delete porque actualmente puede fallar por restricciones de llave foranea

## Contribuidores

- `elunboundfiremail` - infraestructura base
- `leo-aig` - funcionalidades backend

## Proyecto Academico

Universidad Privada Domingo Savio  
Programacion Web II - 2026

