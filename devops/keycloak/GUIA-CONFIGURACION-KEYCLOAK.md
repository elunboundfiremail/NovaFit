# Guia de Configuracion Keycloak - NovaFit

Pasos detallados para configurar Keycloak con el realm y cliente para NovaFit.

## Requisitos Previos

- Keycloak corriendo en http://localhost:8080
- Credenciales admin: admin / Upds123
- Docker Compose levantado (COMMIT #5)

## Paso 1: Acceder a Keycloak Admin Console

1. Abrir navegador: **http://localhost:8080**
2. Click en **"Administration Console"**
3. Login:
   - Username: `admin`
   - Password: `Upds123`

## Paso 2: Crear Realm "novafit-realm"

1. En el menu superior izquierdo, hacer hover sobre **"master"**
2. Click en **"Create Realm"**
3. Completar:
   - **Realm name**: `novafit-realm`
   - **Enabled**: ON
4. Click en **"Create"**

## Paso 3: Crear Cliente "novafit-api"

1. En el menu lateral, ir a **"Clients"**
2. Click en **"Create client"**
3. Seccion **"General Settings"**:
   - **Client type**: `OpenID Connect`
   - **Client ID**: `novafit-api`
4. Click en **"Next"**
5. Seccion **"Capability config"**:
   - **Client authentication**: ON
   - **Authorization**: OFF
   - **Standard flow**: ON
   - **Direct access grants**: ON
   - **Implicit flow**: OFF
   - **Service accounts roles**: ON
6. Click en **"Next"**
7. Seccion **"Login settings"**:
   - **Root URL**: `http://localhost:5000`
   - **Home URL**: `http://localhost:5000`
   - **Valid redirect URIs**: `http://localhost:5000/*`
   - **Valid post logout redirect URIs**: `http://localhost:5000/*`
   - **Web origins**: `*`
8. Click en **"Save"**

## Paso 4: Obtener Client Secret

1. En la lista de clientes, click en **"novafit-api"**
2. Ir a la pestaña **"Credentials"**
3. Copiar el **"Client secret"** (guardar para uso posterior)
   - Ejemplo: `abc123def456...`

## Paso 5: Crear Roles

1. En el menu lateral, ir a **"Realm roles"**
2. Click en **"Create role"**
3. Crear los siguientes roles uno por uno:

### Rol: admin
- **Role name**: `admin`
- **Description**: `Administrador del sistema`
- Click **"Save"**

### Rol: empleado
- **Role name**: `empleado`
- **Description**: `Empleado del gimnasio`
- Click **"Save"**

### Rol: cliente
- **Role name**: `cliente`
- **Description**: `Cliente del gimnasio`
- Click **"Save"**

## Paso 6: Crear Usuario de Prueba (Administrador)

1. En el menu lateral, ir a **"Users"**
2. Click en **"Add user"**
3. Completar:
   - **Username**: `admin_novafit`
   - **Email**: `admin@novafit.com`
   - **First name**: `Admin`
   - **Last name**: `NovaFit`
   - **Email verified**: ON
   - **Enabled**: ON
4. Click en **"Create"**

### Asignar Password
1. Ir a la pestaña **"Credentials"**
2. Click en **"Set password"**
3. Completar:
   - **Password**: `Upds123`
   - **Password confirmation**: `Upds123`
   - **Temporary**: OFF
4. Click en **"Save"**
5. Confirmar en el dialogo

### Asignar Rol
1. Ir a la pestaña **"Role mapping"**
2. Click en **"Assign role"**
3. Filtrar por **"Filter by realm roles"**
4. Seleccionar: **admin**
5. Click en **"Assign"**

## Paso 7: Crear Usuario de Prueba (Empleado)

Repetir Paso 6 con estos datos:
- **Username**: `empleado_novafit`
- **Email**: `empleado@novafit.com`
- **First name**: `Empleado`
- **Last name**: `NovaFit`
- **Password**: `Upds123` (Temporary: OFF)
- **Rol asignado**: `empleado`

## Paso 8: Verificar Configuracion del Realm

1. Ir a **"Realm settings"**
2. En la pestaña **"General"**:
   - **Enabled**: ON
   - **Endpoints**: Click en **"OpenID Endpoint Configuration"**
   - Verificar que aparezca JSON con endpoints

3. Copiar el **"issuer"** del JSON:
   ```
   "issuer": "http://localhost:8080/realms/novafit-realm"
   ```

## Paso 9: Probar Obtencion de Token

### Via Postman

1. Crear nuevo request POST
2. URL: `http://localhost:8080/realms/novafit-realm/protocol/openid-connect/token`
3. En **Body** → **x-www-form-urlencoded**:
   - `grant_type`: `password`
   - `client_id`: `novafit-api`
   - `client_secret`: `[tu-client-secret-del-paso-4]`
   - `username`: `admin_novafit`
   - `password`: `Upds123`
4. Click **"Send"**
5. Debe retornar JSON con `access_token`, `refresh_token`, etc.

### Via cURL

```bash
curl -X POST http://localhost:8080/realms/novafit-realm/protocol/openid-connect/token ^
  -H "Content-Type: application/x-www-form-urlencoded" ^
  -d "grant_type=password" ^
  -d "client_id=novafit-api" ^
  -d "client_secret=[tu-client-secret]" ^
  -d "username=admin_novafit" ^
  -d "password=Upds123"
```

## Paso 10: Probar Token en la API

1. Copiar el `access_token` del response
2. En Postman, ir a cualquier endpoint de NovaFit API
3. En **Authorization**:
   - Type: `Bearer Token`
   - Token: `[pegar-access_token]`
4. Hacer request
5. Debe retornar respuesta exitosa (no 401 Unauthorized)

## Configuracion Completada

✅ Realm "novafit-realm" creado
✅ Cliente "novafit-api" configurado
✅ Roles (admin, empleado, cliente) creados
✅ Usuarios de prueba creados
✅ Tokens funcionando

## Resumen de Credenciales

### Keycloak Admin
- URL: http://localhost:8080
- Usuario: admin
- Password: Upds123

### Usuario Admin NovaFit
- Username: admin_novafit
- Password: Upds123
- Rol: admin

### Usuario Empleado NovaFit
- Username: empleado_novafit
- Password: Upds123
- Rol: empleado

### Cliente API
- Client ID: novafit-api
- Client Secret: [obtenido en Paso 4]

## Endpoints Importantes

**Token endpoint:**
```
POST http://localhost:8080/realms/novafit-realm/protocol/openid-connect/token
```

**OpenID Configuration:**
```
GET http://localhost:8080/realms/novafit-realm/.well-known/openid-configuration
```

**Logout:**
```
POST http://localhost:8080/realms/novafit-realm/protocol/openid-connect/logout
```

## Troubleshooting

### Error: "Realm not found"
→ Verificar que el realm se llame exactamente "novafit-realm"
→ Verificar en URL: http://localhost:8080/realms/novafit-realm

### Error: "Invalid client credentials"
→ Verificar Client ID: "novafit-api"
→ Copiar nuevamente el Client Secret de la pestaña Credentials

### Error: "Invalid user credentials"
→ Verificar username y password
→ Verificar que usuario este habilitado (Enabled: ON)
→ Verificar que password no sea temporal

### Token expira muy rapido
→ Ir a Realm settings → Tokens
→ Ajustar "Access Token Lifespan" (default: 5 minutos)
→ Recomendado para desarrollo: 30 minutos

## Notas de Seguridad

⚠️ **SOLO PARA DESARROLLO**

- RequireHttpsMetadata: false (permitir HTTP)
- Web origins: * (permitir todos los origenes)
- Password: Upds123 (password debil)

**En produccion cambiar:**
- Habilitar HTTPS (RequireHttpsMetadata: true)
- Configurar Web origins especificos
- Passwords robustas
- Habilitar 2FA
- Configurar session timeouts adecuados

## Referencias

- [Keycloak Documentation](https://www.keycloak.org/documentation)
- [OpenID Connect](https://openid.net/connect/)
- [JWT.io](https://jwt.io/) - Decodificar tokens
