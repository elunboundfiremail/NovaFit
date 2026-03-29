-- Script de inicializacion para PostgreSQL
-- Base de datos: novafit_db
-- Usuario: novafit_user
-- Fecha: 29/03/2026

-- Crear usuario si no existe
DO
$$
BEGIN
    IF NOT EXISTS (SELECT FROM pg_catalog.pg_user WHERE usename = 'novafit_user') THEN
        CREATE USER novafit_user WITH PASSWORD 'Upds123';
    END IF;
END
$$;

-- Crear base de datos si no existe
SELECT 'CREATE DATABASE novafit_db OWNER novafit_user'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'novafit_db')\gexec

-- Conectar a la base de datos
\c novafit_db

-- Otorgar privilegios completos al usuario
GRANT ALL PRIVILEGES ON DATABASE novafit_db TO novafit_user;
GRANT ALL ON SCHEMA public TO novafit_user;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO novafit_user;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO novafit_user;

-- Configurar permisos por defecto para objetos futuros
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON TABLES TO novafit_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON SEQUENCES TO novafit_user;

-- Habilitar extension para UUIDs
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Mensaje de confirmacion
\echo 'Base de datos novafit_db inicializada correctamente'
\echo 'Usuario: novafit_user'
\echo 'Password: Upds123'
