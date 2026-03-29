-- NovaFit - Migracion inicial (PostgreSQL)
-- Enfoque: Code First de referencia para revisar modelo relacional

BEGIN;

CREATE EXTENSION IF NOT EXISTS pgcrypto;

DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'tipo_plan_membresia') THEN
        CREATE TYPE tipo_plan_membresia AS ENUM ('mensual', 'anual', 'sesion');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'estado_membresia') THEN
        CREATE TYPE estado_membresia AS ENUM ('activa', 'vencida', 'cancelada');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'tipo_cliente') THEN
        CREATE TYPE tipo_cliente AS ENUM ('nuevo', 'antiguo');
    END IF;
END $$;

CREATE TABLE IF NOT EXISTS clientes (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    ci VARCHAR(20) NOT NULL,
    nombres VARCHAR(120) NOT NULL,
    apellido_paterno VARCHAR(60) NOT NULL,
    apellido_materno VARCHAR(60),
    tipo_cliente tipo_cliente NOT NULL DEFAULT 'nuevo',
    fecha_registro TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    activo BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE UNIQUE INDEX IF NOT EXISTS ux_clientes_ci ON clientes (ci);
CREATE INDEX IF NOT EXISTS ix_clientes_apellidos ON clientes (apellido_paterno, apellido_materno);

CREATE TABLE IF NOT EXISTS membresias (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    cliente_id UUID NOT NULL,
    tipo_plan tipo_plan_membresia NOT NULL,
    costo NUMERIC(10,2) NOT NULL,
    fecha_inicio DATE NOT NULL,
    fecha_fin DATE NOT NULL,
    estado estado_membresia NOT NULL DEFAULT 'activa',
    observacion VARCHAR(250),
    creado_en TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT fk_membresias_clientes
        FOREIGN KEY (cliente_id) REFERENCES clientes (id) ON DELETE RESTRICT,
    CONSTRAINT ck_membresias_costo_positivo CHECK (costo > 0),
    CONSTRAINT ck_membresias_rango_fechas CHECK (fecha_fin >= fecha_inicio)
);

CREATE INDEX IF NOT EXISTS ix_membresias_cliente_fecha_fin ON membresias (cliente_id, fecha_fin DESC);
CREATE INDEX IF NOT EXISTS ix_membresias_estado ON membresias (estado);

CREATE TABLE IF NOT EXISTS ingresos (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    cliente_id UUID NOT NULL,
    fecha_hora_ingreso TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    permitido BOOLEAN NOT NULL,
    motivo_alerta VARCHAR(200),
    membresia_id UUID,
    creado_en TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT fk_ingresos_clientes
        FOREIGN KEY (cliente_id) REFERENCES clientes (id) ON DELETE RESTRICT,
    CONSTRAINT fk_ingresos_membresias
        FOREIGN KEY (membresia_id) REFERENCES membresias (id) ON DELETE SET NULL
);

CREATE INDEX IF NOT EXISTS ix_ingresos_cliente_fecha ON ingresos (cliente_id, fecha_hora_ingreso DESC);

CREATE TABLE IF NOT EXISTS casilleros (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    numero INTEGER NOT NULL,
    disponible BOOLEAN NOT NULL DEFAULT TRUE,
    activo BOOLEAN NOT NULL DEFAULT TRUE,
    creado_en TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT ck_casilleros_numero_positivo CHECK (numero > 0)
);

CREATE UNIQUE INDEX IF NOT EXISTS ux_casilleros_numero ON casilleros (numero);

CREATE TABLE IF NOT EXISTS prestamos_casilleros (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    casillero_id UUID NOT NULL,
    cliente_id UUID NOT NULL,
    documento_retenido VARCHAR(120) NOT NULL,
    fecha_hora_prestamo TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    fecha_hora_devolucion TIMESTAMPTZ,
    observacion VARCHAR(250),
    creado_en TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT fk_prestamos_casilleros
        FOREIGN KEY (casillero_id) REFERENCES casilleros (id) ON DELETE RESTRICT,
    CONSTRAINT fk_prestamos_clientes
        FOREIGN KEY (cliente_id) REFERENCES clientes (id) ON DELETE RESTRICT,
    CONSTRAINT ck_prestamos_devolucion_valida
        CHECK (fecha_hora_devolucion IS NULL OR fecha_hora_devolucion >= fecha_hora_prestamo)
);

CREATE INDEX IF NOT EXISTS ix_prestamos_cliente ON prestamos_casilleros (cliente_id, fecha_hora_prestamo DESC);
CREATE INDEX IF NOT EXISTS ix_prestamos_casillero ON prestamos_casilleros (casillero_id, fecha_hora_prestamo DESC);

CREATE UNIQUE INDEX IF NOT EXISTS ux_prestamo_activo_por_casillero
    ON prestamos_casilleros (casillero_id)
    WHERE fecha_hora_devolucion IS NULL;

CREATE TABLE IF NOT EXISTS promociones_festivas (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    nombre VARCHAR(120) NOT NULL,
    descripcion VARCHAR(250),
    fecha_inicio DATE NOT NULL,
    fecha_fin DATE NOT NULL,
    porcentaje_descuento NUMERIC(5,2) NOT NULL,
    activa BOOLEAN NOT NULL DEFAULT TRUE,
    creado_en TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT ck_promociones_rango_fechas CHECK (fecha_fin >= fecha_inicio),
    CONSTRAINT ck_promociones_descuento CHECK (porcentaje_descuento > 0 AND porcentaje_descuento <= 100)
);

CREATE INDEX IF NOT EXISTS ix_promociones_fechas ON promociones_festivas (fecha_inicio, fecha_fin);

COMMIT;
