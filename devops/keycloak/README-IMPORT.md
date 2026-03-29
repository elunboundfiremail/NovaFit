# Importar Configuracion Keycloak - Opcion Rapida

Este archivo JSON contiene la configuracion completa del realm NovaFit.

## Opcion 1: Importar via Admin Console (RECOMENDADO)

1. Acceder a Keycloak: http://localhost:8080
2. Login con admin/Upds123
3. En el menu superior izquierdo, hover sobre "master"
4. Click en "Create Realm"
5. Click en "Browse" junto a "Resource file"
6. Seleccionar archivo: `novafit-realm-export.json`
7. Click en "Create"

**LISTO** - Realm, cliente, roles y usuarios creados automaticamente.

## Opcion 2: Importar via CLI

```bash
# Copiar archivo al contenedor
docker cp novafit-realm-export.json novafit_keycloak:/tmp/

# Importar realm
docker exec novafit_keycloak /opt/keycloak/bin/kc.sh import --file /tmp/novafit-realm-export.json
```

## Verificar Importacion

1. En Keycloak Admin Console
2. Cambiar al realm "novafit-realm" (menu superior izquierdo)
3. Verificar:
   - **Clients**: Debe aparecer "novafit-api"
   - **Realm roles**: admin, empleado, cliente
   - **Users**: admin_novafit, empleado_novafit

## Obtener Client Secret

Despues de importar:
1. Ir a Clients → novafit-api
2. Pestaña "Credentials"
3. Copiar "Client Secret" para usar en appsettings.json

## Credenciales Creadas

### Admin NovaFit
- Username: admin_novafit
- Password: Upds123
- Rol: admin

### Empleado NovaFit
- Username: empleado_novafit
- Password: Upds123
- Rol: empleado

## Notas

⚠️ **DESARROLLO SOLAMENTE**

Esta configuracion es SOLO para desarrollo:
- SSL deshabilitado (sslRequired: "none")
- Web origins abiertos ("*")
- Passwords simples (Upds123)

En produccion:
- Cambiar todas las passwords
- Habilitar SSL (sslRequired: "external")
- Configurar web origins especificos
- Habilitar proteccion brute force
- Configurar timeouts adecuados

## Si la Importacion Falla

Usar metodo manual siguiendo: `GUIA-CONFIGURACION-KEYCLOAK.md`
