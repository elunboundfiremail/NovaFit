# NovaFit - Sistema de Gestion para Gimnasio

Sistema web para administracion de gimnasio desarrollado con .NET Core 9

## Tecnologias

- **Backend:** .NET Core 9 (Clean Architecture)
- **Base de datos:** PostgreSQL 18
- **Autenticacion:** Keycloak
- **Contenedores:** Docker Compose

## Estructura del Proyecto

```
NovaFit/
├── backend/          - Codigo fuente .NET
│   ├── src/
│   │   ├── NovaFit.Domain/
│   │   ├── NovaFit.Application/
│   │   ├── NovaFit.Infrastructure/
│   │   └── NovaFit.WebAPI/
│   └── NovaFit.sln
└── devops/           - Archivos Docker
    ├── docker-compose.yml
    └── .env.example
```

## Requisitos Previos

- .NET 9 SDK
- Docker Desktop
- Postman (para pruebas)

## Instalacion

Instrucciones detalladas se completaran en proximos commits.

## Contribuidores

- elunboundfiremail (Dev1) - Infraestructura
- leo-aig (Dev2) - Features

## Proyecto Academico

Universidad Privada Domingo Savio (UPDS)  
Programacion Web 2 - 2026
