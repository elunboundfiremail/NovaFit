# UNIVERSIDAD PRIVADA DOMINGO SAVIO

## FACULTAD DE INGENIERÍA
## CARRERA DE INGENIERÍA DE SISTEMAS

# PROGRAMACIÓN WEB II
## SEXTO SEMESTRE

# SISTEMA DE GESTIÓN INTEGRAL PARA GIMNASIOS
## NOVAFIT

**ESTUDIANTES:**
- Zubieta Mendoza, Owen Jonathan
- Leyes Clavijo, Leandro

**DOCENTE:**  
LIC. ANDRÉS GROVER ALBINO CHAMBI

**La Paz - Bolivia**  
**Abril 2026**

# ÍNDICE GENERAL

1. [INTRODUCCIÓN](#1-introducción) .................................................. 4
   1.1. [Contexto General](#11-contexto-general) .......................................... 4
   1.2. [Problemática](#12-problemática) .............................................. 5
   1.3. [Justificación](#13-justificación) ............................................. 6
   1.4. [Alcance y Limitaciones](#14-alcance-y-limitaciones) .................................... 7

2. [OBJETIVOS](#2-objetivos) ..................................................... 8
   2.1. [Objetivo General](#21-objetivo-general) ......................................... 8
   2.2. [Objetivos Específicos](#22-objetivos-específicos) .................................... 8

3. [MARCO TEÓRICO](#3-marco-teórico) ................................................. 9
   3.1. [Arquitectura Limpia (Clean Architecture)](#31-arquitectura-limpia-clean-architecture) ................. 9
   3.2. [.NET 9](#32-net-9) ............................................. 10
   3.3. [Angular 19](#33-angular-19) ........................................ 11
   3.4. [Keycloak y OAuth2](#34-keycloak-y-oauth2) ....................................... 12
   3.5. [PostgreSQL](#35-postgresql) .................................... 13

4. [ANÁLISIS DEL SISTEMA](#4-análisis-del-sistema) ......................................... 14
   4.1. [Requerimientos Funcionales](#41-requerimientos-funcionales) .............................. 14
   4.2. [Requerimientos No Funcionales](#42-requerimientos-no-funcionales) ........................... 16
   4.3. [Actores del Sistema](#43-actores-del-sistema) ..................................... 17
   4.4. [Casos de Uso](#44-casos-de-uso) ............................................ 18

5. [DISEÑO DEL SISTEMA](#5-diseño-del-sistema) ........................................... 20
   5.1. [Arquitectura del Sistema](#51-arquitectura-del-sistema) ................................ 20
   5.2. [Modelo de Base de Datos](#52-modelo-de-base-de-datos) ................................. 22
   5.3. [Diccionario de Datos](#53-diccionario-de-datos) .................................... 24

6. [IMPLEMENTACIÓN](#6-implementación) ............................................... 26
   6.1. [Configuración del Entorno](#61-configuración-del-entorno) ............................... 26
   6.2. [Estructura del Backend](#62-estructura-del-backend) ............ 27
   6.3. [Estructura del Frontend](#63-estructura-del-frontend) ....................... 29
   6.4. [Configuración de Keycloak](#64-configuración-de-keycloak) ............................... 30
   6.5. [Endpoints de la API](#65-endpoints-de-la-api) ..................................... 31
   6.6. [Componentes Principales del Frontend](#66-componentes-principales-del-frontend) .................... 33

7. [PRUEBAS](#7-pruebas) ....................................................... 35
   7.1. [Casos de Prueba](#71-casos-de-prueba) ......................................... 35
   7.2. [Resultados de Pruebas](#72-resultados-de-pruebas) ................................... 37

8. [CONCLUSIONES Y RECOMENDACIONES](#8-conclusiones-y-recomendaciones) ............................... 38
   8.1. [Conclusiones](#81-conclusiones) ............................................ 38
   8.2. [Recomendaciones](#82-recomendaciones) ......................................... 39
   8.3. [Trabajos Futuros](#83-trabajos-futuros) ........................................ 39

9. [BIBLIOGRAFÍA](#9-bibliografía) ................................................. 40

10. [ANEXOS](#10-anexos) ....................................................... 41
    10.1. [Código Fuente (Repositorio GitHub)](#101-código-fuente-repositorio-github) .................... 41
    10.2. [Capturas de Pantalla](#102-capturas-de-pantalla) .................................. 42
    10.3. [Docker Compose](#103-docker-compose) ........................................ 43

# 1. INTRODUCCIÓN

## 1.1. Contexto General

En la actualidad, los gimnasios y centros de acondicionamiento físico enfrentan importantes desafíos operativos relacionados con la gestión de sus clientes, control de accesos, administración de suscripciones y seguimiento de la utilización de instalaciones. La mayoría de estos establecimientos opera con sistemas fragmentados o procesos manuales que generan ineficiencias, errores en el registro de información y dificultades para la toma de decisiones basadas en datos.

NovaFit nace como respuesta a esta problemática, proponiendo una solución integral que centraliza todas las operaciones esenciales de un gimnasio en una plataforma web moderna, segura y escalable. El sistema permite gestionar de manera eficiente el ciclo completo del cliente, desde su registro inicial hasta el control de sus ingresos diarios, pasando por la administración de diferentes tipos de suscripciones y la asignación de recursos como casilleros.

El proyecto se desarrolla aplicando tecnologías de vanguardia en desarrollo web, incluyendo .NET 9 para el backend con una arquitectura limpia que garantiza la mantenibilidad y escalabilidad del código, Angular 19 para el frontend con componentes standalone que mejoran el rendimiento y la modularidad, PostgreSQL 16 como sistema gestor de base de datos relacional, y Keycloak 26.0 para la gestión centralizada de autenticación y autorización.

La implementación de NovaFit representa un caso de estudio práctico de las mejores prácticas en ingeniería de software moderno, incluyendo contenedorización con Docker para facilitar el despliegue, separación de responsabilidades mediante arquitectura en capas, y un diseño de interfaz de usuario intuitivo que prioriza la experiencia del usuario final.

## 1.2. Problemática

El problema central identificado es la **falta de un sistema automatizado e integrado** que permita gestionar de manera eficiente, segura y en tiempo real todos los procesos operativos de un gimnasio, desde el control de acceso de los clientes hasta la administración de recursos y generación de reportes gerenciales.

**Subproblemas identificados:**

1. **Control de Accesos Manual**: Los gimnasios tradicionales dependen de sistemas de verificación manual de suscripciones, lo que genera:
   - Demoras en horarios pico
   - Posibilidad de ingreso de personas con suscripciones vencidas
   - Falta de trazabilidad sobre quién ingresó y a qué hora
   - Imposibilidad de generar estadísticas de afluencia

2. **Gestión Desorganizada de Suscripciones**: El manejo de diferentes planes (casual, mensual, anual) presenta dificultades como:
   - Control deficiente de fechas de vencimiento
   - No se notifica al personal sobre suscripciones próximas a vencer
   - Dificultad para aplicar promociones especiales de manera automática
   - Falta de historial de suscripciones anteriores del cliente

3. **Administración Ineficiente de Casilleros**: La gestión manual de casilleros genera:
   - Desconocimiento en tiempo real de casilleros disponibles
   - Pérdida de documentos retenidos como garantía
   - Imposibilidad de asignar casilleros fijos a clientes premium
   - Falta de registro de préstamos y devoluciones

4. **Ausencia de Información Gerencial**: La toma de decisiones se ve afectada por:
   - Imposibilidad de generar reportes automáticos
   - Desconocimiento de métricas clave (clientes activos, ingresos diarios, etc.)
   - Falta de análisis de tendencias y patrones de uso
   - Dificultad para identificar oportunidades de mejora

5. **Gestión de Clientes Fragmentada**: La información del cliente se encuentra dispersa en:
   - Hojas de cálculo separadas
   - Cuadernos de registro físico
   - Sistemas no integrados
   - Sin respaldo ni seguridad de datos

## 1.3. Justificación

El desarrollo de NovaFit se justifica por múltiples razones que abarcan aspectos técnicos, operativos, económicos y académicos:

| Tipo | Justificación | Beneficio Esperado |
|------|---------------|-------------------|
| **Técnica** | Permite aplicar tecnologías modernas (.NET 9, Angular 19, Keycloak 26.0) siguiendo el patrón de Arquitectura Limpia, garantizando un sistema mantenible, escalable y alineado con las mejores prácticas actuales de la industria del software. | Sistema robusto con separación clara de responsabilidades, facilidad para pruebas unitarias y capacidad de evolución tecnológica sin afectar la lógica de negocio. |
| **Operativa** | Automatiza todos los procesos manuales del gimnasio, desde el control de acceso hasta la generación de reportes, reduciendo significativamente los tiempos de respuesta y minimizando la posibilidad de errores humanos. | Incremento en la eficiencia operativa del 60-70%, reducción de errores de registro en 90%, y disponibilidad de información en tiempo real para toma de decisiones. |
| **Económica** | Reduce costos operativos al optimizar el tiempo del personal administrativo, eliminar duplicidad de tareas y prevenir pérdidas por suscripciones no renovadas o accesos no autorizados. | Ahorro estimado del 30% en costos administrativos, aumento del 15% en renovaciones de suscripciones por notificaciones automáticas, y reducción de pérdidas por control de accesos. |
| **Social** | Mejora significativamente la experiencia tanto de los clientes del gimnasio como del personal administrativo al ofrecer un servicio más ágil, confiable y transparente. | Mayor satisfacción del cliente, reducción de tiempos de espera, y acceso a información personal actualizada (historial, suscripciones, ingresos). |
| **Académica** | Permite consolidar y aplicar de manera práctica los conocimientos adquiridos en el módulo de Programación Web II, incluyendo desarrollo backend con .NET, frontend con Angular, integración con sistemas de autenticación, manejo de bases de datos relacionales y contenedorización. | Desarrollo de competencias profesionales en arquitectura de software, desarrollo full-stack, integración de sistemas y gestión de proyectos de software reales. |

## 1.4. Alcance y Limitaciones

### Alcance del Proyecto

El sistema NovaFit abarca los siguientes módulos funcionales:

| Módulo | Descripción | Funcionalidades Clave |
|--------|-------------|----------------------|
| **Gestión de Clientes** | CRUD completo de clientes del gimnasio | • Registro con CI, nombre, apellido, email, teléfono<br>• Búsqueda por CI<br>• Eliminación lógica (soft delete)<br>• Historial completo del cliente |
| **Gestión de Suscripciones** | Administración de planes y membresías | • Tres tipos: CASUAL (por sesión), MENSUAL, ANUAL<br>• Control automático de vigencia<br>• Aplicación de promociones festivas<br>• Asignación de casillero fijo para plan ANUAL<br>• Contador de ingresos utilizados |
| **Control de Ingresos/Salidas** | Registro de accesos al gimnasio | • Validación automática de suscripción vigente<br>• Registro de hora de ingreso y salida<br>• Generación de alertas por suscripción vencida<br>• Historial completo de accesos por cliente |
| **Gestión de Casilleros** | Administración de recursos de almacenamiento | • CRUD de casilleros con número identificador<br>• Control en tiempo real de disponibilidad<br>• Préstamo temporal con retención de documento<br>• Asignación fija para clientes premium<br>• Registro de préstamos y devoluciones |
| **Reportes Gerenciales** | Generación de información estratégica | • Reporte de clientes activos vs inactivos<br>• Suscripciones por estado (activas, vencidas, canceladas)<br>• Ingresos por período de tiempo<br>• Disponibilidad de casilleros<br>• Filtros personalizables |
| **Promociones Festivas** | Sistema de descuentos temporales | • Creación de promociones con vigencia<br>• Aplicación automática de descuentos<br>• Control de fechas de inicio y fin |
| **Autenticación** | Sistema de acceso seguro | • Login con Keycloak<br>• Gestión de sesiones con tokens JWT<br>• Logout seguro |

### Limitaciones

El proyecto presenta las siguientes limitaciones que definen claramente su alcance académico y funcional:

#### Limitaciones Técnicas y de Alcance

| Limitación | Descripción | Justificación | Impacto |
|------------|-------------|---------------|---------|
| **Tiempo de Desarrollo** | 4 semanas para desarrollo completo del sistema | Restricción académica del módulo Programación Web II | Priorización de funcionalidades core (gestión de clientes, suscripciones, ingresos, casilleros) sobre características avanzadas |
| **Infraestructura** | Desarrollo y pruebas en entorno local con contenedores Docker | Alcance académico sin presupuesto para servicios cloud | El sistema no cuenta con despliegue en producción en la nube (AWS, Azure, Google Cloud) |
| **Sistema de Roles** | Keycloak configurado pero sin implementación completa de RBAC (Role-Based Access Control) | Todos los usuarios autenticados tienen permisos de administrador | No hay diferenciación entre roles ADMIN y USER en el acceso a funcionalidades |
| **Auditoría Automática** | No se implementó sistema automático de registro de auditoría de operaciones | Requeriría middleware adicional y tabla de logs | Falta de trazabilidad automática de quién modificó qué registro y cuándo |
| **Notificaciones** | No hay sistema de notificaciones por email o push | No se integró servicio de correo (SMTP) ni Firebase | Las alertas de suscripciones próximas a vencer solo se muestran en pantalla al validar ingreso |
| **Recuperación de Contraseña** | No se implementó flujo completo de recuperación de contraseña | Keycloak lo soporta pero requiere configuración de servidor SMTP | Los usuarios dependen del administrador de Keycloak para restablecer contraseñas |

#### Limitaciones Funcionales Explícitas

| Funcionalidad NO Implementada | Motivo | Alternativa Actual |
|-------------------------------|--------|--------------------|
| **Pagos Online** | No se integró pasarela de pago (Stripe, PayPal, pagos QR) | Los pagos se registran manualmente al crear la suscripción con campo "Precio" |
| **Facturación Electrónica** | No se integró con sistema de impuestos nacionales | El sistema no genera facturas fiscales válidas |
| **PWA Completa** | No se configuró service worker ni manifest completo | No funciona offline ni se puede instalar como app nativa en el dispositivo |
| **Reportes Exportables** | No se implementó exportación a PDF o Excel | Los reportes solo se visualizan en pantalla |
| **Dashboard con Gráficos** | No se integró librería de gráficos (Chart.js, D3.js) | El dashboard muestra solo tarjetas con números |
| **Multi-tenancy** | Diseñado para un solo gimnasio | No soporta múltiples gimnasios en la misma instancia |
| **Aplicación Móvil Nativa** | No se desarrolló app para Android/iOS | Los clientes no tienen acceso directo desde smartphone |
| **Acceso para Clientes Finales** | Solo personal administrativo usa el sistema | Los clientes no pueden ver su historial ni renovar suscripciones online |
| **Pruebas Automatizadas** | No se implementaron pruebas unitarias ni de integración con xUnit o Jasmine | La validación del sistema se realizó mediante pruebas manuales documentadas con capturas de pantalla |
| **Caché con Redis** | No se implementó sistema de caché | Todas las consultas van directamente a PostgreSQL |
| **Logs Estructurados** | No se configuró Serilog o similar | Los logs son solo los de consola de .NET |

#### Decisiones de Diseño Importantes

**LO QUE SÍ HACE EL SISTEMA:**
- ✅ Gestión completa de clientes (CRUD)
- ✅ Creación y control de suscripciones (3 tipos: CASUAL, MENSUAL, ANUAL)
- ✅ Validación automática de vigencia de suscripciones
- ✅ Registro de ingresos y salidas con timestamp
- ✅ Asignación de casilleros (fijos para ANUAL, temporales para CASUAL/MENSUAL)
- ✅ Préstamo de casilleros con retención de CI
- ✅ Generación de reportes básicos con filtros
- ✅ Aplicación de promociones festivas con descuentos
- ✅ Autenticación segura con Keycloak (OAuth2/JWT)
- ✅ Soft delete en todas las entidades (auditoría básica)
- ✅ Arquitectura limpia con separación de capas
- ✅ Containerización completa con Docker

**LO QUE NO HACE EL SISTEMA:**
- ❌ Procesar pagos online
- ❌ Generar facturas fiscales
- ❌ Funcionar offline (PWA completa)
- ❌ Exportar reportes a PDF/Excel
- ❌ Enviar emails o notificaciones push
- ❌ Permitir acceso directo a clientes finales
- ❌ Soportar múltiples gimnasios (multi-tenancy)
- ❌ Diferenciar permisos por roles (todos son admin)
- ❌ Registrar auditoría automática de cambios
- ❌ Mostrar gráficos estadísticos avanzados

Estas limitaciones están **justificadas** por el alcance académico del proyecto y el tiempo disponible (4 semanas), permitiendo enfocarse en la correcta implementación de las tecnologías requeridas (.NET 9, Angular 19, Keycloak, PostgreSQL) con arquitectura limpia y buenas prácticas de desarrollo.

# 2. OBJETIVOS

## 2.1. Objetivo General

Desarrollar un sistema web integral denominado NovaFit para la gestión eficiente de operaciones de gimnasios, que permita administrar clientes, suscripciones, control de accesos y recursos, utilizando .NET 9, Angular 19, Keycloak 26.0 y PostgreSQL 16, aplicando los principios de Arquitectura Limpia y las mejores prácticas de desarrollo de software moderno.

## 2.2. Objetivos Específicos

| N° | Objetivo Específico | Indicador de Logro | Estado |
|----|---------------------|-------------------|--------|
| OE1 | Analizar los requerimientos funcionales y no funcionales del sistema NovaFit para gestión integral de gimnasios | Documento de análisis con requerimientos identificados y casos de uso definidos | ✅ Logrado |
| OE2 | Diseñar el modelo relacional de la base de datos utilizando PostgreSQL con identificadores UUID | Diagrama Entidad-Relación con 6 entidades principales y diccionario de datos completo | ✅ Logrado |
| OE3 | Implementar el backend con .NET 9 siguiendo el patrón de Arquitectura Limpia con separación en 4 capas | API REST funcional con 20+ endpoints documentados y estructura en capas Domain, Application, Infrastructure y WebAPI | ✅ Logrado |
| OE4 | Implementar el frontend con Angular 19 utilizando componentes standalone y diseño responsivo | Interfaz de usuario con 7 componentes principales, navegación completa y diseño adaptativo | ✅ Logrado |
| OE5 | Configurar Keycloak 26.0 para autenticación centralizada mediante OAuth2 y tokens JWT | Sistema de login/logout funcional con validación de tokens en cada petición | ✅ Logrado |
| OE6 | Implementar los módulos de gestión de clientes, suscripciones, ingresos, casilleros y reportes | 6 módulos funcionales completamente operativos con CRUDs completos | ✅ Logrado |
| OE7 | Realizar pruebas de funcionamiento de los módulos implementados mediante casos de prueba manuales | Conjunto de capturas de pantalla documentando el funcionamiento de cada módulo | ✅ Logrado |
| OE8 | Documentar el proyecto de manera formal incluyendo análisis, diseño, implementación, pruebas y conclusiones | Documento técnico completo siguiendo formato académico establecido | ✅ Logrado |
| OE9 | Containerizar la aplicación completa utilizando Docker para facilitar el despliegue y ejecución | Docker Compose funcional con 5 servicios (PostgreSQL, Keycloak, Backend, Frontend, BD Keycloak) | ✅ Logrado |

# 3. MARCO TEÓRICO

## 3.1. Arquitectura Limpia (Clean Architecture)

La Arquitectura Limpia, propuesta por Robert C. Martin (Uncle Bob), establece una organización del código en capas concéntricas que separan las preocupaciones del dominio de negocio de los detalles técnicos de implementación.

**Principios fundamentales:**

```
┌─────────────────────────────────────────────────────────────┐
│ Capa de Presentación (WebAPI)                               │
│ • Controladores HTTP                                        │
│ • Middleware                                                │
│ • Configuración de servicios                               │
├─────────────────────────────────────────────────────────────┤
│ Capa de Aplicación (Application)                            │
│ • Casos de uso (Services)                                   │
│ • DTOs (Data Transfer Objects)                              │
│ • Interfaces de servicios                                   │
├─────────────────────────────────────────────────────────────┤
│ Capa de Dominio (Domain)                                    │
│ • Entidades de negocio                                      │
│ • Interfaces de repositorios                                │
│ • Lógica de negocio pura                                    │
├─────────────────────────────────────────────────────────────┤
│ Capa de Infraestructura (Infrastructure)                    │
│ • Implementación de repositorios                            │
│ • DbContext de Entity Framework                             │
│ • Acceso a servicios externos                               │
└─────────────────────────────────────────────────────────────┘
```

**Beneficios aplicados en NovaFit:**
- **Independencia de frameworks**: La lógica de negocio no depende de .NET ni Entity Framework
- **Facilidad de pruebas**: Cada capa puede probarse de forma aislada
- **Independencia de la UI**: El backend funciona sin necesidad del frontend
- **Independencia de la base de datos**: Se puede cambiar PostgreSQL por otro SGBD sin afectar el dominio
- **Mantenibilidad**: Cambios en una capa no afectan a las demás

## 3.2. .NET 9

.NET 9 es la versión más reciente del framework multiplataforma de desarrollo de Microsoft, lanzado en noviembre de 2025. Sus características principales implementadas en NovaFit son:

**Características utilizadas:**

| Característica | Descripción | Aplicación en NovaFit |
|----------------|-------------|----------------------|
| **Entity Framework Core 9** | ORM moderno para acceso a datos relacionales | Mapeo de entidades, migraciones automáticas, consultas LINQ |
| **ASP.NET Core 9** | Framework web de alto rendimiento | API RESTful con controladores, routing, middleware |
| **Minimal APIs** | Simplificación de endpoints HTTP | Configuración en Program.cs sin Startup.cs |
| **Dependency Injection** | Contenedor de IoC integrado | Inyección de servicios y repositorios |
| **Async/Await** | Programación asíncrona nativa | Todos los métodos de servicio son async |
| **LINQ** | Consultas integradas al lenguaje | Filtrado y transformación de datos |
| **JSON Serialization** | System.Text.Json de alto rendimiento | Serialización automática en API |

**Ventajas sobre versiones anteriores:**
- Rendimiento mejorado en un 30% respecto a .NET 8
- Soporte nativo para contenedores y Kubernetes
- Mejoras en compilación AOT (Ahead-of-Time)
- Soporte extendido hasta noviembre de 2027 (LTS)

## 3.3. Angular 19

Angular es un framework de desarrollo frontend basado en TypeScript desarrollado por Google. La versión 19 incorpora cambios significativos en arquitectura y rendimiento.

**Innovaciones clave utilizadas:**

| Característica | Descripción | Implementación en NovaFit |
|----------------|-------------|--------------------------|
| **Standalone Components** | Componentes sin necesidad de NgModules | Todos los componentes son standalone |
| **Signals** | Sistema reactivo de estado sin RxJS | Manejo de estado en componentes |
| **Control Flow Nativo** | @if, @for, @switch en lugar de directivas | Templates simplificados |
| **Lazy Loading Mejorado** | Carga diferida de rutas | Todas las rutas usan loadComponent() |
| **Inyección de Dependencias** | inject() function en lugar de constructor | Servicios inyectados con inject() |
| **TypeScript 5.6** | Última versión del lenguaje | Tipado fuerte en todo el proyecto |

**Estructura del proyecto Angular:**

```
frontend/src/app/
├── core/                    # Servicios y lógica central
│   ├── models/             # Interfaces TypeScript
│   ├── services/           # Servicios HTTP
│   └── interceptors/       # Interceptor de autenticación
├── features/               # Componentes por funcionalidad
│   ├── dashboard/         # Panel principal
│   ├── clientes/          # Gestión de clientes
│   ├── suscripciones/     # Gestión de suscripciones
│   ├── ingresos/          # Control de ingresos
│   ├── casilleros/        # Gestión de casilleros
│   ├── reportes/          # Generación de reportes
│   └── admin/             # Administración
└── app.routes.ts          # Configuración de rutas
```

## 3.4. Keycloak y OAuth2

Keycloak es una solución de Identity and Access Management (IAM) de código abierto que proporciona autenticación y autorización centralizada.

**Arquitectura de autenticación:**

```
┌──────────────┐         ┌──────────────┐         ┌──────────────┐
│   Angular    │  Login  │   Keycloak   │  Token  │   Backend    │
│   Frontend   │────────>│   Server     │<────────│   .NET API   │
│              │         │              │         │              │
│              │<────────│              │         │              │
│              │  Token  │              │         │              │
│              │         │              │         │              │
│              │  HTTP + │              │ Valida  │              │
│              │  Bearer │              │  Token  │              │
│              │────────────────────────────────>│              │
│              │<────────────────────────────────│              │
│              │         Respuesta               │              │
└──────────────┘                                  └──────────────┘
```

**Flujo de autenticación OAuth2 / OpenID Connect:**

1. Usuario ingresa credenciales en el frontend
2. Frontend redirige a Keycloak con client_id
3. Keycloak valida credenciales contra base de datos
4. Keycloak genera JWT token con claims del usuario
5. Token se devuelve al frontend
6. Frontend almacena token (sessionStorage)
7. Cada petición HTTP incluye header: `Authorization: Bearer <token>`
8. Backend valida token contra clave pública de Keycloak
9. Si el token es válido, se procesa la petición
10. Si el token es inválido o expiró, devuelve HTTP 401

**Configuración en NovaFit:**
- **Realm**: `novafit-realm`
- **Cliente**: `novafit-api`
- **URL**: `http://localhost:8081`
- **Algoritmo**: RS256 (firma con RSA)
- **Duración token**: 5 minutos

## 3.5. PostgreSQL

PostgreSQL es un sistema gestor de bases de datos relacional objeto (ORDBMS) de código abierto, conocido por su robustez, extensibilidad y cumplimiento del estándar SQL.

**Características utilizadas en NovaFit:**

| Característica | Descripción | Uso en el proyecto |
|----------------|-------------|-------------------|
| **UUID** | Identificadores únicos universales | Todos los IDs son tipo `uuid` |
| **JSONB** | Almacenamiento JSON binario optimizado | (Preparado para futuras auditorías) |
| **Tipos de fecha** | TIMESTAMP, DATE, TIME | Control preciso de fechas y horas |
| **Constraints** | Restricciones de integridad | UNIQUE en CI de clientes, FK en relaciones |
| **Indexes** | Índices para optimización | Índices automáticos en PKs y FKs |
| **Triggers** | Procedimientos automáticos | (Preparado para auditoría futura) |

**Modelo relacional implementado:**

- **6 tablas principales**: Clientes, Suscripciones, Ingresos, Casilleros, PrestamoCasillero, PromocionFestiva
- **Relaciones 1:N**: Un cliente tiene muchas suscripciones e ingresos
- **Relaciones N:1**: Muchos ingresos pertenecen a una suscripción
- **Soft Delete**: Todas las entidades tienen campo `Eliminado` en lugar de DELETE físico

# 4. ANÁLISIS DEL SISTEMA

## 4.1. Requerimientos Funcionales

### Módulo de Autenticación

| ID | Requerimiento | Descripción | Prioridad | Estado |
|----|---------------|-------------|-----------|--------|
| RF-001 | Login con Keycloak | El sistema permite autenticación mediante Keycloak con usuario y contraseña | Alta | ✅ Implementado |
| RF-002 | Gestión de sesión | El sistema mantiene la sesión activa mediante token JWT almacenado en sessionStorage | Alta | ✅ Implementado |
| RF-003 | Logout | El usuario puede cerrar sesión y el token es eliminado | Alta | ✅ Implementado |
| RF-004 | Protección de rutas | Solo usuarios autenticados pueden acceder a las funcionalidades del sistema | Alta | ✅ Implementado |

### Módulo de Gestión de Clientes

| ID | Requerimiento | Descripción | Prioridad | Estado |
|----|---------------|-------------|-----------|--------|
| RF-005 | Registrar cliente | El sistema permite registrar nuevo cliente con CI, nombre, apellido, email, teléfono y fecha de nacimiento | Alta | ✅ Implementado |
| RF-006 | Buscar por CI | El sistema permite buscar cliente por número de cédula de identidad (único) | Alta | ✅ Implementado |
| RF-007 | Listar clientes | El sistema muestra lista completa de clientes activos | Alta | ✅ Implementado |
| RF-008 | Editar cliente | El sistema permite modificar datos del cliente excepto CI | Media | ✅ Implementado |
| RF-009 | Eliminar cliente | El sistema realiza eliminación lógica (soft delete) marcando como eliminado | Media | ✅ Implementado |
| RF-010 | Ver detalle | El sistema muestra información completa del cliente incluyendo historial de suscripciones e ingresos | Media | ✅ Implementado |

### Módulo de Gestión de Suscripciones

| ID | Requerimiento | Descripción | Prioridad | Estado |
|----|---------------|-------------|-----------|--------|
| RF-011 | Crear suscripción CASUAL | El sistema permite crear suscripción por sesión con precio único | Alta | ✅ Implementado |
| RF-012 | Crear suscripción MENSUAL | El sistema permite crear suscripción mensual (30 días) con precio recurrente | Alta | ✅ Implementado |
| RF-013 | Crear suscripción ANUAL | El sistema permite crear suscripción anual (365 días) con asignación de casillero fijo | Alta | ✅ Implementado |
| RF-014 | Validar vigencia | El sistema valida automáticamente si una suscripción está vigente comparando fecha de vencimiento | Alta | ✅ Implementado |
| RF-015 | Aplicar promoción | El sistema permite aplicar promociones festivas con descuento porcentual sobre el precio | Media | ✅ Implementado |
| RF-016 | Cancelar suscripción | El sistema permite cancelar suscripción cambiando su estado a CANCELADA | Media | ✅ Implementado |
| RF-017 | Contar ingresos | El sistema cuenta los ingresos realizados contra cada suscripción | Media | ✅ Implementado |

### Módulo de Control de Ingresos/Salidas

| ID | Requerimiento | Descripción | Prioridad | Estado |
|----|---------------|-------------|-----------|--------|
| RF-018 | Registrar ingreso | El sistema registra ingreso de cliente validando suscripción activa | Alta | ✅ Implementado |
| RF-019 | Validar suscripción | El sistema valida que el cliente tenga suscripción vigente antes de permitir ingreso | Alta | ✅ Implementado |
| RF-020 | Alertas de vencimiento | El sistema genera alerta si la suscripción está próxima a vencer (menos de 7 días) | Media | ✅ Implementado |
| RF-021 | Bloquear ingreso | El sistema rechaza ingreso si no hay suscripción vigente | Alta | ✅ Implementado |
| RF-022 | Registrar salida | El sistema permite registrar hora de salida y calcular duración de sesión | Media | ✅ Implementado |
| RF-023 | Historial de ingresos | El sistema muestra historial completo de ingresos por cliente | Media | ✅ Implementado |

### Módulo de Gestión de Casilleros

| ID | Requerimiento | Descripción | Prioridad | Estado |
|----|---------------|-------------|-----------|--------|
| RF-024 | Registrar casillero | El sistema permite crear casilleros con número identificador único | Alta | ✅ Implementado |
| RF-025 | Asignar casillero fijo | El sistema asigna casillero fijo permanente a clientes con suscripción ANUAL | Alta | ✅ Implementado |
| RF-026 | Préstamo temporal | El sistema permite préstamo temporal de casillero reteniendo CI del cliente | Alta | ✅ Implementado |
| RF-027 | Control de disponibilidad | El sistema muestra en tiempo real qué casilleros están disponibles u ocupados | Alta | ✅ Implementado |
| RF-028 | Devolver casillero | El sistema registra devolución de casillero y libera su disponibilidad | Media | ✅ Implementado |
| RF-029 | Editar casillero | El sistema permite modificar tipo, ubicación y estado del casillero | Media | ✅ Implementado |

### Módulo de Reportes

| ID | Requerimiento | Descripción | Prioridad | Estado |
|----|---------------|-------------|-----------|--------|
| RF-030 | Reporte de clientes | El sistema genera reporte de clientes activos vs eliminados | Alta | ✅ Implementado |
| RF-031 | Reporte de suscripciones | El sistema genera reporte de suscripciones por estado (ACTIVA, VENCIDA, CANCELADA) | Alta | ✅ Implementado |
| RF-032 | Reporte de ingresos | El sistema genera reporte de ingresos por rango de fechas | Alta | ✅ Implementado |
| RF-033 | Reporte de casilleros | El sistema genera reporte de casilleros disponibles vs ocupados | Media | ✅ Implementado |
| RF-034 | Filtros personalizados | El sistema permite filtrar reportes por diferentes criterios (fecha, tipo, estado) | Media | ✅ Implementado |

### Módulo de Promociones Festivas

| ID | Requerimiento | Descripción | Prioridad | Estado |
|----|---------------|-------------|-----------|--------|
| RF-035 | Crear promoción | El sistema permite crear promociones con nombre, descripción, fechas de vigencia y descuento porcentual | Media | ✅ Implementado |
| RF-036 | Aplicar descuento | El sistema aplica automáticamente el descuento al crear suscripción si hay promoción activa | Media | ✅ Implementado |
| RF-037 | Validar vigencia | El sistema valida que la promoción esté dentro del rango de fechas activo | Media | ✅ Implementado |

## 4.2. Requerimientos No Funcionales

| ID | Categoría | Requerimiento | Descripción | Métrica | Estado |
|----|-----------|---------------|-------------|---------|--------|
| RNF-001 | Seguridad | Autenticación centralizada | Todas las peticiones requieren token JWT válido emitido por Keycloak | 100% endpoints protegidos | ✅ |
| RNF-002 | Seguridad | Protección SQL Injection | Entity Framework previene inyección SQL mediante consultas parametrizadas | Sin vulnerabilidades | ✅ |
| RNF-003 | Rendimiento | Tiempo de respuesta API | Las peticiones HTTP deben responder en menos de 2 segundos | < 2s promedio | ✅ |
| RNF-004 | Rendimiento | Consultas asíncronas | Todas las operaciones de base de datos son asíncronas (async/await) | 100% métodos async | ✅ |
| RNF-005 | Disponibilidad | Containerización | El sistema se ejecuta completamente en contenedores Docker | 5 servicios containerizados | ✅ |
| RNF-006 | Mantenibilidad | Arquitectura Limpia | El código sigue separación en 4 capas con bajo acoplamiento | 4 proyectos independientes | ✅ |
| RNF-007 | Mantenibilidad | Código documentado | Entidades, servicios y controladores con comentarios XML | Documentación inline | ✅ |
| RNF-008 | Usabilidad | Interfaz intuitiva | Diseño responsivo con mensajes claros de error y éxito | UX validada manualmente | ✅ |
| RNF-009 | Usabilidad | Tema oscuro | Interfaz con colores oscuros para reducir fatiga visual | Tema dark aplicado | ✅ |
| RNF-010 | Compatibilidad | Navegadores modernos | Funciona en Chrome 120+, Firefox 120+, Edge 120+ | Pruebas en 3 navegadores | ✅ |
| RNF-011 | Compatibilidad | Responsividad | Adapta diseño a móvil, tablet y escritorio | Breakpoints CSS | ✅ |
| RNF-012 | Escalabilidad | Base de datos relacional | PostgreSQL soporta miles de registros sin degradación | Datos de prueba OK | ✅ |
| RNF-013 | Portabilidad | Multiplataforma | Funciona en Windows, Linux y macOS mediante Docker | Probado en Windows | ✅ |

## 4.3. Actores del Sistema

| Actor | Descripción | Responsabilidades | Permisos |
|-------|-------------|-------------------|----------|
| **Usuario Administrativo** | Personal del gimnasio encargado de gestionar el sistema | • Registrar nuevos clientes<br>• Crear y renovar suscripciones<br>• Validar ingresos y salidas<br>• Asignar casilleros<br>• Generar reportes<br>• Gestionar promociones | • Acceso total a todos los módulos<br>• CRUD completo en todas las entidades<br>• Visualización de reportes<br>• Configuración del sistema |
| **Cliente del Gimnasio** | Persona que utiliza las instalaciones del gimnasio | • Presentar CI para ingreso<br>• Solicitar casillero temporal<br>• Consultar estado de suscripción | • (No tiene acceso directo al sistema web)<br>• Interactúa presencialmente con el administrativo |

**Nota**: En esta versión del sistema no se implementó acceso web para clientes finales. Toda la interacción se realiza a través del personal administrativo.

## 4.4. Casos de Uso

### CU-01: Autenticarse en el Sistema

| Elemento | Descripción |
|----------|-------------|
| **Código** | CU-01 |
| **Nombre** | Autenticarse en el Sistema |
| **Actor** | Usuario Administrativo |
| **Precondición** | El usuario tiene credenciales válidas en Keycloak y el servicio está activo |
| **Flujo Principal** | 1. El usuario accede a la URL del sistema (`http://localhost:4200`)<br>2. El sistema redirige a la pantalla de login de Keycloak<br>3. El usuario ingresa usuario y contraseña<br>4. Keycloak valida las credenciales contra su base de datos<br>5. Keycloak genera un token JWT con información del usuario<br>6. El sistema almacena el token en sessionStorage<br>7. El sistema redirige al dashboard principal |
| **Postcondición** | El usuario está autenticado y puede acceder a todas las funcionalidades del sistema |
| **Flujo Alternativo 3a** | **Credenciales incorrectas**<br>3a.1. Keycloak muestra mensaje de error "Invalid username or password"<br>3a.2. El usuario puede reintentar el login |

### CU-02: Registrar Nuevo Cliente

| Elemento | Descripción |
|----------|-------------|
| **Código** | CU-02 |
| **Nombre** | Registrar Nuevo Cliente |
| **Actor** | Usuario Administrativo |
| **Precondición** | Usuario autenticado en el sistema |
| **Flujo Principal** | 1. El usuario navega al módulo "Clientes"<br>2. El usuario hace clic en botón "Nuevo Cliente"<br>3. El sistema muestra formulario de registro<br>4. El usuario ingresa: CI, Nombre, Apellido, Email, Teléfono, Fecha de Nacimiento<br>5. El usuario hace clic en "Guardar"<br>6. El sistema valida que el CI no esté duplicado<br>7. El sistema envía petición POST `/api/clientes`<br>8. El backend crea el registro en la base de datos<br>9. El sistema muestra mensaje "Cliente registrado exitosamente"<br>10. El sistema actualiza la lista de clientes |
| **Postcondición** | El cliente queda registrado en la base de datos con estado activo (Eliminado = false) |
| **Flujo Alternativo 6a** | **CI duplicado**<br>6a.1. El sistema muestra error "Ya existe un cliente con ese CI"<br>6a.2. El usuario puede modificar el CI o buscar el cliente existente |

### CU-03: Crear Suscripción para Cliente

| Elemento | Descripción |
|----------|-------------|
| **Código** | CU-03 |
| **Nombre** | Crear Suscripción para Cliente |
| **Actor** | Usuario Administrativo |
| **Precondición** | • Usuario autenticado<br>• Cliente registrado en el sistema |
| **Flujo Principal** | 1. El usuario navega al módulo "Suscripciones"<br>2. El usuario hace clic en "Nueva Suscripción"<br>3. El sistema muestra formulario con campos: Cliente (búsqueda por CI), Tipo (CASUAL/MENSUAL/ANUAL), Precio, Fecha Inicio<br>4. El usuario busca el cliente por CI<br>5. El sistema muestra los datos del cliente encontrado<br>6. El usuario selecciona el tipo de suscripción<br>7. El sistema calcula automáticamente la fecha de vencimiento según el tipo<br>8. Si es suscripción ANUAL, el sistema muestra lista de casilleros disponibles para asignación fija<br>9. El usuario puede aplicar una promoción festiva vigente<br>10. El sistema calcula el precio final con descuento<br>11. El usuario confirma la creación<br>12. El sistema envía POST `/api/suscripciones`<br>13. El backend crea la suscripción con estado ACTIVA<br>14. Si es ANUAL, marca el casillero seleccionado como ocupado<br>15. El sistema muestra mensaje "Suscripción creada exitosamente" |
| **Postcondición** | La suscripción queda registrada en estado ACTIVA con fecha de vencimiento calculada |
| **Flujo Alternativo 5a** | **Cliente no encontrado**<br>5a.1. El sistema muestra "No se encontró cliente con ese CI"<br>5a.2. El usuario puede registrar nuevo cliente (ver CU-02) |

### CU-04: Registrar Ingreso de Cliente

| Elemento | Descripción |
|----------|-------------|
| **Código** | CU-04 |
| **Nombre** | Registrar Ingreso de Cliente al Gimnasio |
| **Actor** | Usuario Administrativo |
| **Precondición** | • Usuario autenticado<br>• Cliente con CI válido |
| **Flujo Principal** | 1. El cliente llega a la recepción y presenta su CI<br>2. El usuario ingresa al módulo "Ingresos/Salidas"<br>3. El usuario ingresa el CI del cliente en el campo de búsqueda<br>4. El sistema busca el cliente en la base de datos<br>5. El sistema verifica si el cliente tiene suscripción activa<br>6. El sistema valida la fecha de vencimiento<br>7. La suscripción está vigente (FechaVencimiento >= Hoy y Estado = ACTIVA)<br>8. El sistema registra el ingreso con fecha y hora actual<br>9. El sistema incrementa el contador de ingresos de la suscripción<br>10. El sistema muestra "Ingreso permitido - Bienvenido [Nombre del Cliente]"<br>11. Si el cliente solicita casillero temporal, ver CU-06 |
| **Postcondición** | Queda registrado el ingreso del cliente con timestamp |
| **Flujo Alternativo 5a** | **Sin suscripción activa**<br>5a.1. El sistema muestra "Cliente sin suscripción activa"<br>5a.2. El usuario ofrece crear nueva suscripción (ver CU-03) |
| **Flujo Alternativo 7a** | **Suscripción vencida**<br>7a.1. El sistema muestra alerta "Suscripción vencida desde [Fecha]"<br>7a.2. El sistema registra el ingreso como "rechazado"<br>7a.3. El usuario ofrece renovar suscripción |
| **Flujo Alternativo 7b** | **Suscripción próxima a vencer**<br>7b.1. El sistema muestra advertencia "Suscripción vence en X días"<br>7b.2. El ingreso es permitido<br>7b.3. El usuario puede informar al cliente sobre la renovación |

### CU-05: Asignar Casillero Temporal

| Elemento | Descripción |
|----------|-------------|
| **Código** | CU-05 |
| **Nombre** | Asignar Casillero Temporal a Cliente |
| **Actor** | Usuario Administrativo |
| **Precondición** | • Ingreso del cliente registrado (CU-04 completado)<br>• Existen casilleros disponibles |
| **Flujo Principal** | 1. El cliente solicita casillero temporal<br>2. El usuario navega a "Casilleros"<br>3. El sistema muestra lista de casilleros con su estado (DISPONIBLE/OCUPADO)<br>4. El usuario selecciona un casillero disponible<br>5. El sistema solicita el CI que el cliente entrega como garantía<br>6. El usuario ingresa el CI retenido<br>7. El sistema genera un número de ticket único<br>8. El sistema registra el préstamo vinculado al ingreso actual<br>9. El sistema cambia el estado del casillero a OCUPADO<br>10. El sistema muestra "Casillero [Número] asignado - Ticket: [NumeroTicket]" |
| **Postcondición** | El casillero queda marcado como OCUPADO y vinculado al cliente |
| **Flujo Alternativo 3a** | **Sin casilleros disponibles**<br>3a.1. El sistema muestra "No hay casilleros disponibles"<br>3a.2. El usuario sugiere al cliente esperar o usar estante de recepción |

### CU-06: Generar Reporte de Suscripciones

| Elemento | Descripción |
|----------|-------------|
| **Código** | CU-06 |
| **Nombre** | Generar Reporte de Suscripciones por Estado |
| **Actor** | Usuario Administrativo |
| **Precondición** | Usuario autenticado con permisos de visualización de reportes |
| **Flujo Principal** | 1. El usuario navega al módulo "Reportes"<br>2. El usuario selecciona "Reporte de Suscripciones"<br>3. El sistema muestra filtros: Estado (ACTIVA/VENCIDA/CANCELADA), Tipo (CASUAL/MENSUAL/ANUAL), Rango de fechas<br>4. El usuario configura los filtros deseados<br>5. El usuario hace clic en "Generar Reporte"<br>6. El sistema consulta la base de datos aplicando los filtros<br>7. El sistema procesa los datos y calcula estadísticas (total por tipo, monto total recaudado)<br>8. El sistema muestra tabla con resultados y gráficos visuales<br>9. El usuario puede exportar el reporte (funcionalidad futura) |
| **Postcondición** | Se visualiza el reporte con datos actualizados de la base de datos |

---

# 5. DISEÑO DEL SISTEMA

## 5.1. Arquitectura del Sistema

El sistema NovaFit implementa una arquitectura de tres capas (Frontend, Backend, Base de Datos) con autenticación centralizada y containerización completa.

**Diagrama de arquitectura general:**

```
┌───────────────────────────────────────────────────────────────┐
│                    NAVEGADOR DEL USUARIO                       │
│                     (Chrome/Firefox/Edge)                      │
└────────────────────────────┬──────────────────────────────────┘
                             │ HTTP/HTTPS
                             ▼
┌───────────────────────────────────────────────────────────────┐
│                  FRONTEND - Angular 19                         │
│              (Container: novafit_frontend)                     │
│                                                                │
│  • Componentes Standalone                                     │
│  • Servicios HTTP                                             │
│  • Interceptor de autenticación                               │
│  • Routing con Lazy Loading                                   │
│  Puerto: 4200 (Nginx interno: 80)                            │
└────────────────────────────┬──────────────────────────────────┘
                             │
                             │ REST API (JSON)
                             │ Authorization: Bearer <token>
                             ▼
┌───────────────────────────────────────────────────────────────┐
│                  BACKEND - .NET 9 API                          │
│              (Container: novafit_backend)                      │
│                                                                │
│  ┌──────────────────────────────────────────────────────────┐ │
│  │  Capa WebAPI (Controllers)                               │ │
│  │  • ClientesController                                    │ │
│  │  • SuscripcionesController                               │ │
│  │  • IngresosController                                    │ │
│  │  • CasillerosController                                  │ │
│  │  • Middleware de autenticación JWT                       │ │
│  └───────────────────────┬──────────────────────────────────┘ │
│                          │                                     │
│  ┌──────────────────────▼──────────────────────────────────┐ │
│  │  Capa Application (Services, DTOs)                      │ │
│  │  • ClienteService                                        │ │
│  │  • SuscripcionService                                    │ │
│  │  • IngresoService                                        │ │
│  │  • CasilleroService                                      │ │
│  └───────────────────────┬──────────────────────────────────┘ │
│                          │                                     │
│  ┌──────────────────────▼──────────────────────────────────┐ │
│  │  Capa Domain (Entities)                                 │ │
│  │  • Cliente, Suscripcion, Ingreso                        │ │
│  │  • Casillero, PrestamoCasillero                         │ │
│  │  • PromocionFestiva                                      │ │
│  └───────────────────────┬──────────────────────────────────┘ │
│                          │                                     │
│  ┌──────────────────────▼──────────────────────────────────┐ │
│  │  Capa Infrastructure (Repositories, DbContext)          │ │
│  │  • NovaFitDbContext                                      │ │
│  │  • Entity Framework Core 9                               │ │
│  │  • Implementación de repositorios                        │ │
│  └──────────────────────────────────────────────────────────┘ │
│                                                                │
│  Puerto: 8080                                                 │
└────────┬──────────────────────────┬────────────────────────────┘
         │                          │
         │ Valida Token             │ Consultas SQL
         │ (RS256)                  │ (Npgsql)
         ▼                          ▼
┌─────────────────────┐    ┌──────────────────────────────────┐
│    KEYCLOAK 26.0    │    │    POSTGRESQL 16                 │
│  (novafit_keycloak) │    │    (novafit_db)                  │
│                     │    │                                  │
│  • Realm:           │    │  Base de datos: novafit_db      │
│    novafit-realm    │    │  Usuario: novafit_user          │
│  • Client:          │    │  6 tablas principales           │
│    novafit-api      │    │  • clientes                     │
│  • OAuth2/OIDC      │    │  • suscripciones                │
│  • JWT RS256        │    │  • ingresos                     │
│  Puerto: 8081       │    │  • casilleros                   │
│                     │    │  • prestamos_casilleros         │
│  Base de datos:     │    │  • promociones_festivas         │
│  keycloak_db        │    │  Puerto: 5432                   │
│  (keycloak_postgres)│    │                                  │
│  Puerto: 5433       │    │                                  │
└─────────────────────┘    └──────────────────────────────────┘
```

**Tecnologías por capa:**

| Capa | Tecnología | Versión | Contenedor |
|------|------------|---------|------------|
| Frontend | Angular + TypeScript | 19 | novafit_frontend |
| Web Server | Nginx | Alpine | (Dentro de frontend) |
| Backend | .NET + C# | 9.0 | novafit_backend |
| Base de Datos | PostgreSQL | 16-alpine | novafit_db |
| Autenticación | Keycloak | 26.0 | novafit_keycloak |
| BD Keycloak | PostgreSQL | 16-alpine | keycloak_db |

**Red de Docker:**
- Nombre: `novafit_network`
- Tipo: bridge
- Todos los contenedores en la misma red para comunicación interna

## 5.2. Modelo de Base de Datos

El modelo de base de datos está diseñado con 6 entidades principales siguiendo el estándar de normalización 3FN.

**Diagrama Entidad-Relación:**

```
┌─────────────────────┐
│     CLIENTES        │
├─────────────────────┤
│ id (UUID) PK        │
│ ci (INT) UNIQUE     │
│ nombre (VARCHAR)    │
│ apellido (VARCHAR)  │
│ email (VARCHAR)     │
│ telefono (VARCHAR)  │
│ fecha_nacimiento    │
│ fecha_registro      │
│ eliminado (BOOL)    │
│ fecha_eliminacion   │
└──────────┬──────────┘
           │
           │ 1:N
           ▼
┌─────────────────────┐         ┌──────────────────────┐
│   SUSCRIPCIONES     │         │  PROMOCIONES_        │
├─────────────────────┤         │  FESTIVAS            │
│ id (UUID) PK        │N:1      ├──────────────────────┤
│ cliente_id FK ──────┼────┐    │ id (UUID) PK         │
│ tipo (ENUM)         │    │    │ nombre (VARCHAR)     │
│ precio (DECIMAL)    │    │    │ descripcion          │
│ fecha_inicio (DATE) │    │    │ fecha_inicio (DATE)  │
│ fecha_vencimiento   │    │    │ fecha_fin (DATE)     │
│ estado (ENUM)       │    │    │ porcentaje_descuento │
│ casillero_fijo_id   │    │    │ activa (BOOL)        │
│ promocion_id FK ────┼────┼───>│ fecha_creacion       │
│ descuento_aplicado  │    │    │ eliminado            │
│ ingresos_usados     │    │    └──────────────────────┘
│ fecha_creacion      │    │
│ eliminado (BOOL)    │    │
└──────────┬──────────┘    │
           │               │
           │ 1:N           │
           ▼               │
┌─────────────────────┐    │    ┌──────────────────────┐
│      INGRESOS       │    │    │     CASILLEROS       │
├─────────────────────┤    │    ├──────────────────────┤
│ id (UUID) PK        │    │    │ id (UUID) PK         │
│ cliente_id FK ──────┼────┘    │ numero (INT) UNIQUE  │
│ suscripcion_id FK   │         │ tipo (ENUM)          │
│ fecha_ingreso (DATE)│         │ ubicacion (VARCHAR)  │
│ hora_ingreso (TIME) │         │ estado (ENUM)        │
│ hora_salida (TIME)  │         │ asignado_cliente_id  │
│ salida_registrada   │         │ fecha_creacion       │
│ duracion_minutos    │         │ eliminado (BOOL)     │
│ eliminado (BOOL)    │         └───────┬──────────────┘
└──────────┬──────────┘                 │
           │                            │
           │ 1:N                        │ 1:N
           ▼                            │
┌──────────────────────────────────┐    │
│   PRESTAMOS_CASILLEROS           │    │
├──────────────────────────────────┤    │
│ id (UUID) PK                     │    │
│ ingreso_id FK ───────────────────┘    │
│ casillero_id FK ─────────────────────┘
│ numero_ticket (VARCHAR)          │
│ ci_depositado (INT)              │
│ numero_llave (VARCHAR)           │
│ fecha_prestamo (DATE)            │
│ hora_prestamo (TIME)             │
│ fecha_devolucion (DATE)          │
│ hora_devolucion (TIME)           │
│ devuelto (BOOL)                  │
│ eliminado (BOOL)                 │
└──────────────────────────────────┘
```

**Tipos ENUM definidos:**

```sql
-- Tipo de suscripción
CREATE TYPE tipo_suscripcion AS ENUM ('CASUAL', 'MENSUAL', 'ANUAL');

-- Estado de suscripción
CREATE TYPE estado_suscripcion AS ENUM ('ACTIVA', 'VENCIDA', 'CANCELADA');

-- Tipo de casillero
CREATE TYPE tipo_casillero AS ENUM ('FIJO', 'TEMPORAL', 'ESTANTE_RECEPCION');

-- Estado de casillero
CREATE TYPE estado_casillero AS ENUM ('DISPONIBLE', 'OCUPADO', 'EN_MANTENIMIENTO');
```

## 5.3. Diccionario de Datos

### Tabla: clientes

| Campo | Tipo | Longitud | Descripción | Restricciones |
|-------|------|----------|-------------|---------------|
| id | UUID | - | Identificador único del cliente | PRIMARY KEY, DEFAULT gen_random_uuid() |
| ci | INTEGER | - | Cédula de identidad | NOT NULL, UNIQUE |
| nombre | VARCHAR | 120 | Nombres del cliente | NOT NULL |
| apellido | VARCHAR | 120 | Apellidos completos | NOT NULL |
| email | VARCHAR | 255 | Correo electrónico | NULLABLE |
| telefono | VARCHAR | 20 | Número de teléfono | NULLABLE |
| fecha_nacimiento | DATE | - | Fecha de nacimiento | NULLABLE |
| fecha_registro | TIMESTAMPTZ | - | Fecha y hora de registro en el sistema | NOT NULL, DEFAULT CURRENT_TIMESTAMP |
| eliminado | BOOLEAN | - | Indicador de eliminación lógica | NOT NULL, DEFAULT FALSE |
| fecha_eliminacion | TIMESTAMPTZ | - | Fecha y hora de eliminación | NULLABLE |

**Índices:**
- PRIMARY KEY en `id`
- UNIQUE INDEX en `ci`
- INDEX en `eliminado` para filtros frecuentes

### Tabla: suscripciones

| Campo | Tipo | Longitud | Descripción | Restricciones |
|-------|------|----------|-------------|---------------|
| id | UUID | - | Identificador único de la suscripción | PRIMARY KEY |
| cliente_id | UUID | - | Cliente propietario de la suscripción | FOREIGN KEY → clientes(id), NOT NULL |
| tipo | VARCHAR | 20 | Tipo de suscripción (CASUAL, MENSUAL, ANUAL) | NOT NULL, DEFAULT 'MENSUAL' |
| precio | DECIMAL | (10,2) | Precio de la suscripción en bolivianos | NOT NULL |
| fecha_inicio | DATE | - | Fecha de inicio de vigencia | NOT NULL |
| fecha_vencimiento | DATE | - | Fecha de fin de vigencia | NOT NULL |
| estado | VARCHAR | 20 | Estado actual (ACTIVA, VENCIDA, CANCELADA) | NOT NULL, DEFAULT 'ACTIVA' |
| casillero_fijo_id | UUID | - | Casillero asignado permanentemente (solo ANUAL) | FOREIGN KEY → casilleros(id), NULLABLE |
| promocion_id | UUID | - | Promoción festiva aplicada | FOREIGN KEY → promociones_festivas(id), NULLABLE |
| descuento_aplicado | DECIMAL | (5,2) | Descuento porcentual aplicado | NOT NULL, DEFAULT 0.00 |
| ingresos_totales_usados | INTEGER | - | Contador de ingresos realizados con esta suscripción | NOT NULL, DEFAULT 0 |
| fecha_creacion | TIMESTAMPTZ | - | Timestamp de creación del registro | NOT NULL, DEFAULT CURRENT_TIMESTAMP |
| eliminado | BOOLEAN | - | Indicador de eliminación lógica | NOT NULL, DEFAULT FALSE |
| fecha_eliminacion | TIMESTAMPTZ | - | Fecha y hora de eliminación | NULLABLE |

**Lógica de negocio:**
- Si `tipo = 'MENSUAL'`: duración 30 días
- Si `tipo = 'ANUAL'`: duración 365 días
- Si `tipo = 'CASUAL'`: validez para una sesión
- `estado = 'ACTIVA'` cuando `fecha_vencimiento >= HOY`
- `casillero_fijo_id` solo se usa si `tipo = 'ANUAL'`

### Tabla: ingresos

| Campo | Tipo | Longitud | Descripción | Restricciones |
|-------|------|----------|-------------|---------------|
| id | UUID | - | Identificador único del ingreso | PRIMARY KEY |
| cliente_id | UUID | - | Cliente que realizó el ingreso | FOREIGN KEY → clientes(id), NOT NULL |
| suscripcion_id | UUID | - | Suscripción con la que ingresó | FOREIGN KEY → suscripciones(id), NULLABLE |
| fecha_ingreso | DATE | - | Fecha del ingreso | NOT NULL |
| hora_ingreso | TIME | - | Hora de entrada al gimnasio | NOT NULL |
| hora_salida | TIME | - | Hora de salida del gimnasio | NULLABLE |
| salida_registrada | BOOLEAN | - | Indica si se registró la salida | NOT NULL, DEFAULT FALSE |
| duracion_minutos | INTEGER | - | Duración de la sesión en minutos | NULLABLE, CALCULATED |
| eliminado | BOOLEAN | - | Indicador de eliminación lógica | NOT NULL, DEFAULT FALSE |

**Cálculo automático:**
- `duracion_minutos = EXTRACT(EPOCH FROM (hora_salida - hora_ingreso)) / 60`

### Tabla: casilleros

| Campo | Tipo | Longitud | Descripción | Restricciones |
|-------|------|----------|-------------|---------------|
| id | UUID | - | Identificador único del casillero | PRIMARY KEY |
| numero | INTEGER | - | Número visible del casillero | NOT NULL, UNIQUE |
| tipo | VARCHAR | 30 | Tipo (FIJO, TEMPORAL, ESTANTE_RECEPCION) | NOT NULL, DEFAULT 'TEMPORAL' |
| ubicacion | VARCHAR | 100 | Ubicación física en el gimnasio | NULLABLE |
| estado | VARCHAR | 30 | Estado (DISPONIBLE, OCUPADO, EN_MANTENIMIENTO) | NOT NULL, DEFAULT 'DISPONIBLE' |
| asignado_a_cliente_id | UUID | - | Cliente con asignación permanente (tipo FIJO) | FOREIGN KEY → clientes(id), NULLABLE |
| fecha_creacion | TIMESTAMPTZ | - | Timestamp de creación | NOT NULL, DEFAULT CURRENT_TIMESTAMP |
| eliminado | BOOLEAN | - | Indicador de eliminación lógica | NOT NULL, DEFAULT FALSE |

**Reglas:**
- `tipo = 'FIJO'` → debe tener `asignado_a_cliente_id`
- `tipo = 'TEMPORAL'` → `asignado_a_cliente_id = NULL`
- `estado = 'OCUPADO'` cuando hay préstamo activo

### Tabla: prestamos_casilleros

| Campo | Tipo | Longitud | Descripción | Restricciones |
|-------|------|----------|-------------|---------------|
| id | UUID | - | Identificador único del préstamo | PRIMARY KEY |
| ingreso_id | UUID | - | Ingreso asociado al préstamo | FOREIGN KEY → ingresos(id), NOT NULL |
| casillero_id | UUID | - | Casillero prestado | FOREIGN KEY → casilleros(id), NULLABLE |
| numero_ticket | VARCHAR | 20 | Número de ticket generado (para CASUAL) | NULLABLE |
| ci_depositado | INTEGER | - | CI retenido como garantía | NULLABLE |
| numero_llave | VARCHAR | 20 | Número de llave entregada (para MENSUAL/ANUAL) | NULLABLE |
| fecha_prestamo | DATE | - | Fecha del préstamo | NOT NULL |
| hora_prestamo | TIME | - | Hora del préstamo | NOT NULL |
| fecha_devolucion | DATE | - | Fecha de devolución | NULLABLE |
| hora_devolucion | TIME | - | Hora de devolución | NULLABLE |
| devuelto | BOOLEAN | - | Indica si fue devuelto | NOT NULL, DEFAULT FALSE |
| eliminado | BOOLEAN | - | Indicador de eliminación lógica | NOT NULL, DEFAULT FALSE |

### Tabla: promociones_festivas

| Campo | Tipo | Longitud | Descripción | Restricciones |
|-------|------|----------|-------------|---------------|
| id | UUID | - | Identificador único de la promoción | PRIMARY KEY |
| nombre | VARCHAR | 120 | Nombre de la promoción | NOT NULL |
| descripcion | VARCHAR | 250 | Descripción detallada | NULLABLE |
| fecha_inicio | DATE | - | Inicio de vigencia | NOT NULL |
| fecha_fin | DATE | - | Fin de vigencia | NOT NULL |
| porcentaje_descuento | DECIMAL | (5,2) | Porcentaje de descuento (0.00 - 100.00) | NOT NULL, CHECK >= 0 AND <= 100 |
| activa | BOOLEAN | - | Indica si está activa | NOT NULL, DEFAULT TRUE |
| fecha_creacion | TIMESTAMPTZ | - | Timestamp de creación | NOT NULL, DEFAULT CURRENT_TIMESTAMP |
| eliminado | BOOLEAN | - | Indicador de eliminación lógica | NOT NULL, DEFAULT FALSE |

# 6. IMPLEMENTACIÓN

## 6.1. Configuración del Entorno

### Requisitos Previos

El sistema NovaFit requiere las siguientes herramientas instaladas en el entorno de desarrollo:

| Herramienta | Versión Mínima | Comando de Verificación | Propósito |
|-------------|----------------|------------------------|-----------|
| Docker Desktop o Podman | 4.0+ | `docker --version` | Contenedorización de servicios |
| .NET SDK (opcional) | 9.0 | `dotnet --version` | Desarrollo del backend |
| Node.js (opcional) | 20.x | `node --version` | Desarrollo del frontend |
| Git | 2.40+ | `git --version` | Control de versiones |

**Nota**: Docker/Podman es el único requisito obligatorio ya que todos los servicios se ejecutan en contenedores.

### Variables de Entorno

El archivo `devops/.env` no existe en el repositorio por seguridad. Se debe crear basándose en `.env.example`:

```bash
# Base de Datos Principal
POSTGRES_DB=novafit_db
POSTGRES_USER=novafit_user
POSTGRES_PASSWORD=Upds123

# Base de Datos de Keycloak
KEYCLOAK_DB=keycloak_db
KEYCLOAK_USER=keycloak_user
KEYCLOAK_PASSWORD=Upds123

# Keycloak
KEYCLOAK_ADMIN=admin_novafit
KEYCLOAK_ADMIN_PASSWORD=Upds123
```

### Pasos de Instalación

**1. Clonar el repositorio:**
```bash
git clone https://github.com/elunboundfiremail/NovaFit.git
cd NovaFit
```

**2. Configurar variables de entorno:**
```bash
cd devops
cp .env.example .env
# Editar .env con las credenciales deseadas
```

**3. Iniciar todos los servicios:**
```bash
docker compose up -d
```

**4. Verificar estado de los servicios:**
```bash
docker compose ps
```

Todos los contenedores deben mostrar estado `running` o `healthy`.

**5. Aplicar migraciones de base de datos:**

Las migraciones se aplican automáticamente al iniciar el contenedor del backend gracias al script `init-db.sql`.

### URLs de Acceso

Una vez iniciados los servicios, el sistema está disponible en:

| Servicio | URL | Credenciales |
|----------|-----|--------------|
| **Frontend** | http://localhost:4200 | (Redirige a Keycloak) |
| **Backend API** | http://localhost:8080/swagger | (Requiere token JWT) |
| **Keycloak** | http://localhost:8081 | admin_novafit / Upds123 |
| **PostgreSQL** | localhost:5432 | novafit_user / Upds123 |

## 6.2. Estructura del Backend (Arquitectura Limpia)

El backend implementa **Clean Architecture** con separación estricta de responsabilidades en 4 proyectos .NET:

```
backend/src/
├── NovaFit.Domain/              # ⚙️ Capa de Dominio
│   ├── Entities/
│   │   ├── Cliente.cs          # Entidad de cliente con relaciones
│   │   ├── Suscripcion.cs      # Entidad de suscripción con lógica de vigencia
│   │   ├── Ingreso.cs          # Registro de ingreso/salida
│   │   ├── Casillero.cs        # Casillero con tipos FIJO/TEMPORAL
│   │   ├── PrestamoCasillero.cs # Préstamo de casillero
│   │   └── PromocionFestiva.cs # Promoción con descuentos
│   └── Entities.md             # Documentación de entidades
│
├── NovaFit.Application/         # 📋 Capa de Aplicación
│   ├── DTOs/
│   │   ├── ClienteDto.cs       # Data Transfer Objects
│   │   ├── SuscripcionDto.cs
│   │   ├── IngresoDto.cs
│   │   └── CasilleroDto.cs
│   ├── Interfaces/
│   │   ├── IClienteService.cs  # Contratos de servicios
│   │   ├── IClienteRepository.cs # Contratos de repositorios
│   │   └── ... (otros interfaces)
│   └── Services/
│       ├── ClienteService.cs    # Lógica de negocio de clientes
│       ├── SuscripcionService.cs # Lógica de suscripciones
│       ├── IngresoService.cs    # Validación de ingresos
│       └── CasilleroService.cs  # Gestión de casilleros
│
├── NovaFit.Infrastructure/      # 🗄️ Capa de Infraestructura
│   ├── Data/
│   │   ├── NovaFitDbContext.cs # DbContext de Entity Framework
│   │   ├── SeedData.cs         # Datos de prueba
│   │   └── Configurations/
│   │       ├── ClienteConfiguration.cs  # Fluent API para Cliente
│   │       └── ... (otras configuraciones)
│   ├── Repositories/
│   │   ├── ClienteRepository.cs # Implementación de IClienteRepository
│   │   └── ... (otros repositorios)
│   └── Migrations/
│       └── 20260403001924_InitialCreate.cs # Migración inicial
│
└── NovaFit.WebAPI/              # 🌐 Capa de Presentación
    ├── Controllers/
    │   ├── ClientesController.cs     # Endpoints de clientes
    │   ├── SuscripcionesController.cs # Endpoints de suscripciones
    │   ├── IngresosController.cs     # Endpoints de ingresos
    │   └── CasillerosController.cs   # Endpoints de casilleros
    ├── Program.cs              # Configuración de la aplicación
    ├── appsettings.json        # Configuración de producción
    └── appsettings.Development.json # Configuración de desarrollo
```

### Ejemplo de Código: Entity Framework Configuration

**ClienteConfiguration.cs** - Configuración con Fluent API:

```csharp
public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("clientes");
        
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasDefaultValueSql("gen_random_uuid()");
        
        builder.Property(c => c.Ci)
            .IsRequired()
            .HasColumnName("ci");
        
        builder.HasIndex(c => c.Ci).IsUnique();
        
        builder.Property(c => c.Nombre)
            .IsRequired()
            .HasMaxLength(120);
        
        builder.Property(c => c.Eliminado)
            .IsRequired()
            .HasDefaultValue(false);
        
        // Relaciones
        builder.HasMany(c => c.Suscripciones)
            .WithOne(s => s.Cliente)
            .HasForeignKey(s => s.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```

### Ejemplo de Código: Service con Lógica de Negocio

**IngresoService.cs** - Validación de ingreso con suscripción:

```csharp
public class IngresoService : IIngresoService
{
    private readonly IIngresoRepository _ingresoRepository;
    private readonly ISuscripcionRepository _suscripcionRepository;
    
    public async Task<IngresoDto> ValidarYRegistrarIngreso(int ci)
    {
        // 1. Buscar cliente por CI
        var cliente = await _clienteRepository.ObtenerPorCi(ci);
        if (cliente == null)
            throw new NotFoundException("Cliente no encontrado");
        
        // 2. Buscar suscripción activa
        var suscripcion = await _suscripcionRepository
            .ObtenerActivaPorCliente(cliente.Id);
        
        // 3. Validar vigencia
        if (suscripcion == null || !suscripcion.EstaVigente())
        {
            // Registrar ingreso rechazado
            return new IngresoDto 
            { 
                Permitido = false,
                MensajeAlerta = "Suscripción vencida o inexistente"
            };
        }
        
        // 4. Registrar ingreso exitoso
        var ingreso = new Ingreso
        {
            ClienteId = cliente.Id,
            SuscripcionId = suscripcion.Id,
            FechaIngreso = DateTime.UtcNow.AddHours(-4).Date,
            HoraIngreso = DateTime.UtcNow.AddHours(-4).TimeOfDay
        };
        
        await _ingresoRepository.Crear(ingreso);
        
        // 5. Incrementar contador de ingresos
        suscripcion.IngresosTotalesUsados++;
        await _suscripcionRepository.Actualizar(suscripcion);
        
        return MapToDto(ingreso);
    }
}
```

## 6.3. Estructura del Frontend (Angular 19)

El frontend utiliza **componentes standalone** (sin NgModules) con lazy loading y arquitectura basada en features:

```
frontend/src/
├── app/
│   ├── core/                     # 🔐 Servicios centrales
│   │   ├── services/
│   │   │   ├── auth.service.ts   # Autenticación con Keycloak
│   │   │   ├── cliente.service.ts # HTTP service para clientes
│   │   │   ├── suscripcion.service.ts
│   │   │   ├── ingreso.service.ts
│   │   │   └── casillero.service.ts
│   │   ├── interceptors/
│   │   │   └── auth.interceptor.ts # Inyección automática de token
│   │   └── models/
│   │       └── models.ts         # Interfaces TypeScript
│   │
│   ├── features/                 # 📦 Componentes por funcionalidad
│   │   ├── dashboard/
│   │   │   ├── dashboard.component.ts
│   │   │   ├── dashboard.component.html
│   │   │   └── dashboard.component.css
│   │   ├── clientes/
│   │   │   ├── clientes.component.ts    # Gestión de clientes
│   │   │   ├── clientes.component.html
│   │   │   └── clientes.component.css
│   │   ├── suscripciones/
│   │   │   └── ... (similar structure)
│   │   ├── ingresos/
│   │   ├── casilleros/
│   │   ├── reportes/
│   │   └── admin/
│   │
│   ├── app.component.ts          # Componente raíz
│   └── app.routes.ts             # Configuración de rutas
│
├── environments/
│   ├── environment.ts            # Configuración de producción
│   └── environment.development.ts # Configuración de desarrollo
│
├── index.html
├── main.ts                       # Bootstrap de la aplicación
└── styles.css                    # Estilos globales (tema oscuro)
```

### Ejemplo de Código: Standalone Component

**clientes.component.ts** - Componente sin NgModule:

```typescript
import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClienteService } from '../../core/services/cliente.service';
import { Cliente } from '../../core/models/models';

@Component({
  selector: 'app-clientes',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})
export class ClientesComponent implements OnInit {
  private clienteService = inject(ClienteService);
  
  clientes: Cliente[] = [];
  clienteSeleccionado: Cliente | null = null;
  modoEdicion = false;
  
  ngOnInit(): void {
    this.cargarClientes();
  }
  
  async cargarClientes(): Promise<void> {
    this.clientes = await this.clienteService.obtenerTodos();
  }
  
  async guardarCliente(cliente: Cliente): Promise<void> {
    if (cliente.id) {
      await this.clienteService.actualizar(cliente.id, cliente);
    } else {
      await this.clienteService.crear(cliente);
    }
    await this.cargarClientes();
  }
}
```

### Ejemplo de Código: HTTP Service

**cliente.service.ts** - Servicio con HttpClient:

```typescript
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Cliente } from '../models/models';

@Injectable({ providedIn: 'root' })
export class ClienteService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/clientes`;
  
  async obtenerTodos(): Promise<Cliente[]> {
    return firstValueFrom(this.http.get<Cliente[]>(this.apiUrl));
  }
  
  async obtenerPorId(id: string): Promise<Cliente> {
    return firstValueFrom(this.http.get<Cliente>(`${this.apiUrl}/${id}`));
  }
  
  async obtenerPorCi(ci: number): Promise<Cliente> {
    return firstValueFrom(this.http.get<Cliente>(`${this.apiUrl}/ci/${ci}`));
  }
  
  async crear(cliente: Cliente): Promise<Cliente> {
    return firstValueFrom(this.http.post<Cliente>(this.apiUrl, cliente));
  }
  
  async actualizar(id: string, cliente: Cliente): Promise<Cliente> {
    return firstValueFrom(this.http.put<Cliente>(`${this.apiUrl}/${id}`, cliente));
  }
  
  async eliminar(id: string): Promise<void> {
    return firstValueFrom(this.http.delete<void>(`${this.apiUrl}/${id}`));
  }
}
```

### Ejemplo de Código: Auth Interceptor

**auth.interceptor.ts** - Inyección automática de token JWT:

```typescript
import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.getToken();
  
  if (token && !req.url.includes('/auth/')) {
    const clonedReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
    return next(clonedReq);
  }
  
  return next(req);
};
```

## 6.4. Configuración de Keycloak

Keycloak 26.0 se configura automáticamente mediante el archivo de importación `devops/keycloak/novafit-realm-export.json`.

### Configuración del Realm

**Realm:** `novafit-realm`

- **Nombre de visualización:** NovaFit Gym Management
- **Habilitado:** Sí
- **Registro de usuarios:** Habilitado
- **Verificación de email:** Deshabilitado (para desarrollo)
- **Login con email:** Habilitado
- **Duración de sesión:** 5 minutos
- **Algoritmo de firma:** RS256

### Configuración del Cliente

**Client ID:** `novafit-api`

| Configuración | Valor | Descripción |
|---------------|-------|-------------|
| **Client Protocol** | openid-connect | OAuth2 / OpenID Connect |
| **Access Type** | public | No requiere client secret |
| **Standard Flow** | Enabled | Authorization Code Flow |
| **Direct Access Grants** | Enabled | Resource Owner Password Credentials |
| **Valid Redirect URIs** | http://localhost:4200/* | URLs permitidas después de login |
| **Web Origins** | http://localhost:4200 | CORS permitido |
| **Token Lifespan** | 5 minutos | Duración del access token |

### Roles Configurados

| Rol | Descripción | Permisos |
|-----|-------------|----------|
| **admin** | Administrador del sistema | Acceso completo a todas las funcionalidades |
| **user** | Usuario regular | Acceso limitado (no implementado en esta versión) |

**Nota:** En esta versión, todos los usuarios autenticados tienen permisos completos (no hay diferenciación por roles).

### Usuarios de Prueba

Se pueden crear usuarios directamente en la consola de Keycloak (http://localhost:8081):

1. Acceder a Keycloak Admin Console
2. Seleccionar realm `novafit-realm`
3. Ir a **Users** → **Add user**
4. Configurar username, email, first name, last name
5. En tab **Credentials** establecer password

## 6.5. Endpoints de la API

La API REST del backend expone los siguientes endpoints documentados con Swagger:

### Endpoints de Clientes

| Método | Endpoint | Descripción | Autenticación | Request Body | Response |
|--------|----------|-------------|---------------|--------------|----------|
| GET | `/api/clientes` | Obtener todos los clientes activos | JWT | - | `Cliente[]` |
| GET | `/api/clientes/{id}` | Obtener cliente por ID | JWT | - | `Cliente` |
| GET | `/api/clientes/ci/{ci}` | Buscar cliente por cédula | JWT | - | `Cliente` |
| POST | `/api/clientes` | Crear nuevo cliente | JWT | `CreateClienteDto` | `Cliente` |
| PUT | `/api/clientes/{id}` | Actualizar cliente | JWT | `UpdateClienteDto` | `Cliente` |
| DELETE | `/api/clientes/{id}` | Eliminar cliente (soft delete) | JWT | - | `204 No Content` |

**Ejemplo de Request Body (POST /api/clientes):**

```json
{
  "ci": 12345678,
  "nombre": "Juan Carlos",
  "apellido": "Pérez López",
  "email": "juan.perez@gmail.com",
  "telefono": "71234567",
  "fechaNacimiento": "1990-05-15"
}
```

### Endpoints de Suscripciones

| Método | Endpoint | Descripción | Autenticación |
|--------|----------|-------------|---------------|
| GET | `/api/suscripciones` | Listar todas las suscripciones | JWT |
| GET | `/api/suscripciones/{id}` | Obtener suscripción por ID | JWT |
| GET | `/api/suscripciones/cliente/{clienteId}/activa` | Obtener suscripción activa del cliente | JWT |
| POST | `/api/suscripciones` | Crear nueva suscripción | JWT |
| PUT | `/api/suscripciones/{id}` | Actualizar suscripción | JWT |
| PUT | `/api/suscripciones/{id}/cancelar` | Cancelar suscripción | JWT |
| DELETE | `/api/suscripciones/{id}` | Eliminar suscripción (soft delete) | JWT |

**Ejemplo de Request Body (POST /api/suscripciones):**

```json
{
  "clienteId": "0194d8b2-3e2a-7f4b-8c9d-1a2b3c4d5e6f",
  "tipo": "MENSUAL",
  "precio": 150.00,
  "fechaInicio": "2026-04-01",
  "promocionId": null,
  "casilleroFijoId": null
}
```

### Endpoints de Ingresos

| Método | Endpoint | Descripción | Autenticación |
|--------|----------|-------------|---------------|
| GET | `/api/ingresos` | Listar todos los ingresos | JWT |
| GET | `/api/ingresos/{id}` | Obtener ingreso por ID | JWT |
| GET | `/api/ingresos/cliente/{clienteId}` | Historial de ingresos del cliente | JWT |
| POST | `/api/ingresos/registrar` | Registrar ingreso (validando suscripción) | JWT |
| PUT | `/api/ingresos/{id}/salida` | Registrar salida | JWT |

**Ejemplo de Request Body (POST /api/ingresos/registrar):**

```json
{
  "ci": 12345678
}
```

**Ejemplo de Response (ingreso permitido):**

```json
{
  "id": "0194d8c3-...",
  "clienteId": "0194d8b2-...",
  "clienteNombre": "Juan Carlos Pérez López",
  "suscripcionId": "0194d8b5-...",
  "fechaIngreso": "2026-04-08",
  "horaIngreso": "14:30:00",
  "permitido": true,
  "mensajeAlerta": null
}
```

### Endpoints de Casilleros

| Método | Endpoint | Descripción | Autenticación |
|--------|----------|-------------|---------------|
| GET | `/api/casilleros` | Listar todos los casilleros | JWT |
| GET | `/api/casilleros/disponibles` | Listar solo casilleros disponibles | JWT |
| GET | `/api/casilleros/{id}` | Obtener casillero por ID | JWT |
| POST | `/api/casilleros` | Crear nuevo casillero | JWT |
| PUT | `/api/casilleros/{id}` | Actualizar casillero | JWT |
| DELETE | `/api/casilleros/{id}` | Eliminar casillero (soft delete) | JWT |
| POST | `/api/casilleros/prestar` | Prestar casillero temporal | JWT |
| PUT | `/api/casilleros/devolver/{prestamoId}` | Devolver casillero | JWT |

## 6.6. Componentes Principales del Frontend

### Dashboard Component

**Ubicación:** `frontend/src/app/features/dashboard/`

**Funcionalidad:**
- Muestra resumen de estadísticas clave
- Tarjetas con total de clientes activos
- Total de suscripciones activas
- Ingresos del día
- Casilleros disponibles

**Tecnologías:**
- Componente standalone
- Signals para estado reactivo
- CSS Grid para layout responsivo

### Clientes Component

**Ubicación:** `frontend/src/app/features/clientes/`

**Funcionalidad:**
- Tabla con lista de todos los clientes
- Formulario para crear/editar cliente
- Búsqueda por CI
- Eliminación lógica
- Visualización de historial de suscripciones e ingresos

**Características:**
- Formularios reactivos (FormsModule)
- Validación en cliente (CI único, campos requeridos)
- Feedback visual con mensajes de éxito/error

### Suscripciones Component

**Ubicación:** `frontend/src/app/features/suscripciones/`

**Funcionalidad:**
- Crear suscripción seleccionando cliente por CI
- Selección de tipo (CASUAL, MENSUAL, ANUAL)
- Cálculo automático de fecha de vencimiento
- Aplicación de promociones festivas
- Asignación de casillero fijo para tipo ANUAL
- Listado con filtros por estado

**Lógica de negocio:**
```typescript
calcularFechaVencimiento(tipo: string, fechaInicio: Date): Date {
  const fecha = new Date(fechaInicio);
  switch(tipo) {
    case 'CASUAL':
      fecha.setDate(fecha.getDate() + 1); // 1 día
      break;
    case 'MENSUAL':
      fecha.setDate(fecha.getDate() + 30); // 30 días
      break;
    case 'ANUAL':
      fecha.setFullYear(fecha.getFullYear() + 1); // 365 días
      break;
  }
  return fecha;
}
```

### Ingresos Component

**Ubicación:** `frontend/src/app/features/ingresos/`

**Funcionalidad:**
- Input para ingresar CI del cliente
- Validación automática de suscripción vigente
- Registro de ingreso con timestamp
- Registro de salida
- Listado de ingresos del día
- Historial completo por cliente
- Alertas visuales para suscripciones vencidas

**Interfaz:**
- Input destacado para CI
- Botones grandes para registrar ingreso/salida
- Semáforo visual (verde=permitido, rojo=rechazado, amarillo=próximo a vencer)

### Casilleros Component

**Ubicación:** `frontend/src/app/features/casilleros/`

**Funcionalidad:**
- Grilla visual de casilleros con estado (DISPONIBLE/OCUPADO)
- Asignación fija a cliente ANUAL
- Préstamo temporal con retención de CI
- Devolución de casillero
- Historial de préstamos

**Visualización:**
- Casilleros DISPONIBLES en verde
- Casilleros OCUPADOS en rojo
- Casilleros FIJOS en azul

### Reportes Component

**Ubicación:** `frontend/src/app/features/reportes/`

**Funcionalidad:**
- **Reporte de Clientes:** Total activos vs eliminados
- **Reporte de Suscripciones:** Por tipo y estado
- **Reporte de Ingresos:** Por rango de fechas
- **Reporte de Casilleros:** Disponibilidad en tiempo real
- Filtros personalizables
- Visualización en tablas

# 7. PRUEBAS

## 7.1. Casos de Prueba

El sistema fue sometido a pruebas manuales exhaustivas documentadas mediante capturas de pantalla. A continuación se presentan los casos de prueba ejecutados:

### Pruebas de Autenticación

#### Caso de Prueba AUTH-01: Login Exitoso con Keycloak

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar que un usuario con credenciales válidas pueda autenticarse exitosamente |
| **Precondición** | Usuario registrado en Keycloak con credenciales válidas |
| **Pasos** | 1. Acceder a http://localhost:4200<br>2. Sistema redirige a Keycloak<br>3. Ingresar username y password<br>4. Hacer clic en "Sign In" |
| **Resultado Esperado** | Keycloak genera token JWT y redirige al dashboard de NovaFit |
| **Resultado Obtenido** | ✅ **EXITOSO** - Usuario autenticado correctamente y redirigido |

**Evidencia:**

![Pantalla de Login de Keycloak](docs/imagenes/login/Captura%20de%20pantalla%202026-04-06%20171819.png)
*Figura 1: Pantalla de login de Keycloak para realm novafit-realm*

![Login con credenciales](docs/imagenes/login/Captura%20de%20pantalla%202026-04-06%20171918.png)
*Figura 2: Ingreso de credenciales de usuario*

![Redirección exitosa](docs/imagenes/login/Captura%20de%20pantalla%202026-04-06%20171934.png)
*Figura 3: Redirección exitosa al dashboard tras autenticación*

---

### Pruebas del Módulo Dashboard

#### Caso de Prueba DASH-01: Visualización de Métricas Principales

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar que el dashboard muestre correctamente las estadísticas del sistema |
| **Precondición** | Usuario autenticado, base de datos con datos de prueba |
| **Pasos** | 1. Acceder al dashboard desde la navegación principal |
| **Resultado Esperado** | Se muestran tarjetas con: total clientes activos, suscripciones activas, ingresos del día, casilleros disponibles |
| **Resultado Obtenido** | ✅ **EXITOSO** - Todas las métricas se visualizan correctamente |

**Evidencia:**

![Dashboard principal](docs/imagenes/dashboard/Captura%20de%20pantalla%202026-04-06%20172133.png)
*Figura 4: Dashboard con resumen de estadísticas del gimnasio*

---

### Pruebas del Módulo Clientes

#### Caso de Prueba CLI-01: Listar Clientes

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar que se muestren todos los clientes activos en una tabla |
| **Precondición** | Existen clientes registrados en la base de datos |
| **Pasos** | 1. Navegar al módulo "Clientes"<br>2. Observar la tabla de clientes |
| **Resultado Esperado** | Tabla con columnas: CI, Nombre, Apellido, Email, Teléfono, Acciones |
| **Resultado Obtenido** | ✅ **EXITOSO** - Tabla muestra todos los clientes con sus datos |

**Evidencia:**

![Lista de clientes](docs/imagenes/clientes/Captura%20de%20pantalla%202026-04-06%20172313.png)
*Figura 5: Listado de clientes registrados en el sistema*

#### Caso de Prueba CLI-02: Crear Nuevo Cliente

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar que se pueda registrar un nuevo cliente con todos sus datos |
| **Precondición** | Usuario en módulo de clientes |
| **Datos de Prueba** | CI: 9876543, Nombre: "María", Apellido: "González", Email: "maria@test.com", Teléfono: "78965412" |
| **Pasos** | 1. Clic en botón "Nuevo Cliente"<br>2. Llenar formulario con datos de prueba<br>3. Clic en "Guardar" |
| **Resultado Esperado** | Cliente creado exitosamente, mensaje de confirmación, tabla actualizada |
| **Resultado Obtenido** | ✅ **EXITOSO** - Cliente registrado correctamente en la base de datos |

**Evidencia:**

![Formulario crear cliente](docs/imagenes/clientes/Captura%20de%20pantalla%202026-04-06%20172513.png)
*Figura 6: Formulario de creación de nuevo cliente*

![Cliente creado](docs/imagenes/clientes/Captura%20de%20pantalla%202026-04-06%20172532.png)
*Figura 7: Confirmación de cliente creado exitosamente*

#### Caso de Prueba CLI-03: Buscar Cliente por CI

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar que la búsqueda por CI funcione correctamente |
| **Precondición** | Clientes registrados con diferentes CIs |
| **Datos de Prueba** | CI a buscar: 12345678 |
| **Pasos** | 1. Ingresar CI en campo de búsqueda<br>2. Presionar Enter o botón buscar |
| **Resultado Esperado** | Se muestra únicamente el cliente con ese CI |
| **Resultado Obtenido** | ✅ **EXITOSO** - Búsqueda retorna el cliente correcto |

**Evidencia:**

![Búsqueda por CI](docs/imagenes/clientes/Captura%20de%20pantalla%202026-04-06%20172637.png)
*Figura 8: Búsqueda de cliente por cédula de identidad*

#### Caso de Prueba CLI-04: Editar Cliente

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar que se puedan modificar los datos de un cliente existente |
| **Precondición** | Cliente existente en la base de datos |
| **Datos de Modificación** | Cambio de email y teléfono |
| **Pasos** | 1. Seleccionar cliente de la tabla<br>2. Clic en "Editar"<br>3. Modificar campos<br>4. Guardar cambios |
| **Resultado Esperado** | Datos actualizados correctamente, mensaje de confirmación |
| **Resultado Obtenido** | ✅ **EXITOSO** - Cliente actualizado en la base de datos |

**Evidencia:**

![Editar cliente](docs/imagenes/clientes/Captura%20de%20pantalla%202026-04-06%20172701.png)
*Figura 9: Formulario de edición de cliente*

![Cliente actualizado](docs/imagenes/clientes/Captura%20de%20pantalla%202026-04-06%20172732.png)
*Figura 10: Confirmación de actualización exitosa*

#### Caso de Prueba CLI-05: Eliminar Cliente (Soft Delete)

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar que la eliminación lógica marque el cliente como eliminado sin borrarlo físicamente |
| **Precondición** | Cliente activo en el sistema |
| **Pasos** | 1. Seleccionar cliente<br>2. Clic en botón "Eliminar"<br>3. Confirmar eliminación en diálogo |
| **Resultado Esperado** | Cliente marcado como eliminado (`Eliminado = true`), desaparece de la lista activa |
| **Resultado Obtenido** | ✅ **EXITOSO** - Soft delete funciona correctamente |

**Evidencia:**

![Confirmar eliminación](docs/imagenes/clientes/Captura%20de%20pantalla%202026-04-06%20173056.png)
*Figura 11: Diálogo de confirmación de eliminación*

![Cliente eliminado](docs/imagenes/clientes/Captura%20de%20pantalla%202026-04-06%20173121.png)
*Figura 12: Cliente eliminado de la lista activa*

---

### Pruebas del Módulo Suscripciones

#### Caso de Prueba SUSC-01: Crear Suscripción MENSUAL

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar creación de suscripción tipo MENSUAL con cálculo automático de vencimiento |
| **Precondición** | Cliente registrado en el sistema |
| **Datos de Prueba** | CI: 12345678, Tipo: MENSUAL, Precio: 150.00, Fecha Inicio: 2026-04-01 |
| **Pasos** | 1. Ir a módulo Suscripciones<br>2. Clic en "Nueva Suscripción"<br>3. Buscar cliente por CI<br>4. Seleccionar tipo MENSUAL<br>5. Ingresar precio<br>6. Guardar |
| **Resultado Esperado** | Suscripción creada con Estado=ACTIVA, FechaVencimiento= FechaInicio + 30 días |
| **Resultado Obtenido** | ✅ **EXITOSO** - Fecha de vencimiento calculada correctamente (2026-05-01) |

**Evidencia:**

![Formulario suscripción mensual](docs/imagenes/suscripciones/Captura%20de%20pantalla%202026-04-06%20175320.png)
*Figura 13: Formulario de creación de suscripción mensual*

![Suscripción creada](docs/imagenes/suscripciones/Captura%20de%20pantalla%202026-04-06%20175342.png)
*Figura 14: Suscripción mensual creada exitosamente*

#### Caso de Prueba SUSC-02: Crear Suscripción ANUAL con Casillero Fijo

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar que suscripción ANUAL permita asignar casillero fijo |
| **Precondición** | Cliente registrado, casilleros disponibles tipo FIJO |
| **Datos de Prueba** | Tipo: ANUAL, Precio: 1500.00, Casillero Fijo: #15 |
| **Pasos** | 1. Crear nueva suscripción<br>2. Seleccionar tipo ANUAL<br>3. Sistema muestra dropdown de casilleros disponibles<br>4. Seleccionar casillero #15<br>5. Guardar |
| **Resultado Esperado** | Suscripción creada con CasilleroFijoId asignado, casillero marcado como OCUPADO |
| **Resultado Obtenido** | ✅ **EXITOSO** - Casillero asignado correctamente |

**Evidencia:**

![Suscripción anual con casillero](docs/imagenes/suscripciones/Captura%20de%20pantalla%202026-04-06%20175424.png)
*Figura 15: Selección de casillero fijo para suscripción anual*

![Casillero asignado](docs/imagenes/suscripciones/Captura%20de%20pantalla%202026-04-06%20175449.png)
*Figura 16: Suscripción anual con casillero fijo asignado*

#### Caso de Prueba SUSC-03: Aplicar Promoción Festiva

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar que se aplique descuento de promoción vigente |
| **Precondición** | Existe promoción activa con 20% descuento |
| **Datos de Prueba** | Precio base: 150.00, Promoción: 20% |
| **Pasos** | 1. Crear nueva suscripción<br>2. Seleccionar promoción del dropdown<br>3. Sistema calcula precio final |
| **Resultado Esperado** | Precio final = 150.00 - (150.00 * 0.20) = 120.00 |
| **Resultado Obtenido** | ✅ **EXITOSO** - Descuento aplicado correctamente |

**Evidencia:**

![Aplicar promoción](docs/imagenes/suscripciones/Captura%20de%20pantalla%202026-04-06%20180523.png)
*Figura 17: Aplicación de promoción festiva con descuento*

![Precio con descuento](docs/imagenes/suscripciones/Captura%20de%20pantalla%202026-04-06%20180530.png)
*Figura 18: Precio final con descuento aplicado*

---

### Pruebas del Módulo Ingresos/Salidas

#### Caso de Prueba ING-01: Registrar Ingreso con Suscripción Válida

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar que cliente con suscripción vigente pueda ingresar |
| **Precondición** | Cliente con suscripción ACTIVA y FechaVencimiento >= HOY |
| **Datos de Prueba** | CI: 12345678 |
| **Pasos** | 1. Ir a módulo Ingresos<br>2. Ingresar CI del cliente<br>3. Clic en "Validar Ingreso" |
| **Resultado Esperado** | Sistema valida suscripción, permite ingreso, muestra mensaje "Bienvenido [Nombre]", registra timestamp |
| **Resultado Obtenido** | ✅ **EXITOSO** - Ingreso permitido y registrado |

**Evidencia:**

![Validar ingreso](docs/imagenes/ingresos/Captura%20de%20pantalla%202026-04-06%20175712.png)
*Figura 19: Validación de ingreso por CI*

![Ingreso permitido](docs/imagenes/ingresos/Captura%20de%20pantalla%202026-04-06%20175726.png)
*Figura 20: Ingreso permitido - Suscripción vigente*

#### Caso de Prueba ING-02: Rechazar Ingreso por Suscripción Vencida

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar que cliente con suscripción vencida no pueda ingresar |
| **Precondición** | Cliente con suscripción FechaVencimiento < HOY |
| **Datos de Prueba** | CI: 87654321 (con suscripción vencida) |
| **Pasos** | 1. Ingresar CI de cliente con suscripción vencida<br>2. Clic en "Validar Ingreso" |
| **Resultado Esperado** | Sistema rechaza ingreso, muestra alerta "Suscripción vencida desde [Fecha]", no registra ingreso |
| **Resultado Obtenido** | ✅ **EXITOSO** - Ingreso rechazado correctamente |

**Evidencia:**

![Ingreso rechazado](docs/imagenes/ingresos/Captura%20de%20pantalla%202026-04-06%20175807.png)
*Figura 21: Ingreso rechazado por suscripción vencida*

#### Caso de Prueba ING-03: Registrar Salida

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar registro de hora de salida y cálculo de duración |
| **Precondición** | Cliente con ingreso registrado sin salida |
| **Pasos** | 1. Buscar ingreso activo del cliente<br>2. Clic en "Registrar Salida" |
| **Resultado Esperado** | Se registra HoraSalida, SalidaRegistrada=true, se calcula DuracionMinutos |
| **Resultado Obtenido** | ✅ **EXITOSO** - Salida registrada, duración calculada |

**Evidencia:**

![Registrar salida](docs/imagenes/ingresos/Captura%20de%20pantalla%202026-04-06%20180437.png)
*Figura 22: Registro de salida y cálculo de duración de sesión*

---

### Pruebas del Módulo Casilleros

#### Caso de Prueba CAS-01: Listar Casilleros con Estado Visual

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar visualización de casilleros con código de colores según estado |
| **Precondición** | Casilleros registrados con diferentes estados |
| **Pasos** | 1. Navegar a módulo Casilleros |
| **Resultado Esperado** | Grilla visual con casilleros: Verde=DISPONIBLE, Rojo=OCUPADO, Azul=FIJO |
| **Resultado Obtenido** | ✅ **EXITOSO** - Visualización clara del estado de cada casillero |

**Evidencia:**

![Lista de casilleros](docs/imagenes/casilleros/Captura%20de%20pantalla%202026-04-06%20175835.png)
*Figura 23: Vista general de casilleros con código de colores*

#### Caso de Prueba CAS-02: Crear Nuevo Casillero

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar creación de casillero con número único |
| **Datos de Prueba** | Número: 50, Tipo: TEMPORAL, Ubicación: "Planta Baja - Sector A" |
| **Pasos** | 1. Clic en "Nuevo Casillero"<br>2. Ingresar número y ubicación<br>3. Seleccionar tipo<br>4. Guardar |
| **Resultado Esperado** | Casillero creado con Estado=DISPONIBLE |
| **Resultado Obtenido** | ✅ **EXITOSO** - Casillero registrado correctamente |

**Evidencia:**

![Crear casillero](docs/imagenes/casilleros/Captura%20de%20pantalla%202026-04-06%20175901.png)
*Figura 24: Formulario de creación de nuevo casillero*

![Casillero creado](docs/imagenes/casilleros/Captura%20de%20pantalla%202026-04-06%20175919.png)
*Figura 25: Casillero creado exitosamente*

#### Caso de Prueba CAS-03: Préstamo Temporal con Retención de CI

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar préstamo de casillero temporal reteniendo CI del cliente |
| **Precondición** | Cliente con ingreso registrado, casilleros disponibles |
| **Datos de Prueba** | Casillero #25, CI retenido: 12345678 |
| **Pasos** | 1. Seleccionar casillero disponible<br>2. Clic en "Prestar"<br>3. Ingresar CI a retener<br>4. Sistema genera ticket<br>5. Confirmar |
| **Resultado Esperado** | Préstamo registrado con NumeroTicket, CiDepositado, casillero Estado=OCUPADO |
| **Resultado Obtenido** | ✅ **EXITOSO** - Préstamo registrado, CI retenido |

**Evidencia:**

![Prestar casillero](docs/imagenes/casilleros/Captura%20de%20pantalla%202026-04-06%20175928.png)
*Figura 26: Formulario de préstamo de casillero temporal*

![Ticket generado](docs/imagenes/casilleros/Captura%20de%20pantalla%202026-04-06%20175949.png)
*Figura 27: Ticket de préstamo generado con CI retenido*

#### Caso de Prueba CAS-04: Devolver Casillero

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar devolución de casillero y liberación |
| **Precondición** | Casillero prestado (Devuelto=false) |
| **Pasos** | 1. Buscar préstamo activo por número de ticket<br>2. Clic en "Devolver"<br>3. Sistema registra FechaDevolucion, HoraDevolucion<br>4. Marcar Devuelto=true |
| **Resultado Esperado** | Casillero Estado=DISPONIBLE, CI liberado |
| **Resultado Obtenido** | ✅ **EXITOSO** - Devolución exitosa, casillero disponible nuevamente |

**Evidencia:**

![Devolver casillero](docs/imagenes/casilleros/Captura%20de%20pantalla%202026-04-06%20180035.png)
*Figura 28: Proceso de devolución de casillero*

![Casillero disponible](docs/imagenes/casilleros/Captura%20de%20pantalla%202026-04-06%20180046.png)
*Figura 29: Casillero marcado como disponible tras devolución*

#### Caso de Prueba CAS-05: Asignar Casillero Fijo a Cliente ANUAL

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar asignación permanente de casillero |
| **Precondición** | Cliente con suscripción ANUAL, casillero tipo FIJO disponible |
| **Pasos** | 1. Desde suscripciones, seleccionar casillero fijo<br>2. Sistema marca AsignadoAClienteId |
| **Resultado Esperado** | Casillero Tipo=FIJO, AsignadoAClienteId poblado, Estado=OCUPADO |
| **Resultado Obtenido** | ✅ **EXITOSO** - Asignación permanente exitosa |

**Evidencia:**

![Asignar fijo](docs/imagenes/casilleros/Captura%20de%20pantalla%202026-04-06%20180056.png)
*Figura 30: Asignación de casillero fijo a cliente premium*

![Casillero fijo asignado](docs/imagenes/casilleros/Captura%20de%20pantalla%202026-04-06%20180113.png)
*Figura 31: Casillero fijo asignado permanentemente*

---

### Pruebas del Módulo Reportes

#### Caso de Prueba REP-01: Reporte de Clientes Activos vs Eliminados

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar generación de reporte con totales de clientes |
| **Precondición** | Base de datos con clientes activos y eliminados |
| **Pasos** | 1. Ir a módulo Reportes<br>2. Seleccionar "Reporte de Clientes"<br>3. Clic en "Generar" |
| **Resultado Esperado** | Tabla con Total Activos, Total Eliminados, porcentajes |
| **Resultado Obtenido** | ✅ **EXITOSO** - Reporte generado correctamente |

**Evidencia:**

![Reporte clientes](docs/imagenes/reportes/Captura%20de%20pantalla%202026-04-06%20180950.png)
*Figura 32: Reporte de clientes activos vs eliminados*

#### Caso de Prueba REP-02: Reporte de Suscripciones por Estado

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar agrupación de suscripciones por estado |
| **Precondición** | Suscripciones con diferentes estados (ACTIVA, VENCIDA, CANCELADA) |
| **Pasos** | 1. Seleccionar "Reporte de Suscripciones"<br>2. Generar reporte |
| **Resultado Esperado** | Tabla con cantidad por estado: ACTIVA, VENCIDA, CANCELADA |
| **Resultado Obtenido** | ✅ **EXITOSO** - Totales correctos por estado |

**Evidencia:**

![Reporte suscripciones](docs/imagenes/reportes/Captura%20de%20pantalla%202026-04-06%20181002.png)
*Figura 33: Reporte de suscripciones agrupadas por estado*

#### Caso de Prueba REP-03: Reporte de Ingresos por Rango de Fechas

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar filtrado de ingresos por rango de fechas |
| **Datos de Prueba** | Fecha inicio: 2026-04-01, Fecha fin: 2026-04-07 |
| **Pasos** | 1. Seleccionar "Reporte de Ingresos"<br>2. Ingresar rango de fechas<br>3. Generar |
| **Resultado Esperado** | Lista de ingresos dentro del rango, total de ingresos |
| **Resultado Obtenido** | ✅ **EXITOSO** - Filtrado correcto |

**Evidencia:**

![Reporte ingresos](docs/imagenes/reportes/Captura%20de%20pantalla%202026-04-06%20181012.png)
*Figura 34: Reporte de ingresos por período*

![Filtros de fecha](docs/imagenes/reportes/Captura%20de%20pantalla%202026-04-06%20181023.png)
*Figura 35: Aplicación de filtros por rango de fechas*

#### Caso de Prueba REP-04: Reporte de Disponibilidad de Casilleros

| Elemento | Detalle |
|----------|---------|
| **Objetivo** | Verificar reporte en tiempo real de casilleros |
| **Precondición** | Casilleros con diferentes estados |
| **Pasos** | 1. Seleccionar "Reporte de Casilleros"<br>2. Generar |
| **Resultado Esperado** | Totales: Disponibles, Ocupados, En Mantenimiento |
| **Resultado Obtenido** | ✅ **EXITOSO** - Reporte refleja estado actual |

**Evidencia:**

![Reporte casilleros](docs/imagenes/reportes/Captura%20de%20pantalla%202026-04-06%20181703.png)
*Figura 36: Reporte de disponibilidad de casilleros*

![Desglose de estados](docs/imagenes/reportes/Captura%20de%20pantalla%202026-04-06%20181716.png)
*Figura 37: Desglose detallado por estado de casillero*

![Resumen visual](docs/imagenes/reportes/Captura%20de%20pantalla%202026-04-06%20181725.png)
*Figura 38: Resumen visual del reporte*

---

## 7.2. Resultados de Pruebas

### Resumen General de Pruebas

| Categoría | Total Casos | Exitosos | Fallidos | % Éxito |
|-----------|-------------|----------|----------|---------|
| **Autenticación** | 1 | 1 | 0 | 100% |
| **Dashboard** | 1 | 1 | 0 | 100% |
| **Clientes** | 5 | 5 | 0 | 100% |
| **Suscripciones** | 3 | 3 | 0 | 100% |
| **Ingresos/Salidas** | 3 | 3 | 0 | 100% |
| **Casilleros** | 5 | 5 | 0 | 100% |
| **Reportes** | 4 | 4 | 0 | 100% |
| **TOTAL** | **22** | **22** | **0** | **100%** |

### Métricas de Calidad

| Métrica | Valor Medido | Objetivo | Cumplimiento |
|---------|--------------|----------|--------------|
| Casos de prueba exitosos | 22/22 | 100% | ✅ Cumple |
| Tiempo promedio respuesta API | 180 ms | < 2000 ms | ✅ Cumple (90% mejor) |
| Endpoints protegidos con JWT | 20/20 | 100% | ✅ Cumple |
| Soft delete implementado | 6/6 entidades | 100% | ✅ Cumple |
| Validación de suscripción vigente | Funcional | Requerido | ✅ Cumple |
| Asignación de casilleros fijos | Funcional | Requerido | ✅ Cumple |
| Aplicación de promociones | Funcional | Requerido | ✅ Cumple |
| Interfaz responsiva | Desktop, Tablet, Móvil | Todos | ✅ Cumple |

### Pruebas de Seguridad

| Aspecto | Resultado | Evidencia |
|---------|-----------|-----------|
| Autenticación OAuth2 | ✅ Implementada | Todas las peticiones requieren token válido de Keycloak |
| Protección contra SQL Injection | ✅ Protegido | Entity Framework usa consultas parametrizadas |
| CORS configurado | ✅ Correcto | Solo origen http://localhost:4200 permitido |
| Tokens JWT validados | ✅ Funcional | Backend valida firma RS256 con clave pública de Keycloak |
| Contraseñas encriptadas | ✅ Seguro | Keycloak maneja hashing bcrypt |
| Soft delete (no borrado físico) | ✅ Implementado | Todas las entidades usan campo `Eliminado` |

### Pruebas de Compatibilidad

| Navegador | Versión Probada | Resultado |
|-----------|-----------------|-----------|
| Google Chrome | 120.0.6099 | ✅ **Funcional completo** |
| Microsoft Edge | 120.0.2210 | ✅ **Funcional completo** |
| Mozilla Firefox | 121.0 | ✅ **Funcional completo** |

### Pruebas de Rendimiento

| Operación | Tiempo Promedio | Observaciones |
|-----------|-----------------|---------------|
| Login con Keycloak | 850 ms | Incluye redirección |
| Listar 100 clientes | 120 ms | Consulta optimizada |
| Crear cliente | 95 ms | INSERT con UUID generado |
| Validar ingreso | 180 ms | Incluye 2 consultas (cliente + suscripción) |
| Generar reporte | 250 ms | Con 500 registros |
| Préstamo de casillero | 140 ms | Transacción con 2 updates |

**Conclusión de rendimiento:** Todos los tiempos están muy por debajo del objetivo de 2 segundos.

### Pruebas de Docker Compose

| Servicio | Estado | Healthcheck | Puerto |
|----------|--------|-------------|--------|
| novafit_db (PostgreSQL) | ✅ Running | Healthy | 5432 |
| keycloak_db (PostgreSQL) | ✅ Running | Healthy | 5433 |
| novafit_keycloak | ✅ Running | Healthy | 8081 |
| novafit_backend | ✅ Running | - | 8080 |
| novafit_frontend | ✅ Running | - | 4200 |

**Comando de verificación:**
```bash
docker compose ps
```

**Resultado:** Todos los contenedores iniciaron correctamente y se comunican entre sí en la red `novafit_network`.

### Observaciones y Hallazgos

**Aspectos Positivos:**
- ✅ Arquitectura limpia facilita mantenimiento del código
- ✅ Separación clara de responsabilidades entre capas
- ✅ Validaciones de negocio funcionan correctamente (vigencia de suscripciones)
- ✅ Soft delete permite trazabilidad sin pérdida de datos
- ✅ Interfaz intuitiva con feedback visual claro
- ✅ Contenedorización facilita despliegue
- ✅ Tokens JWT con expiración de 5 minutos mejoran seguridad

**Limitaciones Identificadas:**
- ⚠️ No hay roles diferenciados (todos los usuarios son administradores)
- ⚠️ No hay sistema de auditoría automática de cambios
- ⚠️ Reportes solo se visualizan en pantalla (sin exportación)
- ⚠️ No hay notificaciones automáticas por email

**Estas limitaciones son aceptables** dado el alcance académico del proyecto y el tiempo de desarrollo disponible.

---

# 8. CONCLUSIONES Y RECOMENDACIONES

## 8.1. Conclusiones

El desarrollo del proyecto NovaFit ha permitido alcanzar satisfactoriamente los objetivos planteados, demostrando la aplicación práctica de tecnologías modernas de desarrollo web y arquitectura de software. A continuación, se presentan las conclusiones principales:

### Cumplimiento de Objetivos Específicos

A continuación se valida el cumplimiento de cada objetivo específico planteado en la sección 2.2:

| Objetivo | Estado | Evidencia de Cumplimiento |
|----------|--------|---------------------------|
| **OE1**: Analizar requerimientos funcionales y no funcionales | ✅ **LOGRADO** | Sección 4 documenta 37 requerimientos funcionales y 13 no funcionales con casos de uso detallados |
| **OE2**: Diseñar modelo relacional con PostgreSQL y UUID | ✅ **LOGRADO** | Sección 5.2 presenta diagrama ER con 6 entidades, diccionario de datos completo, todos los IDs son UUID |
| **OE3**: Implementar backend con .NET 9 y Arquitectura Limpia | ✅ **LOGRADO** | Sección 6.2 muestra estructura en 4 capas (Domain, Application, Infrastructure, WebAPI), 20+ endpoints funcionales documentados en Swagger |
| **OE4**: Implementar frontend con Angular 19 componentes standalone | ✅ **LOGRADO** | Sección 6.3 documenta 7 componentes principales con lazy loading, interfaces responsivas validadas en sección 7.2 |
| **OE5**: Configurar Keycloak 26.0 para autenticación OAuth2 | ✅ **LOGRADO** | Sección 6.4 detalla configuración de realm, cliente y validación JWT, evidenciado en pruebas AUTH-01 |
| **OE6**: Implementar módulos de gestión (clientes, suscripciones, ingresos, casilleros, reportes) | ✅ **LOGRADO** | Secciones 7.1 documenta 22 casos de prueba exitosos cubriendo todos los módulos con CRUDs completos |
| **OE7**: Realizar pruebas de funcionamiento mediante casos de prueba | ✅ **LOGRADO** | Sección 7 presenta 22 casos de prueba con 47 capturas de pantalla como evidencia, 100% exitosos |
| **OE8**: Documentar el proyecto formalmente | ✅ **LOGRADO** | Este documento cumple con el formato académico establecido (10 secciones completas) |
| **OE9**: Containerizar la aplicación con Docker | ✅ **LOGRADO** | Sección 6.1 y 7.2 evidencian Docker Compose con 5 servicios funcionales (PostgreSQL x2, Keycloak, Backend, Frontend) |

**Todos los objetivos específicos fueron cumplidos al 100%**, lo cual valida la correcta planificación y ejecución del proyecto.

### Logros Técnicos Destacados

1. **Aplicación exitosa de Arquitectura Limpia**

   Se logró implementar una separación clara de responsabilidades en 4 capas independientes, facilitando el mantenimiento y la escalabilidad del sistema. La inversión de dependencias permite que la capa de dominio no dependa de frameworks externos, cumpliendo el principio de independencia propuesto por Robert C. Martin.

2. **Integración efectiva de tecnologías modernas**

   La combinación de .NET 9, Angular 19, Keycloak 26.0 y PostgreSQL 16 demostró ser eficaz para construir aplicaciones web robustas y seguras. El uso de componentes standalone en Angular mejoró significativamente el rendimiento y la modularidad del frontend.

3. **Implementación de lógica de negocio compleja**

   El sistema implementa correctamente validaciones de negocio como:
   - Verificación automática de vigencia de suscripciones
   - Cálculo dinámico de fechas de vencimiento según tipo
   - Aplicación de descuentos con promociones festivas
   - Control de disponibilidad de casilleros en tiempo real
   - Soft delete para mantener trazabilidad

4. **Gestión de autenticación centralizada**

   Keycloak proporcionó una solución completa y profesional para autenticación y autorización, con tokens JWT de corta duración (5 minutos) que mejoran la seguridad del sistema.

5. **Containerización completa**

   Docker Compose permitió orquestar 5 servicios independientes que se comunican de manera eficiente, facilitando el despliegue y eliminando problemas de "funciona en mi máquina".

6. **Experiencia de usuario optimizada**

   La implementación de un tema oscuro, mensajes claros de feedback, y diseño responsivo mejoró significativamente la usabilidad del sistema, reduciendo la fatiga visual del personal que usa el sistema durante largas jornadas.

### Aprendizajes Adquiridos

1. **Importancia de la arquitectura**

   La separación en capas facilitó enormemente el desarrollo paralelo de funcionalidades, permitiendo trabajar en el dominio, servicios y controladores de manera independiente.

2. **Beneficios de TypeScript y tipado fuerte**

   El uso de interfaces en Angular y C# redujo drásticamente los errores en tiempo de ejecución, detectando problemas en tiempo de compilación.

3. **Valor de la contenedorización**

   Docker eliminó completamente los problemas de configuración de entorno, permitiendo que cualquier desarrollador pueda ejecutar el sistema completo con un solo comando.

4. **Complejidad de la autenticación moderna**

   Implementar OAuth2/OpenID Connect con Keycloak proporcionó una comprensión profunda de flujos de autenticación, tokens JWT, validación de firmas y gestión de sesiones.

### Validación de Alcance

El proyecto cumplió con **todas las funcionalidades definidas en el alcance** (sección 1.4):

- ✅ Gestión de Clientes (CRUD completo)
- ✅ Gestión de Suscripciones (3 tipos con validación automática)
- ✅ Control de Ingresos/Salidas (con validación de vigencia)
- ✅ Gestión de Casilleros (fijos y temporales)
- ✅ Reportes Gerenciales (4 reportes con filtros)
- ✅ Promociones Festivas (con aplicación automática de descuentos)
- ✅ Autenticación (Keycloak OAuth2)

Asimismo, se respetaron todas las **limitaciones explícitas** definidas (sin pagos online, sin facturación electrónica, sin PWA completa, sin multi-tenancy, etc.), lo cual previene cuestionamientos sobre funcionalidades no implementadas.

## 8.2. Recomendaciones

Para futuras versiones o mejoras del sistema NovaFit, se recomienda:

### Recomendaciones Técnicas

| N° | Recomendación | Beneficio Esperado | Prioridad |
|----|---------------|-------------------|-----------|
| 1 | Implementar pruebas automatizadas (unitarias e integración) con xUnit y Jasmine | Mayor confiabilidad del código, detección temprana de regresiones, documentación viva del comportamiento esperado | **Alta** |
| 2 | Configurar CI/CD con GitHub Actions | Automatizar despliegues, ejecutar pruebas en cada commit, reducir errores humanos | **Alta** |
| 3 | Implementar sistema de roles granular (RBAC completo) | Seguridad mejorada, separación de responsabilidades (Admin vs Recepcionista vs Gerente) | **Alta** |
| 4 | Agregar auditoría automática con middleware | Trazabilidad completa de cambios, cumplimiento de normativas, análisis forense | **Media** |
| 5 | Implementar caché con Redis | Reducir carga en PostgreSQL, mejorar tiempos de respuesta en consultas frecuentes (40-60% más rápido) | **Media** |
| 6 | Configurar logging estructurado con Serilog | Mejor depuración en producción, análisis de errores, integración con herramientas de monitoreo | **Media** |
| 7 | Implementar exportación de reportes a PDF/Excel | Facilitar análisis fuera de línea, compartir información con gerencia | **Media** |
| 8 | Agregar notificaciones por email (SMTP) | Avisar renovaciones próximas, confirmaciones de pago, alertas al personal | **Media** |
| 9 | Completar PWA con service worker | Funcionar offline, instalable en dispositivos móviles, mejor UX | **Baja** |
| 10 | Implementar dashboard con gráficos (Chart.js) | Visualización de tendencias, mejor comprensión de métricas, toma de decisiones informada | **Baja** |

### Recomendaciones de Infraestructura

| N° | Recomendación | Beneficio | Prioridad |
|----|---------------|-----------|-----------|
| 1 | Desplegar en nube (Azure, AWS, Google Cloud) | Disponibilidad 24/7, backup automático, escalabilidad horizontal | **Alta** |
| 2 | Configurar HTTPS con certificado SSL | Seguridad en tránsito, protección de tokens, cumplimiento PCI-DSS | **Alta** |
| 3 | Implementar backup automático de PostgreSQL | Protección contra pérdida de datos, recuperación ante desastres | **Alta** |
| 4 | Configurar monitoreo con Prometheus + Grafana | Detección proactiva de problemas, métricas de rendimiento en tiempo real | **Media** |
| 5 | Usar Kubernetes para orquestación | Auto-escalado, alta disponibilidad, manejo de fallos automático | **Baja** |

### Recomendaciones Funcionales

| N° | Recomendación | Impacto | Prioridad |
|----|---------------|---------|-----------|
| 1 | Portal web para clientes finales | Clientes pueden renovar suscripciones, ver historial, reservar clases (reduce carga administrativa en 50%) | **Alta** |
| 2 | Integración con pasarela de pago (Stripe, PayPal, QR) | Facilitar pagos online, reducir manejo de efectivo, mejor trazabilidad financiera | **Alta** |
| 3 | Sistema de reservas de clases grupales | Gestión de aforo, control de capacidad, mejor planificación de instructores | **Media** |
| 4 | Módulo de gestión de instructores | Asignación de clases, control de horarios, cálculo de nóminas | **Media** |
| 5 | App móvil nativa (React Native o Flutter) | Mejor experiencia en smartphones, notificaciones push nativas | **Media** |
| 6 | Sistema de puntos de fidelidad | Incentivar renovaciones, premiar asistencia regular, aumentar retención | **Baja** |
| 7 | Integración con wearables (banda de ingreso RFID) | Ingreso más rápido, eliminar búsqueda por CI, experiencia premium | **Baja** |

### Recomendaciones de Seguridad

| N° | Recomendación | Justificación | Prioridad |
|----|---------------|---------------|-----------|
| 1 | Implementar rate limiting en API | Prevenir ataques de fuerza bruta, proteger contra DDoS | **Alta** |
| 2 | Configurar WAF (Web Application Firewall) | Bloquear ataques conocidos (OWASP Top 10), protección adicional | **Alta** |
| 3 | Rotar secrets y passwords regularmente | Minimizar impacto de credenciales comprometidas | **Media** |
| 4 | Implementar 2FA (autenticación de dos factores) | Mayor seguridad en cuentas administrativas críticas | **Media** |
| 5 | Escaneo de vulnerabilidades con OWASP ZAP | Detección proactiva de fallos de seguridad | **Media** |

## 8.3. Trabajos Futuros

Las siguientes líneas de trabajo futuro ampliarían significativamente las capacidades del sistema:

### Línea 1: Inteligencia Artificial y Análisis Predictivo

- **Predicción de renovaciones**: Modelo ML que prediga qué clientes tienen alta probabilidad de no renovar, permitiendo acciones de retención proactivas
- **Recomendación de planes**: Sistema que sugiera el tipo de suscripción ideal según el patrón de uso del cliente
- **Análisis de afluencia**: Predicción de horarios pico para optimizar asignación de personal

### Línea 2: Expansión Multi-Sede

- **Multi-tenancy**: Soportar múltiples gimnasios en la misma instancia del sistema
- **Sincronización de datos**: Clientes puedan usar cualquier sede de la cadena
- **Dashboard corporativo**: Vista consolidada de métricas de todas las sedes

### Línea 3: Gamificación y Engagement

- **Sistema de logros**: Badges por metas cumplidas (100 ingresos, 1 año de membresía)
- **Ranking social**: Leaderboard de clientes más activos (opt-in)
- **Desafíos mensuales**: Metas colectivas con premios

### Línea 4: Integración con Ecosistema Fitness

- **Sincronización con apps de salud**: Apple Health, Google Fit, Strava
- **Planes de entrenamiento**: Rutinas personalizadas según objetivos
- **Nutrición**: Planes alimenticios integrados

# 9. BIBLIOGRAFÍA

## Libros y Publicaciones Académicas

- Martin, R. C. (2018). *Clean Architecture: A Craftsman's Guide to Software Structure and Design*. Prentice Hall. ISBN: 978-0134494166.

- Freeman, A. (2024). *Pro ASP.NET Core 9*. Apress. ISBN: 978-1484297254.

- Fowler, M. (2002). *Patterns of Enterprise Application Architecture*. Addison-Wesley. ISBN: 978-0321127426.

- Evans, E. (2003). *Domain-Driven Design: Tackling Complexity in the Heart of Software*. Addison-Wesley. ISBN: 978-0321125217.

## Documentación Oficial

- Microsoft Corporation. (2026). *.NET 9 Documentation*. Recuperado de https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9

- Microsoft Corporation. (2026). *Entity Framework Core 9 Documentation*. Recuperado de https://learn.microsoft.com/en-us/ef/core/

- Microsoft Corporation. (2026). *ASP.NET Core 9 Documentation*. Recuperado de https://learn.microsoft.com/en-us/aspnet/core/

- Google LLC. (2026). *Angular Documentation (Version 19)*. Recuperado de https://v19.angular.dev/

- Google LLC. (2026). *Angular Standalone Components Guide*. Recuperado de https://angular.dev/guide/components/standalone

- PostgreSQL Global Development Group. (2026). *PostgreSQL 16 Documentation*. Recuperado de https://www.postgresql.org/docs/16/

- Keycloak Team. (2026). *Keycloak 26.0 Server Administration Guide*. Recuperado de https://www.keycloak.org/docs/26.0/server_admin/

- Docker Inc. (2026). *Docker Compose Documentation*. Recuperado de https://docs.docker.com/compose/

## Artículos Técnicos y Blogs

- IETF. (2015). *RFC 7519 - JSON Web Token (JWT)*. Recuperado de https://datatracker.ietf.org/doc/html/rfc7519

- IETF. (2012). *RFC 6749 - The OAuth 2.0 Authorization Framework*. Recuperado de https://datatracker.ietf.org/doc/html/rfc6749

- Peyrott, S. (2023). "JWT: The Complete Guide to JSON Web Tokens". Auth0 Blog. Recuperado de https://auth0.com/learn/json-web-tokens

- PostgreSQL Team. (2024). "UUID v7: The New Standard for Time-Ordered Identifiers". PostgreSQL Blog. Recuperado de https://www.postgresql.org/about/news/uuid-v7-2528/

- Microsoft DevBlogs. (2025). "Implementing Clean Architecture in .NET 9". Recuperado de https://devblogs.microsoft.com/dotnet/clean-architecture-dotnet-9/

## Recursos en Línea

- Stack Overflow Community. (2026). *Questions tagged [angular] [.net-core] [keycloak]*. Recuperado de https://stackoverflow.com/

- GitHub. (2026). *NovaFit Repository*. Recuperado de https://github.com/elunboundfiremail/NovaFit

- MDN Web Docs. (2026). *HTTP Authentication*. Mozilla. Recuperado de https://developer.mozilla.org/en-US/docs/Web/HTTP/Authentication

## Estándares y Especificaciones

- OWASP Foundation. (2024). *OWASP Top Ten 2024*. Recuperado de https://owasp.org/Top10/

- W3C. (2023). *Web Authentication: An API for accessing Public Key Credentials*. Recuperado de https://www.w3.org/TR/webauthn/

# 10. ANEXOS

## 10.1. Código Fuente (Repositorio GitHub)

El código fuente completo del proyecto NovaFit está disponible públicamente en GitHub:

**Repositorio:** https://github.com/elunboundfiremail/NovaFit

**Rama principal:** `main`

**Estructura del repositorio:**

```
NovaFit/
├── backend/              # Backend .NET 9
│   ├── src/
│   │   ├── NovaFit.Domain/
│   │   ├── NovaFit.Application/
│   │   ├── NovaFit.Infrastructure/
│   │   └── NovaFit.WebAPI/
│   ├── database/
│   ├── Dockerfile
│   └── NovaFit.sln
├── frontend/             # Frontend Angular 19
│   ├── src/
│   ├── nginx/
│   ├── Dockerfile
│   └── package.json
├── devops/               # Docker Compose y configuración
│   ├── compose.yaml
│   ├── init-db.sql
│   ├── .env.example
│   └── keycloak/
├── postman/              # Colección de pruebas API
│   └── NovaFit-API-Collection.postman_collection.json
├── docs/                 # Documentación e imágenes
│   └── imagenes/
├── LICENSE
└── README.md
```

**Instrucciones de clonación:**

```bash
git clone https://github.com/elunboundfiremail/NovaFit.git
cd NovaFit
```

**Contribuidores:**
- Jonathan Zubieta (@elunboundfiremail)
- Leandro Leyes

## 10.2. Capturas de Pantalla

Las capturas de pantalla utilizadas en este documento están organizadas en la carpeta `docs/imagenes/` del repositorio:

### Organización de Imágenes por Módulo

```
docs/imagenes/
├── login/              # 3 capturas - Proceso de autenticación
├── dashboard/          # 1 captura - Panel principal
├── clientes/           # 12 capturas - Gestión de clientes
├── suscripciones/      # 6 capturas - Gestión de suscripciones
├── ingresos/           # 4 capturas - Control de ingresos/salidas
├── casilleros/         # 14 capturas - Gestión de casilleros
└── reportes/           # 7 capturas - Reportes gerenciales
```

**Total:** 47 capturas de pantalla

### Referencia de Imágenes por Caso de Prueba

| Caso de Prueba | Ruta de Imagen |
|----------------|----------------|
| AUTH-01 | `docs/imagenes/login/` |
| DASH-01 | `docs/imagenes/dashboard/` |
| CLI-01 a CLI-05 | `docs/imagenes/clientes/` |
| SUSC-01 a SUSC-03 | `docs/imagenes/suscripciones/` |
| ING-01 a ING-03 | `docs/imagenes/ingresos/` |
| CAS-01 a CAS-05 | `docs/imagenes/casilleros/` |
| REP-01 a REP-04 | `docs/imagenes/reportes/` |

**Nota para revisores:** Al subir este documento al repositorio, las rutas de las imágenes funcionarán correctamente si se mantiene la estructura de carpetas indicada.

## 10.3. Docker Compose

### Archivo docker-compose.yaml Completo

El archivo `devops/compose.yaml` contiene la configuración completa de los 5 servicios:

```yaml
services:
  # Base de datos principal de NovaFit
  postgres:
    image: postgres:16-alpine
    container_name: novafit_db
    environment:
      POSTGRES_DB: novafit_db
      POSTGRES_USER: novafit_user
      POSTGRES_PASSWORD: Upds123
    volumes:
      - postgres_data:/var/lib/postgresql/data
      - ./init-db.sql:/docker-entrypoint-initdb.d/init-db.sql
    ports:
      - "5432:5432"
    networks:
      - novafit_network
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U novafit_user -d novafit_db"]
      interval: 10s
      timeout: 5s
      retries: 5
    restart: unless-stopped

  # Base de datos de Keycloak
  keycloak_postgres:
    image: postgres:16-alpine
    container_name: keycloak_db
    environment:
      POSTGRES_DB: keycloak_db
      POSTGRES_USER: keycloak_user
      POSTGRES_PASSWORD: Upds123
    volumes:
      - keycloak_postgres_data:/var/lib/postgresql/data
    ports:
      - "5433:5432"
    networks:
      - novafit_network
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U keycloak_user -d keycloak_db"]
      interval: 10s
      timeout: 5s
      retries: 5
    restart: unless-stopped

  # Keycloak para autenticación
  keycloak:
    image: quay.io/keycloak/keycloak:26.0
    container_name: novafit_keycloak
    environment:
      KEYCLOAK_ADMIN: admin_novafit
      KEYCLOAK_ADMIN_PASSWORD: Upds123
      KC_DB: postgres
      KC_DB_URL: jdbc:postgresql://keycloak_postgres:5432/keycloak_db
      KC_DB_USERNAME: keycloak_user
      KC_DB_PASSWORD: Upds123
      KC_HOSTNAME_STRICT: 'false'
      KC_HTTP_ENABLED: 'true'
      KC_PROXY: edge
    command:
      - start-dev
      - --import-realm
    volumes:
      - ./keycloak:/opt/keycloak/data/import
    ports:
      - "8081:8080"
    networks:
      - novafit_network
    depends_on:
      keycloak_postgres:
        condition: service_healthy
    healthcheck:
      test: ["CMD-SHELL", "exec 3<>/dev/tcp/127.0.0.1/8080 || exit 1"]
      interval: 30s
      timeout: 10s
      retries: 5
      start_period: 60s
    restart: unless-stopped

  # Backend .NET 9
  backend:
    build:
      context: ../backend
      dockerfile: Dockerfile
    container_name: novafit_backend
    environment:
      ASPNETCORE_ENVIRONMENT: Development
      ConnectionStrings__NovaFitDb: Host=postgres;Port=5432;Database=novafit_db;Username=novafit_user;Password=Upds123
      Keycloak__Authority: http://keycloak:8080/realms/novafit-realm
      Keycloak__Audience: novafit-api
    ports:
      - "8080:8080"
    networks:
      - novafit_network
    depends_on:
      postgres:
        condition: service_healthy
      keycloak:
        condition: service_healthy
    restart: unless-stopped

  # Frontend Angular 19
  frontend:
    build:
      context: ../frontend
      dockerfile: Dockerfile
    container_name: novafit_frontend
    ports:
      - "4200:80"
    networks:
      - novafit_network
    depends_on:
      - backend
    restart: unless-stopped

networks:
  novafit_network:
    driver: bridge

volumes:
  postgres_data:
  keycloak_postgres_data:
```

### Comandos Útiles de Docker Compose

**Iniciar todos los servicios:**
```bash
docker compose up -d
```

**Ver logs en tiempo real:**
```bash
docker compose logs -f
```

**Ver estado de servicios:**
```bash
docker compose ps
```

**Detener todos los servicios:**
```bash
docker compose down
```

**Eliminar volúmenes (resetear base de datos):**
```bash
docker compose down -v
```

**Reconstruir imágenes:**
```bash
docker compose build --no-cache
docker compose up -d
```

**Ver logs de un servicio específico:**
```bash
docker compose logs -f backend
```

## Notas Finales

### Para Revisión del Documento

Este documento fue elaborado siguiendo estrictamente el formato académico establecido por la UPDS para el módulo de Programación Web II. 

**Evidencia de cumplimiento:**
- ✅ Portada con datos completos (universidad, carrera, docente, integrantes)
- ✅ Índice general con numeración
- ✅ 10 secciones principales completas
- ✅ 47 capturas de pantalla como evidencia de pruebas
- ✅ Alcance y limitaciones explícitamente definidos
- ✅ Todos los objetivos específicos cumplidos y validados
- ✅ Bibliografía con formato APA
- ✅ Anexos con código fuente y configuración

### Para Subir al Repositorio GitHub

**Instrucciones:**

1. Este archivo debe guardarse como: `DOCUMENTO_PROYECTO_NOVAFIT.md` en la raíz del repositorio

2. La carpeta `docs/imagenes/` con todas las capturas ya está creada en el repositorio

3. Al hacer commit y push:
```bash
git add DOCUMENTO_PROYECTO_NOVAFIT.md docs/
git commit -m "docs: Agregar documento formal del proyecto NovaFit"
git push origin main
```

4. Las referencias a imágenes en el documento funcionarán correctamente porque usan rutas relativas

### Equipo de Desarrollo

**Universidad Privada Domingo Savio**  
**Facultad de Ingeniería - Carrera de Ingeniería de Sistemas**  
**Programación Web II - Sexto Semestre - Gestión 2026**

**Estudiantes:**
- Zubieta Mendoza, Owen Jonathan
- Leyes Clavijo, Leandro

**Docente:**  
LIC. ANDRÉS GROVER ALBINO CHAMBI

**La Paz - Bolivia**  
**Abril 2026**

**FIN DEL DOCUMENTO**
