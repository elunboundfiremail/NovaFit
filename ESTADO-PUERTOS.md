# ⚠️ Estado de Puertos y Duplicados

## 🔌 PUERTOS NECESARIOS

| Puerto | Servicio | Estado | PID | Proceso |
|--------|----------|--------|-----|---------|
| 5432 | PostgreSQL | ⚠️ OCUPADO | 5336 | postgres |
| 8080 | Keycloak | ⚠️ OCUPADO | 3984 | httpd (Apache) |
| 5000 | API .NET | ✅ LIBRE | - | - |

## 🛠️ CÓMO LIBERAR PUERTOS

### Opción 1: Script Automático (Recomendado)
```powershell
# Ejecutar como Administrador (clic derecho → Ejecutar como administrador)
.\scripts\liberar-puertos.ps1
```

### Opción 2: Manual
```powershell
# Abrir PowerShell como Administrador
Stop-Process -Id 5336 -Force  # PostgreSQL
Stop-Process -Id 3984 -Force  # Apache/httpd en puerto 8080
```

### Opción 3: Desde Servicios de Windows
```
1. Win + R → "services.msc"
2. Buscar "PostgreSQL" → Detener
3. Buscar "Apache" o "httpd" → Detener
```

## 📂 ARCHIVOS DUPLICADOS

### ✅ Sin Duplicados Problemáticos
Los archivos "duplicados" encontrados son **NORMALES** del proceso de compilación:

1. **appsettings.json** en `bin/Debug/` → Copia automática de .NET para runtime
2. **project.assets.json** en `obj/` → Archivos de build de NuGet
3. **README.md** múltiples → Diferentes (raíz vs backend)

**No hay duplicados que eliminar.**

## 🎯 PRÓXIMOS PASOS

1. **Liberar puertos**:
   ```powershell
   # Como administrador
   .\scripts\liberar-puertos.ps1
   ```

2. **Iniciar Docker Desktop** (manual)

3. **Ejecutar sistema**:
   ```powershell
   .\scripts\start.ps1
   ```

## 🔍 VERIFICACIÓN

Para verificar que los puertos están libres:
```powershell
netstat -ano | findstr ":5432"  # Debe estar vacío
netstat -ano | findstr ":8080"  # Debe estar vacío
netstat -ano | findstr ":5000"  # Debe estar vacío
```

## ⚡ RESUMEN RÁPIDO

```powershell
# 1. Abrir PowerShell como Administrador
# 2. Ir a la carpeta
cd F:\datafiles\hckrmn\Carrera\Carrera\prog_web_2\novafit-new

# 3. Liberar puertos
.\scripts\liberar-puertos.ps1

# 4. Iniciar Docker Desktop (manual)

# 5. Ejecutar todo
.\scripts\start.ps1
```

---

**Nota**: Los puertos 5432 y 8080 están ocupados por servicios locales de PostgreSQL y Apache. 
Necesitas detenerlos antes de levantar los contenedores Docker que usan esos mismos puertos.
