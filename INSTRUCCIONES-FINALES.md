# 🎯 INSTRUCCIONES FINALES - NovaFit Backend

## ✅ COMPLETADO (82.4%)

### Lo que ya está listo:
- ✅ Keycloak simplificado (1 rol admin, 1 usuario)
- ✅ Migraciones regeneradas limpias
- ✅ Código backend sin autenticación de cliente
- ✅ Scripts de automatización creados
- ✅ Documentación completa

### Tareas completadas: 14/17

## ⚠️ BLOQUEADORES ACTUALES

### 1. Puertos Ocupados
- **Puerto 5432**: PostgreSQL local (PID 5336)
- **Puerto 8080**: Apache/httpd (PID 3984)
- **Puerto 5000**: ✅ Libre

### 2. Docker Desktop
- ⚠️ No está corriendo

## 🚀 PASOS PARA CONTINUAR (EN ORDEN)

### Paso 1: Liberar Puertos
```powershell
# IMPORTANTE: Abrir PowerShell como ADMINISTRADOR
# Clic derecho → Ejecutar como administrador

cd F:\datafiles\hckrmn\Carrera\Carrera\prog_web_2\novafit-new
.\scripts\liberar-puertos.ps1
```

### Paso 2: Iniciar Docker Desktop
```
1. Abrir Docker Desktop
2. Esperar a que esté completamente iniciado (ícono verde)
3. Verificar: docker ps
```

### Paso 3: Ejecutar Todo Automáticamente
```powershell
# Esto hará todo automáticamente:
# - Levantar PostgreSQL + Keycloak en Docker
# - Aplicar migraciones
# - Cargar seed data (10 clientes)
# - Iniciar API

.\scripts\start.ps1
```

### Paso 4: Configurar Keycloak (Primera Vez)
```
1. Abrir: http://localhost:8080
2. Login: admin / Upds123
3. Crear Realm → Import
4. Seleccionar: devops/keycloak/novafit-realm-export.json
5. Verificar que usuario "admin_novafit" existe
```

### Paso 5: Obtener Token JWT
```powershell
# En otra terminal PowerShell
.\scripts\get-token.ps1

# El token se guarda en: $env:NOVAFIT_JWT
```

### Paso 6: Probar API
```bash
# Usar Postman o curl con el token

GET http://localhost:5000/api/clientes
Authorization: Bearer <token>

GET http://localhost:5000/api/clientes/ci/7654321
GET http://localhost:5000/api/membresias
GET http://localhost:5000/api/casilleros/disponibles
```

## 📋 SCRIPTS DISPONIBLES

| Script | Descripción | Requiere Admin |
|--------|-------------|----------------|
| `liberar-puertos.ps1` | Libera puertos 5432, 8080, 5000 | ✅ Sí |
| `start.ps1` | Inicia todo el sistema | ❌ No |
| `stop.ps1` | Detiene servicios Docker | ❌ No |
| `reset.ps1` | Reset completo (limpia BD) | ❌ No |
| `get-token.ps1` | Obtiene JWT de Keycloak | ❌ No |

## 🔍 TROUBLESHOOTING

### Error: "Puerto ocupado"
```powershell
# Ejecutar como administrador
.\scripts\liberar-puertos.ps1
```

### Error: "Docker no responde"
```
1. Reiniciar Docker Desktop
2. Esperar 30-60 segundos
3. Verificar: docker ps
```

### Error: "Keycloak no inicia"
```powershell
# Ver logs
cd devops
docker logs novafit_keycloak -f

# Esperar más tiempo (puede tardar 60s primera vez)
```

### Error: "No puedo obtener token"
```
1. Verificar Keycloak en http://localhost:8080
2. Verificar que realm "novafit-realm" existe
3. Verificar que usuario "admin_novafit" existe
```

## 📊 CREDENCIALES

### Keycloak Admin Console
- URL: http://localhost:8080
- Usuario: `admin`
- Password: `Upds123`

### Usuario API (para JWT)
- Username: `admin_novafit`
- Password: `Upds123`
- Rol: `admin`

### PostgreSQL
- Host: `localhost`
- Port: `5432`
- Database: `novafit_db`
- Usuario: `novafit_user`
- Password: `Upds123`

## 🎓 DATOS DE PRUEBA

El seed carga automáticamente:
- 10 clientes con CIs bolivianos
- 10 membresías (8 activas, 2 vencidas)
- 15 ingresos históricos
- 20 casilleros
- 3 promociones festivas

### Clientes para probar:
- CI: `7654321` - Juan Carlos Perez Mamani
- CI: `8765432` - Maria Elena Rodriguez Quispe
- CI: `9876543` - Carlos Alberto Gutierrez Condori

## 🎉 PRÓXIMOS PASOS (Después de Backend)

1. ✅ Backend funcionando
2. ⏳ Frontend Angular (siguiente fase)
3. ⏳ Nginx para servir frontend
4. ⏳ Integración completa
5. ⏳ Despliegue

---

**Resumen**: 
1. Liberar puertos (como admin)
2. Iniciar Docker Desktop
3. Ejecutar `.\scripts\start.ps1`
4. Importar realm en Keycloak
5. ¡Listo para usar! 🚀
