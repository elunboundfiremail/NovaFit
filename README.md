# NovaFit

Sistema integral de gestión para gimnasios con arquitectura moderna basada en microservicios.

## Descripción

NovaFit es una aplicación web completa para la administración de gimnasios que incluye gestión de clientes, suscripciones, casilleros y registro de ingresos. El sistema utiliza autenticación basada en tokens JWT mediante Keycloak y está diseñado para ejecutarse en contenedores Docker.

## Tecnologías

### Backend
- .NET 9.0
- Entity Framework Core 9.0
- PostgreSQL 16
- Arquitectura limpia (Clean Architecture)

### Frontend
- Angular 19
- TypeScript
- Componentes standalone
- Diseño responsivo con tema oscuro

### Infraestructura
- Docker / Podman
- Nginx
- Keycloak 26.0

## Requisitos Previos

### Windows
- Docker Desktop 4.0+ o Podman 4.0+
- .NET 9 SDK (opcional, para desarrollo)
- Node.js 20+ (opcional, para desarrollo)

### Linux
- Docker Engine 24.0+ o Podman 4.0+
- Docker Compose v2 (incluido en Docker Engine)

## Instalación y Configuración

### 1. Clonar el Repositorio

```bash
git clone https://github.com/elunboundfiremail/NovaFit.git
cd NovaFit
```

### 2. Configurar Variables de Entorno

Navegar a la carpeta de configuración:

```bash
cd devops
```

Crear archivo `.env` basado en `.env.example`:

**Windows (PowerShell):**
```powershell
Copy-Item .env.example .env
```

**Linux:**
```bash
cp .env.example .env
```

Editar el archivo `.env` con las credenciales deseadas:

```
POSTGRES_DB=novafit_db
POSTGRES_USER=novafit_user
POSTGRES_PASSWORD=SuPasswordSeguro123

KEYCLOAK_ADMIN=admin_novafit
KEYCLOAK_ADMIN_PASSWORD=SuPasswordSeguro123
```

### 3. Iniciar los Servicios

Desde la carpeta `devops`, ejecutar:

**Con Docker:**
```bash
docker compose up -d
```

**Con Podman:**
```bash
podman-compose up -d
```

### 4. Verificar Estado de los Servicios

```bash
docker compose ps
```

o

```bash
podman-compose ps
```

Los servicios deberían mostrar estado "healthy" o "running".

### 5. Aplicar Migraciones de Base de Datos

**Opción A: Automático (ya incluido en init-db.sql)**

Las tablas se crean automáticamente al iniciar PostgreSQL.

**Opción B: Manual desde el contenedor backend**

```bash
docker exec -it novafit_backend dotnet ef database update
```

### 6. Configurar Keycloak

Acceder a Keycloak:
- URL: http://localhost:8081
- Usuario: `admin_novafit`
- Contraseña: la definida en `.env`

El realm `novafit-realm` y el cliente `novafit-api` se importan automáticamente desde la carpeta `devops/keycloak`.

## Acceso a la Aplicación

Una vez iniciados todos los servicios:

- **Frontend:** http://localhost:4200
- **Backend API:** http://localhost:8080/swagger
- **Keycloak:** http://localhost:8081
- **PostgreSQL:** localhost:5432

## Estructura del Proyecto

```
NovaFit/
├── backend/
│   ├── src/
│   │   ├── NovaFit.Domain/         # Entidades y lógica de dominio
│   │   ├── NovaFit.Application/    # Casos de uso y DTOs
│   │   ├── NovaFit.Infrastructure/ # Persistencia y servicios externos
│   │   └── NovaFit.WebAPI/         # Controladores y configuración API
│   ├── Dockerfile
│   └── NovaFit.sln
├── frontend/
│   ├── src/
│   │   ├── app/
│   │   │   ├── core/              # Servicios y guardias
│   │   │   └── features/          # Componentes de funcionalidades
│   │   └── environments/
│   ├── nginx/
│   │   └── nginx.conf
│   ├── Dockerfile
│   └── package.json
├── devops/
│   ├── compose.yaml               # Orquestación de contenedores
│   ├── keycloak/                  # Configuración de realm
│   ├── init-db.sql                # Script de inicialización DB
│   └── .env.example
└── postman/                       # Colección de pruebas API
```

## Endpoints Principales de la API

### Clientes
- `GET /api/clientes` - Listar todos los clientes
- `GET /api/clientes/{id}` - Obtener cliente por ID
- `POST /api/clientes` - Crear nuevo cliente
- `PUT /api/clientes/{id}` - Actualizar cliente
- `DELETE /api/clientes/{id}` - Eliminar cliente
- `GET /api/clientes/ci/{ci}` - Buscar por cédula de identidad

### Suscripciones
- `GET /api/suscripciones` - Listar suscripciones
- `GET /api/suscripciones/{id}` - Obtener suscripción
- `POST /api/suscripciones` - Crear suscripción
- `PUT /api/suscripciones/{id}` - Actualizar suscripción
- `DELETE /api/suscripciones/{id}` - Eliminar suscripción

### Ingresos
- `GET /api/ingresos` - Listar registros de ingreso
- `POST /api/ingresos` - Registrar entrada
- `PUT /api/ingresos/{id}/salida` - Registrar salida

### Casilleros
- `GET /api/casilleros` - Listar casilleros
- `GET /api/casilleros/disponibles` - Casilleros disponibles
- `POST /api/casilleros` - Crear casillero
- `PUT /api/casilleros/{id}` - Actualizar casillero
- `DELETE /api/casilleros/{id}` - Eliminar casillero

## Detener los Servicios

```bash
docker compose down
```

o

```bash
podman-compose down
```

Para eliminar también los volúmenes de datos:

```bash
docker compose down -v
```

## Desarrollo Local sin Docker

### Backend

```bash
cd backend/src/NovaFit.WebAPI
dotnet restore
dotnet run
```

Configurar `appsettings.Development.json` con las cadenas de conexión apropiadas.

### Frontend

```bash
cd frontend
npm install --legacy-peer-deps
npm start
```

La aplicación estará disponible en http://localhost:4200

## Pruebas

### Importar Colección Postman

1. Abrir Postman
2. Importar archivo `postman/NovaFit.postman_collection.json`
3. Configurar variables de entorno (URL base, tokens)
4. Ejecutar peticiones de prueba

### Ejecutar Tests Unitarios (Backend)

```bash
cd backend
dotnet test
```

## Solución de Problemas

### Los contenedores no inician correctamente

Verificar logs:
```bash
docker compose logs -f
```

### Error de conexión a base de datos

Verificar que PostgreSQL esté saludable:
```bash
docker compose ps postgres
```

Revisar credenciales en archivo `.env`

### Keycloak no responde

Esperar 60-90 segundos después del inicio para que Keycloak complete su arranque. Verificar logs:
```bash
docker compose logs keycloak
```

### Error en migraciones de Entity Framework

Recrear base de datos:
```bash
docker compose down -v
docker compose up -d
```

## Licencia

MIT License - ver archivo `LICENSE` para más detalles.

## Contribuciones

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crear una rama para la funcionalidad (`git checkout -b feature/nueva-funcionalidad`)
3. Commit los cambios (`git commit -m 'Agregar nueva funcionalidad'`)
4. Push a la rama (`git push origin feature/nueva-funcionalidad`)
5. Abrir un Pull Request

## Contacto

Para consultas o soporte, abrir un issue en el repositorio de GitHub.