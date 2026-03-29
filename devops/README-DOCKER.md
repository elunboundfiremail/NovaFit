# Docker Setup - NovaFit

Documentacion para levantar y gestionar servicios Docker del proyecto NovaFit.

## Servicios Incluidos

- **PostgreSQL 16**: Base de datos principal (puerto 5432)
- **Keycloak 26.0**: Servidor de autenticacion JWT (puerto 8080)
- **PostgreSQL 16**: Base de datos para Keycloak (interno)

## Requisitos Previos

- Docker Desktop instalado y corriendo
- Puertos 5432 y 8080 libres
- 4 GB RAM minimo disponible

## Inicio Rapido

### 1. Levantar servicios

```bash
cd devops
docker-compose up -d
```

### 2. Verificar estado

```bash
docker-compose ps
```

Debe mostrar 3 contenedores corriendo:
- novafit_postgres (healthy)
- novafit_keycloak (healthy)
- novafit_keycloak_db (healthy)

### 3. Ver logs

```bash
# Todos los servicios
docker-compose logs -f

# Solo PostgreSQL
docker-compose logs -f postgres

# Solo Keycloak
docker-compose logs -f keycloak
```

## Acceso a Servicios

### PostgreSQL Principal

```bash
# Conectarse via Docker
docker exec -it novafit_postgres psql -U novafit_user -d novafit_db

# Conectarse via psql local
psql -h localhost -p 5432 -U novafit_user -d novafit_db
```

**Credenciales:**
- Host: localhost
- Puerto: 5432
- Database: novafit_db
- Usuario: novafit_user
- Password: Upds123

### Keycloak Admin Console

**URL:** http://localhost:8080

**Credenciales:**
- Usuario: admin
- Password: Upds123

**Primer acceso:**
1. Acceder a http://localhost:8080
2. Login con admin/Upds123
3. Crear realm "novafit-realm" (ver COMMIT #6)

## Comandos Utiles

### Gestión de Contenedores

```bash
# Iniciar servicios
docker-compose up -d

# Detener servicios (mantiene datos)
docker-compose stop

# Reiniciar servicios
docker-compose restart

# Detener y eliminar contenedores (mantiene volumenes)
docker-compose down

# Detener y eliminar TODO (incluye volumenes - CUIDADO)
docker-compose down -v
```

### Monitoreo

```bash
# Ver estado de salud
docker-compose ps

# Uso de recursos
docker stats

# Inspeccionar red
docker network inspect novafit_network
```

### Mantenimiento

```bash
# Respaldar base de datos
docker exec novafit_postgres pg_dump -U novafit_user novafit_db > backup.sql

# Restaurar base de datos
docker exec -i novafit_postgres psql -U novafit_user -d novafit_db < backup.sql

# Limpiar logs
docker-compose logs --tail=0 -f > /dev/null
```

## Volumenes Persistentes

Los datos se almacenan en volumenes Docker:

- `postgres_data`: Datos de PostgreSQL principal
- `keycloak_postgres_data`: Datos de Keycloak

**Ubicacion (Windows):**
```
C:\ProgramData\Docker\volumes\
```

## Healthchecks

Ambos servicios tienen healthchecks configurados:

**PostgreSQL:** Verifica conexion cada 10 segundos
**Keycloak:** Verifica endpoint /health/ready cada 30 segundos

Ver estado:
```bash
docker inspect novafit_postgres | findstr Health
docker inspect novafit_keycloak | findstr Health
```

## Troubleshooting

### Puerto 5432 ocupado

```bash
# Ver que proceso usa el puerto
netstat -ano | findstr :5432

# Detener proceso (reemplazar PID)
Stop-Process -Id <PID> -Force
```

### Puerto 8080 ocupado

```bash
netstat -ano | findstr :8080
Stop-Process -Id <PID> -Force
```

### Keycloak no inicia

```bash
# Ver logs detallados
docker-compose logs keycloak

# Reiniciar con rebuild
docker-compose up -d --force-recreate keycloak
```

### PostgreSQL no acepta conexiones

```bash
# Verificar healthcheck
docker inspect novafit_postgres | findstr Health

# Reiniciar
docker-compose restart postgres
```

### Limpiar todo y empezar de cero

```bash
# CUIDADO: Elimina todos los datos
docker-compose down -v
docker-compose up -d
```

## Configuracion de Red

Red bridge: `novafit_network`

Contenedores pueden comunicarse entre si usando nombres de servicio:
- `postgres` (resuelve a IP interna)
- `keycloak` (resuelve a IP interna)

## Variables de Entorno

Ver archivo `.env.example` para configuracion personalizada.

Copiar y modificar:
```bash
cp .env.example .env
# Editar .env con tus valores
```

## Notas de Seguridad

⚠️ **DESARROLLO SOLO**

Las passwords "Upds123" son para desarrollo academico.

**NO usar en produccion:**
- Cambiar todas las passwords
- Habilitar SSL/TLS
- Configurar firewall
- Restringir acceso a puertos

## Siguientes Pasos

Despues de levantar Docker:

1. Aplicar migrations: `dotnet ef database update`
2. Configurar Keycloak (COMMIT #6)
3. Iniciar API: `dotnet run`
4. Probar con Postman

## Recursos

- [Docker Compose Docs](https://docs.docker.com/compose/)
- [PostgreSQL Docker](https://hub.docker.com/_/postgres)
- [Keycloak Docker](https://www.keycloak.org/server/containers)
