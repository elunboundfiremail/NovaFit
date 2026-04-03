# NovaFit - Guía Rápida de Uso

## 🚀 Inicio Rápido (3 comandos)

```powershell
# 1. Iniciar Docker Desktop (manual)

# 2. Ejecutar script de inicio
.\scripts\start.ps1

# 3. Listo! API corriendo en http://localhost:5000
```

## 🔐 Credenciales

### Keycloak (http://localhost:8080)
- Usuario: `admin`
- Password: `Upds123`

### PostgreSQL (localhost:5432)
- Database: `novafit_db`
- Usuario: `novafit_user`
- Password: `Upds123`

### Usuario API (para obtener JWT)
- Username: `admin_novafit`
- Password: `Upds123`
- Rol: `admin`

## 📡 Obtener Token JWT

```bash
POST http://localhost:8080/realms/novafit-realm/protocol/openid-connect/token
Content-Type: application/x-www-form-urlencoded

grant_type=password&client_id=novafit-api&client_secret=<ver_en_keycloak>&username=admin_novafit&password=Upds123
```

## 🔍 Endpoints Principales

### Clientes
- `GET /api/clientes` - Listar todos
- `GET /api/clientes/{id}` - Por ID
- `GET /api/clientes/ci/{ci}` - Por CI (para búsqueda en recepción)
- `POST /api/clientes` - Crear nuevo
- `PUT /api/clientes/{id}` - Actualizar
- `DELETE /api/clientes/{id}` - Eliminar

### Membresías
- `GET /api/membresias` - Listar todas
- `GET /api/membresias/cliente/{clienteId}/activa` - Membresía activa del cliente
- `POST /api/membresias` - Crear nueva
- `PUT /api/membresias/{id}/cancelar` - Cancelar

### Ingresos
- `GET /api/ingresos` - Listar todos
- `GET /api/ingresos/cliente/{clienteId}` - Historial de cliente
- `POST /api/ingresos/registrar` - Registrar ingreso (valida membresía)
- `GET /api/ingresos/rechazados` - Ver ingresos rechazados

### Casilleros
- `GET /api/casilleros/disponibles` - Casilleros libres
- `GET /api/casilleros/prestamos/activos` - Préstamos activos
- `POST /api/casilleros/prestar` - Prestar casillero
- `POST /api/casilleros/devolver/{prestamoId}` - Devolver

## 📋 Flujo Típico: Ingreso de Cliente

```
1. Cliente llega → Dicta CI: "7654321"
   GET /api/clientes/ci/7654321

2. Verificar membresía activa
   GET /api/membresias/cliente/{id}/activa

3. Registrar ingreso
   POST /api/ingresos/registrar
   {
     "clienteId": "guid-del-cliente",
     "membresiaId": "guid-de-membresia"
   }

4. Asignar casillero
   POST /api/casilleros/prestar
   {
     "casilleroId": "guid-casillero",
     "clienteId": "guid-cliente",
     "documentoRetenido": "CI 7654321"
   }
```

## 🛠️ Scripts Disponibles

### Iniciar todo
```powershell
.\scripts\start.ps1
```

### Detener servicios
```powershell
.\scripts\stop.ps1
```

### Reset completo (limpia BD)
```powershell
.\scripts\reset.ps1
```

## 🔧 Comandos Útiles

### Ver logs de servicios
```bash
cd devops
docker-compose logs -f
```

### Acceder a PostgreSQL
```bash
docker exec -it novafit_postgres psql -U novafit_user -d novafit_db
```

### Recrear migración
```bash
cd backend
Remove-Item src\NovaFit.Infrastructure\Migrations\*.cs
dotnet ef migrations add InitialCreate --project src\NovaFit.Infrastructure --startup-project src\NovaFit.WebAPI
```

### Aplicar migración
```bash
cd backend
dotnet ef database update --project src\NovaFit.Infrastructure --startup-project src\NovaFit.WebAPI
```

## 📊 Datos de Prueba (Seed)

Al iniciar, el sistema carga automáticamente:
- 10 clientes
- 10 membresías (8 activas, 2 vencidas)
- 15 ingresos históricos
- 20 casilleros
- 3 promociones

### Clientes de prueba:
- CI: 7654321 - Juan Carlos Perez Mamani
- CI: 8765432 - Maria Elena Rodriguez Quispe
- CI: 9876543 - Carlos Alberto Gutierrez Condori
- CI: 6543210 - Ana Patricia Flores Mamani
- CI: 5432109 - Luis Fernando Choque Apaza

## ❗ Troubleshooting

### Error: Docker no responde
```powershell
# Reiniciar Docker Desktop
# Esperar 30 segundos
docker ps
```

### Error: Puerto 5432 ocupado
```powershell
# Ver qué está usando el puerto
netstat -ano | findstr :5432

# Detener otro PostgreSQL si existe
Stop-Service postgresql-x64-XX
```

### Error: Keycloak no inicia
```powershell
# Ver logs
docker logs novafit_keycloak

# Esperar más tiempo (puede tardar 60s primera vez)
```

## 🎯 Próximos Pasos

1. ✅ Backend funcionando
2. ⏳ Frontend Angular (próxima fase)
3. ⏳ Nginx para servir frontend
4. ⏳ Despliegue en producción
