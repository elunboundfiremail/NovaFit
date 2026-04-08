--
-- PostgreSQL database dump
--

\restrict clxySOFSUaNwYaqxfvvKs0ha8FbSeXOgP2XzOoJqbJQp1MRnyYIDby3zt7KH1FN

-- Dumped from database version 16.13
-- Dumped by pg_dump version 16.13

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: uuid-ossp; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;


--
-- Name: EXTENSION "uuid-ossp"; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Suscripcions; Type: TABLE; Schema: public; Owner: novafit_user
--

CREATE TABLE public."Suscripcions" (
    "Id" uuid NOT NULL,
    "ClienteId" uuid NOT NULL,
    "Tipo" character varying(20) NOT NULL,
    "Precio" numeric(10,2) NOT NULL,
    "FechaInicio" timestamp with time zone NOT NULL,
    "FechaVencimiento" timestamp with time zone NOT NULL,
    "Estado" character varying(20) DEFAULT 'ACTIVA'::character varying NOT NULL,
    "CasilleroFijoId" uuid,
    "PromocionId" uuid,
    "DescuentoAplicado" numeric(5,2) DEFAULT 0.0 NOT NULL,
    "IngresosTotalesUsados" integer DEFAULT 0 NOT NULL,
    "FechaCreacion" timestamp with time zone NOT NULL,
    "Eliminado" boolean DEFAULT false NOT NULL,
    "FechaEliminacion" timestamp with time zone
);


ALTER TABLE public."Suscripcions" OWNER TO novafit_user;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: novafit_user
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO novafit_user;

--
-- Name: casilleros; Type: TABLE; Schema: public; Owner: novafit_user
--

CREATE TABLE public.casilleros (
    "Id" uuid NOT NULL,
    "Numero" integer NOT NULL,
    "Tipo" text NOT NULL,
    "Ubicacion" text,
    "Estado" text NOT NULL,
    "AsignadoAClienteId" uuid,
    "FechaCreacion" timestamp with time zone NOT NULL,
    "Eliminado" boolean DEFAULT true NOT NULL,
    "FechaEliminacion" timestamp with time zone
);


ALTER TABLE public.casilleros OWNER TO novafit_user;

--
-- Name: clientes; Type: TABLE; Schema: public; Owner: novafit_user
--

CREATE TABLE public.clientes (
    "Id" uuid NOT NULL,
    "Ci" integer NOT NULL,
    "Nombre" character varying(100) NOT NULL,
    "Apellido" character varying(100) NOT NULL,
    "Email" character varying(150),
    "Telefono" character varying(20),
    "FechaNacimiento" timestamp with time zone,
    "FechaRegistro" timestamp with time zone NOT NULL,
    "Eliminado" boolean NOT NULL,
    "FechaEliminacion" timestamp with time zone
);


ALTER TABLE public.clientes OWNER TO novafit_user;

--
-- Name: ingresos; Type: TABLE; Schema: public; Owner: novafit_user
--

CREATE TABLE public.ingresos (
    "Id" uuid NOT NULL,
    "ClienteId" uuid NOT NULL,
    "SuscripcionId" uuid,
    "FechaIngreso" timestamp with time zone NOT NULL,
    "HoraIngreso" interval NOT NULL,
    "HoraSalida" interval,
    "SalidaRegistrada" boolean NOT NULL,
    "DuracionMinutos" integer,
    "FechaCreacion" timestamp with time zone NOT NULL,
    "Eliminado" boolean NOT NULL,
    "FechaEliminacion" timestamp with time zone
);


ALTER TABLE public.ingresos OWNER TO novafit_user;

--
-- Name: prestamos_casilleros; Type: TABLE; Schema: public; Owner: novafit_user
--

CREATE TABLE public.prestamos_casilleros (
    "Id" uuid NOT NULL,
    "IngresoId" uuid NOT NULL,
    "CasilleroId" uuid NOT NULL,
    "NumeroTicket" text,
    "CiDepositado" integer,
    "NumeroLlave" text,
    "FechaPrestamo" timestamp with time zone NOT NULL,
    "HoraPrestamo" interval NOT NULL,
    "FechaDevolucion" timestamp with time zone,
    "HoraDevolucion" interval,
    "Devuelto" boolean NOT NULL,
    "FechaCreacion" timestamp with time zone NOT NULL,
    "Eliminado" boolean NOT NULL,
    "FechaEliminacion" timestamp with time zone,
    "ClienteId" uuid
);


ALTER TABLE public.prestamos_casilleros OWNER TO novafit_user;

--
-- Name: promociones_festivas; Type: TABLE; Schema: public; Owner: novafit_user
--

CREATE TABLE public.promociones_festivas (
    "Id" uuid NOT NULL,
    "Nombre" character varying(200) NOT NULL,
    "Descripcion" character varying(1000) NOT NULL,
    "PorcentajeDescuento" numeric(5,2) NOT NULL,
    "FechaInicio" timestamp with time zone NOT NULL,
    "FechaFin" timestamp with time zone NOT NULL,
    "Activa" boolean DEFAULT true NOT NULL,
    "VecesAplicada" integer DEFAULT 0 NOT NULL,
    "FechaCreacion" timestamp with time zone NOT NULL,
    "Eliminado" boolean NOT NULL,
    "FechaEliminacion" timestamp with time zone
);


ALTER TABLE public.promociones_festivas OWNER TO novafit_user;

--
-- Data for Name: Suscripcions; Type: TABLE DATA; Schema: public; Owner: novafit_user
--

COPY public."Suscripcions" ("Id", "ClienteId", "Tipo", "Precio", "FechaInicio", "FechaVencimiento", "Estado", "CasilleroFijoId", "PromocionId", "DescuentoAplicado", "IngresosTotalesUsados", "FechaCreacion", "Eliminado", "FechaEliminacion") FROM stdin;
b2914de6-e052-4f5c-93eb-ad49d0fe6c67	9644eaa6-5ac9-45c2-a680-03dc49ca2a25	casual	25.00	2026-04-04 21:02:51.874106+00	2026-04-05 21:02:51.874106+00	activa	\N	\N	0.00	0	2026-04-04 21:02:51.874106+00	f	\N
8040f522-0ed9-4cce-bc0c-e7e650104245	d8bfa446-76e0-4b07-8121-17f74ca521e4	anual	2000.00	2026-04-06 08:52:42.057221+00	2027-04-06 08:52:42.057221+00	activa	\N	\N	0.00	0	2026-04-06 08:52:42.057221+00	f	\N
f330b998-2778-4c18-8b4e-ad33bc221796	83b6d1cf-38d7-4b84-9e31-499ac38def47	mensual	200.00	2026-04-06 17:53:11.725723+00	2026-05-06 17:53:11.725723+00	cancelada	\N	\N	0.00	0	2026-04-06 17:53:11.725723+00	t	2026-04-06 18:05:26.338929+00
8b67472a-9a67-4d13-afc7-97949f920a2c	b43a7289-86b5-4d74-9702-7b586d2f9ad3	mensual	200.00	2026-04-08 02:25:37.553471+00	2026-05-08 02:25:37.553471+00	activa	\N	\N	0.00	0	2026-04-08 02:25:37.553471+00	f	\N
dab70924-32bb-43cf-b653-7a4918b61fbc	2ce81d5b-b370-4557-8daf-a00b4a25a5c8	mensual	200.00	2026-04-08 02:25:42.322441+00	2026-05-08 02:25:42.322441+00	activa	\N	\N	0.00	0	2026-04-08 02:25:42.322441+00	f	\N
84e81bd0-cabd-4a14-8677-77170e55564a	6f88ea2a-cdb6-40b1-a80c-2d19b56db68c	mensual	200.00	2026-04-08 02:25:47.17188+00	2026-05-08 02:25:47.17188+00	activa	\N	\N	0.00	0	2026-04-08 02:25:47.17188+00	f	\N
e7468e91-8f11-436f-9ebf-73a2c8427240	6f88ea2a-cdb6-40b1-a80c-2d19b56db68c	casual	25.00	2026-04-08 02:25:30.440231+00	2026-04-09 02:25:30.440231+00	cancelada	\N	\N	0.00	0	2026-04-08 02:25:30.440231+00	t	2026-04-08 02:25:53.066247+00
f43ba0bd-4f38-4b4e-b44d-8110c3021442	bfb4734f-51fc-49ea-8937-fd3dfa221203	anual	2000.00	2026-04-08 02:27:01.948658+00	2027-04-08 02:27:01.948658+00	activa	\N	\N	0.00	0	2026-04-08 02:27:01.948658+00	f	\N
5289fad2-fd81-4512-bf37-aa5809f27b29	41ca5338-4dcf-4307-a4e0-08dfcde03667	mensual	200.00	2026-04-08 02:27:21.981388+00	2026-05-08 02:27:21.981388+00	activa	\N	\N	0.00	0	2026-04-08 02:27:21.981388+00	f	\N
69adba89-848c-4068-9600-cb579ac6e8ed	0d3421d6-2127-4835-b031-a90e5cacb555	casual	25.00	2026-04-08 02:32:25.049148+00	2026-04-09 02:32:25.049148+00	activa	\N	\N	0.00	0	2026-04-08 02:32:25.049148+00	f	\N
ee97df79-228d-403e-819a-f9a2ffc6bd22	3855cf7b-c9cc-4471-b219-ff5daf7c1022	anual	2000.00	2026-04-08 02:32:37.98848+00	2027-04-08 02:32:37.98848+00	activa	\N	\N	0.00	0	2026-04-08 02:32:37.98848+00	f	\N
ce104aae-a437-4c42-9d05-53958742246c	89279fa9-f944-4ade-a1d1-3f133dbad1b3	mensual	200.00	2026-04-08 02:32:45.618524+00	2026-05-08 02:32:45.618524+00	activa	\N	\N	0.00	0	2026-04-08 02:32:45.618524+00	f	\N
0b518023-28dc-4376-b5a2-57589a7b17f1	0be039a7-d3e9-4c93-b993-426db81488f3	casual	25.00	2026-04-08 02:32:54.607888+00	2026-04-09 02:32:54.607888+00	activa	\N	\N	0.00	0	2026-04-08 02:32:54.607888+00	f	\N
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: novafit_user
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20260403001924_InitialCreate	9.0.0
\.


--
-- Data for Name: casilleros; Type: TABLE DATA; Schema: public; Owner: novafit_user
--

COPY public.casilleros ("Id", "Numero", "Tipo", "Ubicacion", "Estado", "AsignadoAClienteId", "FechaCreacion", "Eliminado", "FechaEliminacion") FROM stdin;
2feaccfd-1d22-459e-bc50-95698150dfcf	6	TEMPORAL	Planta Baja	DISPONIBLE	\N	2026-04-04 21:09:16.113558+00	f	\N
642a7a92-88ca-4b57-837e-140fca70de7f	4	FIJO	Planta Baja	DISPONIBLE	\N	2026-04-04 21:09:16.113558+00	f	\N
57cc6606-53c0-4a86-be55-c6d8683f4339	7	TEMPORAL	Planta Alta	DISPONIBLE	\N	2026-04-04 21:09:16.113558+00	f	\N
368ce794-b28c-429d-9cfa-f479adba7433	3	FIJO	Planta Baja	DISPONIBLE	\N	2026-04-04 21:09:16.113558+00	f	\N
3df69178-4c42-4c7f-9f48-321c54ebfa06	11	ESTANTE_RECEPCION	Planta Alta	DISPONIBLE	\N	2026-04-04 21:09:16.113558+00	f	\N
7670b04f-8233-4b72-b0a2-ec7822f38a76	13	FIJO	Planta Alta	MANTENIMIENTO	\N	2026-04-06 18:03:44.651045+00	t	2026-04-06 18:09:01.893956+00
3966ce5d-bd22-421c-8079-c1306b87826d	1	FIJO	Planta Baja	OCUPADO	\N	2026-04-04 21:09:16.113558+00	f	\N
93665246-d770-4a1c-91f9-2de9cb74e567	5	TEMPORAL	Planta Baja	OCUPADO	\N	2026-04-04 21:09:16.113558+00	f	\N
21166921-e0dc-4c5c-a902-384daa396a18	9	ESTANTE_RECEPCION	Planta Alta	OCUPADO	\N	2026-04-04 21:09:16.113558+00	f	\N
e95ab08c-7a0c-4d27-85a7-28a608a68053	8	TEMPORAL	Planta Alta	OCUPADO	\N	2026-04-04 21:09:16.113558+00	f	\N
427627a9-f78b-4308-adbb-1817af2424a9	10	ESTANTE_RECEPCION	Planta Alta	OCUPADO	\N	2026-04-04 21:09:16.113558+00	f	\N
2d325db5-5b96-4055-b481-32c083f50d21	12	ESTANTE_RECEPCION	Planta Alta	DISPONIBLE	\N	2026-04-04 21:09:16.113558+00	f	\N
c6e08087-0523-4664-926c-7b49dd046951	2	FIJO	Planta Baja	MANTENIMIENTO	\N	2026-04-04 21:09:16.113558+00	t	2026-04-05 21:15:34.798451+00
\.


--
-- Data for Name: clientes; Type: TABLE DATA; Schema: public; Owner: novafit_user
--

COPY public.clientes ("Id", "Ci", "Nombre", "Apellido", "Email", "Telefono", "FechaNacimiento", "FechaRegistro", "Eliminado", "FechaEliminacion") FROM stdin;
b43a7289-86b5-4d74-9702-7b586d2f9ad3	12345678	Juan	Pérez	juan@email.com	75123456	\N	2026-03-05 17:04:35.331266+00	f	\N
d8bfa446-76e0-4b07-8121-17f74ca521e4	87654321	María	González	maria@email.com	75654321	\N	2026-03-15 17:04:35.331266+00	f	\N
83b6d1cf-38d7-4b84-9e31-499ac38def47	75264226	Alejandro	Torres	Ale@gmail.com	987654320	2001-06-15 00:00:00+00	2026-04-06 17:30:50.303861+00	t	2026-04-06 18:08:11.138079+00
9644eaa6-5ac9-45c2-a680-03dc49ca2a25	9878329	Leandro	Leyes	leo@gmail.com	75264226	\N	2026-04-04 18:28:15.072116+00	f	\N
bfb4734f-51fc-49ea-8937-fd3dfa221203	1234567	Carlos	Mendoza	CarlosM@gmail.com	01234567	2002-12-04 00:00:00+00	2026-04-08 02:11:30.500086+00	f	\N
fe2d677d-3f2d-49a3-a381-aaff4d913c88	12839221	Santiago	Mamani	Santi@gmail.com	129839282	1999-09-24 00:00:00+00	2026-04-08 02:16:36.937074+00	f	\N
3855cf7b-c9cc-4471-b219-ff5daf7c1022	3219839	Guadalupe	Flores	Guada@gmail.com	12938943	2000-02-04 00:00:00+00	2026-04-08 02:17:38.315514+00	f	\N
89279fa9-f944-4ade-a1d1-3f133dbad1b3	3281932	Lorenzo	Castillo	Lo@gmail.com	192837465	2003-09-02 00:00:00+00	2026-04-08 02:18:35.60239+00	f	\N
0d3421d6-2127-4835-b031-a90e5cacb555	32183928	Maria	Zotes	mari@gamil.com	76263212	\N	2026-04-08 02:19:10.909084+00	f	\N
bcb17e5e-bff2-44b5-a169-008c2bf1e23d	9302818	Adriana	Rojas	Adri@gamil.com	1092832	2003-07-08 00:00:00+00	2026-04-08 02:20:24.832842+00	f	\N
0be039a7-d3e9-4c93-b993-426db81488f3	10239203	Rosa	Choque	Rosa@gmail.com	238918492	2002-03-07 00:00:00+00	2026-04-08 02:21:16.539515+00	f	\N
41ca5338-4dcf-4307-a4e0-08dfcde03667	29891223	Ariana	Zabaleta	Ariana@gmail.com	1298372938	\N	2026-04-08 02:22:28.545242+00	f	\N
2ce81d5b-b370-4557-8daf-a00b4a25a5c8	94392321	Gerardo	Jimenes	Gerardo@gmail.com	71627326	2007-01-31 00:00:00+00	2026-04-08 02:23:34.919344+00	f	\N
31dd1327-8acf-4322-9b5b-df3b17e86384	19283983	Angela	Lupa	Angy@gmail.com	192483928	\N	2026-04-08 02:24:29.700077+00	f	\N
6f88ea2a-cdb6-40b1-a80c-2d19b56db68c	123893	Eduardo	Villegas	edu@gmail.com	8172837	\N	2026-04-08 02:25:08.776098+00	f	\N
\.


--
-- Data for Name: ingresos; Type: TABLE DATA; Schema: public; Owner: novafit_user
--

COPY public.ingresos ("Id", "ClienteId", "SuscripcionId", "FechaIngreso", "HoraIngreso", "HoraSalida", "SalidaRegistrada", "DuracionMinutos", "FechaCreacion", "Eliminado", "FechaEliminacion") FROM stdin;
453bb8b8-fe54-487d-8321-a16a276e5186	d8bfa446-76e0-4b07-8121-17f74ca521e4	\N	2026-04-04 18:01:56.274002+00	18:01:56.274002	19:01:33.279755	t	59	2026-04-04 18:01:56.274002+00	f	\N
59185cfd-3345-4408-b42a-269bcecf9d6e	d8bfa446-76e0-4b07-8121-17f74ca521e4	\N	2026-04-04 18:03:36.735263+00	18:03:36.735263	19:01:38.430216	t	58	2026-04-04 18:03:36.735263+00	f	\N
6892f197-4651-4a33-ba13-fb5da3d356cd	d8bfa446-76e0-4b07-8121-17f74ca521e4	\N	2026-04-04 18:09:28.309278+00	18:09:28.309278	19:01:39.589541	t	52	2026-04-04 18:09:28.309278+00	f	\N
4889b695-9a90-439a-9502-f8effdac0adc	9644eaa6-5ac9-45c2-a680-03dc49ca2a25	\N	2026-04-04 21:14:35.873236+00	21:14:35.873236	21:27:33.602902	t	12	2026-04-04 21:14:35.873236+00	f	\N
7a367c1c-0763-4906-a14a-207512caf586	d8bfa446-76e0-4b07-8121-17f74ca521e4	\N	2026-04-06 08:53:27.115385+00	08:53:27.115385	08:55:19.431145	t	1	2026-04-06 08:53:27.115385+00	f	\N
c689f4c1-9b6a-4caa-a510-1b9a5555c044	9644eaa6-5ac9-45c2-a680-03dc49ca2a25	\N	2026-04-04 21:27:35.989597+00	21:27:35.989597	17:22:52.551436	t	0	2026-04-04 21:27:35.989597+00	f	\N
9cbaf9ed-bed5-4333-9de7-3467d80332ae	83b6d1cf-38d7-4b84-9e31-499ac38def47	\N	2026-04-06 17:33:33.352191+00	17:33:33.352191	17:33:34.551381	t	0	2026-04-06 17:33:33.352191+00	f	\N
c25a449d-9904-4086-bd77-f82677d62de9	83b6d1cf-38d7-4b84-9e31-499ac38def47	f330b998-2778-4c18-8b4e-ad33bc221796	2026-04-06 17:57:20.12486+00	17:57:20.12486	18:04:26.46944	t	7	2026-04-06 17:57:20.12486+00	f	\N
76894b90-5a9b-4efb-a310-56143419dcfb	6f88ea2a-cdb6-40b1-a80c-2d19b56db68c	84e81bd0-cabd-4a14-8677-77170e55564a	2026-04-08 02:33:19.977905+00	02:33:19.977905	\N	f	\N	2026-04-08 02:33:19.977905+00	f	\N
5216dcc3-c12c-4dad-9d3b-1deab77c4f11	d8bfa446-76e0-4b07-8121-17f74ca521e4	8040f522-0ed9-4cce-bc0c-e7e650104245	2026-04-08 02:33:27.004705+00	02:33:27.004705	\N	f	\N	2026-04-08 02:33:27.004705+00	f	\N
71f68963-a1a0-42c9-9f5a-dce43467a80a	2ce81d5b-b370-4557-8daf-a00b4a25a5c8	dab70924-32bb-43cf-b653-7a4918b61fbc	2026-04-08 02:33:30.461708+00	02:33:30.461708	\N	f	\N	2026-04-08 02:33:30.461708+00	f	\N
0af3e887-31ac-4a32-8cdd-2c9b50f14766	bfb4734f-51fc-49ea-8937-fd3dfa221203	f43ba0bd-4f38-4b4e-b44d-8110c3021442	2026-04-08 02:33:33.378506+00	02:33:33.378506	\N	f	\N	2026-04-08 02:33:33.378506+00	f	\N
95e841a3-cd28-4bf6-9732-4c996403f455	3855cf7b-c9cc-4471-b219-ff5daf7c1022	ee97df79-228d-403e-819a-f9a2ffc6bd22	2026-04-08 02:33:39.629548+00	02:33:39.629548	\N	f	\N	2026-04-08 02:33:39.629548+00	f	\N
0dc25cca-c066-4023-8e80-91b96b78736f	0d3421d6-2127-4835-b031-a90e5cacb555	69adba89-848c-4068-9600-cb579ac6e8ed	2026-04-08 02:33:58.82976+00	02:33:58.82976	\N	f	\N	2026-04-08 02:33:58.82976+00	f	\N
\.


--
-- Data for Name: prestamos_casilleros; Type: TABLE DATA; Schema: public; Owner: novafit_user
--

COPY public.prestamos_casilleros ("Id", "IngresoId", "CasilleroId", "NumeroTicket", "CiDepositado", "NumeroLlave", "FechaPrestamo", "HoraPrestamo", "FechaDevolucion", "HoraDevolucion", "Devuelto", "FechaCreacion", "Eliminado", "FechaEliminacion", "ClienteId") FROM stdin;
140038b4-f65b-4b90-a821-8f803d85a48c	4889b695-9a90-439a-9502-f8effdac0adc	3966ce5d-bd22-421c-8079-c1306b87826d	1	9878328	\N	2026-04-04 21:15:03.605146+00	21:15:03.605146	2026-04-04 21:27:57.870121+00	21:27:57.870121	t	2026-04-04 21:15:03.605146+00	f	\N	\N
dc3d0609-6475-44e2-a994-a254a9f39beb	c689f4c1-9b6a-4caa-a510-1b9a5555c044	3966ce5d-bd22-421c-8079-c1306b87826d	\N	\N	\N	2026-04-04 21:32:52.414577+00	21:32:52.414577	2026-04-04 21:33:14.486648+00	21:33:14.486648	t	2026-04-04 21:32:52.414577+00	f	\N	\N
29156504-90bd-4f32-bbee-80b239ea56b8	c689f4c1-9b6a-4caa-a510-1b9a5555c044	3966ce5d-bd22-421c-8079-c1306b87826d	1	9878328	\N	2026-04-04 23:54:27.161959+00	23:54:27.161959	2026-04-05 00:02:41.76441+00	00:02:41.76441	t	2026-04-04 23:54:27.161959+00	f	\N	\N
295c46cd-ddce-4392-8f6f-db152f657c5e	c689f4c1-9b6a-4caa-a510-1b9a5555c044	3966ce5d-bd22-421c-8079-c1306b87826d	11	9878328	\N	2026-04-05 20:45:57.784229+00	20:45:57.784229	2026-04-05 20:57:07.603509+00	20:57:07.603509	t	2026-04-05 20:45:57.784229+00	f	\N	\N
db2f6b2e-32ef-4b70-8b15-5609de8e142d	c689f4c1-9b6a-4caa-a510-1b9a5555c044	3df69178-4c42-4c7f-9f48-321c54ebfa06	1	9878328	\N	2026-04-05 20:45:44.899372+00	20:45:44.899372	2026-04-05 20:57:08.444003+00	20:57:08.444003	t	2026-04-05 20:45:44.899372+00	f	\N	\N
1488d559-a59c-4557-996f-8696603e8ed6	c689f4c1-9b6a-4caa-a510-1b9a5555c044	3966ce5d-bd22-421c-8079-c1306b87826d	1	9878328	\N	2026-04-05 21:03:36.08475+00	21:03:36.08475	2026-04-05 21:09:43.160021+00	21:09:43.160021	t	2026-04-05 21:03:36.08475+00	f	\N	\N
76869da7-0eb6-472c-8f3f-6f67f84bc205	c689f4c1-9b6a-4caa-a510-1b9a5555c044	3966ce5d-bd22-421c-8079-c1306b87826d	\N	0	\N	2026-04-05 21:10:41.693349+00	21:10:41.693349	2026-04-05 21:10:48.699891+00	21:10:48.699891	t	2026-04-05 21:10:41.693349+00	f	\N	\N
381e5621-bc24-4b1a-a1a9-3abdf7df03fb	c689f4c1-9b6a-4caa-a510-1b9a5555c044	3966ce5d-bd22-421c-8079-c1306b87826d	\N	9878328	1	2026-04-05 21:11:15.32353+00	21:11:15.32353	2026-04-05 21:24:50.95366+00	21:24:50.95366	t	2026-04-05 21:11:15.32353+00	f	\N	\N
dfbe02f7-4a12-4a17-88ac-abc5667b80de	c689f4c1-9b6a-4caa-a510-1b9a5555c044	3966ce5d-bd22-421c-8079-c1306b87826d	\N	\N	1	2026-04-05 21:33:06.805308+00	21:33:06.805308	2026-04-05 22:01:00.364567+00	22:01:00.364567	t	2026-04-05 21:33:06.805308+00	f	\N	\N
ec03aad0-7be6-44be-8554-f8e4dc410c39	c689f4c1-9b6a-4caa-a510-1b9a5555c044	e95ab08c-7a0c-4d27-85a7-28a608a68053	\N	9878328	8	2026-04-05 22:21:17.726059+00	22:21:17.726059	2026-04-05 22:21:38.343537+00	22:21:38.343537	t	2026-04-05 22:21:17.726059+00	f	\N	\N
829270c7-323c-43c7-b9d1-e7c7979b4f85	c689f4c1-9b6a-4caa-a510-1b9a5555c044	21166921-e0dc-4c5c-a902-384daa396a18	TK-05222747	\N	\N	2026-04-05 22:27:47.120238+00	22:27:47.120238	2026-04-05 22:38:15.920847+00	22:38:15.920847	t	2026-04-05 22:27:47.120238+00	f	\N	\N
5d4c148a-6b74-4995-875f-8e037c913b29	c689f4c1-9b6a-4caa-a510-1b9a5555c044	21166921-e0dc-4c5c-a902-384daa396a18	5222748	\N	\N	2026-04-05 22:38:59.037086+00	22:38:59.037086	2026-04-05 22:39:07.37546+00	22:39:07.37546	t	2026-04-05 22:38:59.037086+00	f	\N	\N
b87bc690-3e49-47de-a9cf-95eea85d306b	c689f4c1-9b6a-4caa-a510-1b9a5555c044	21166921-e0dc-4c5c-a902-384daa396a18	5222749	\N	\N	2026-04-05 23:07:22.99146+00	23:07:22.99146	2026-04-05 23:26:43.727592+00	23:26:43.727592	t	2026-04-05 23:07:22.99146+00	f	\N	\N
e738039c-63ab-4e23-b65d-8b036e909342	c689f4c1-9b6a-4caa-a510-1b9a5555c044	21166921-e0dc-4c5c-a902-384daa396a18	5222750	\N	\N	2026-04-05 23:26:48.499013+00	23:26:48.499013	2026-04-05 23:31:18.659348+00	23:31:18.659348	t	2026-04-05 23:26:48.499013+00	f	\N	\N
e2d195bc-baf1-4930-a3ea-537448ba8dca	c689f4c1-9b6a-4caa-a510-1b9a5555c044	21166921-e0dc-4c5c-a902-384daa396a18	8	\N	\N	2026-04-05 23:31:27.252873+00	23:31:27.252873	2026-04-05 23:31:30.660404+00	23:31:30.660404	t	2026-04-05 23:31:27.252873+00	f	\N	\N
c6b45744-376b-44fd-9043-a11001fd006e	c689f4c1-9b6a-4caa-a510-1b9a5555c044	368ce794-b28c-429d-9cfa-f479adba7433	\N	\N	3	2026-04-05 23:32:58.817531+00	23:32:58.817531	2026-04-05 23:33:00.197647+00	23:33:00.197647	t	2026-04-05 23:32:58.817531+00	f	\N	\N
0a6c087c-f013-4454-9490-84a388064483	c689f4c1-9b6a-4caa-a510-1b9a5555c044	3966ce5d-bd22-421c-8079-c1306b87826d	\N	\N	1	2026-04-05 23:32:54.060323+00	23:32:54.060323	2026-04-05 23:39:11.683242+00	23:39:11.683242	t	2026-04-05 23:32:54.060323+00	f	\N	\N
8def5508-8604-4411-8ca3-bf00f262ff7c	c689f4c1-9b6a-4caa-a510-1b9a5555c044	3966ce5d-bd22-421c-8079-c1306b87826d	\N	\N	1	2026-04-05 23:39:49.929536+00	23:39:49.929536	2026-04-05 23:40:39.105651+00	23:40:39.105651	t	2026-04-05 23:39:49.929536+00	f	\N	\N
88e1b038-1ede-471a-83d9-b7a48122adee	7a367c1c-0763-4906-a14a-207512caf586	3966ce5d-bd22-421c-8079-c1306b87826d	\N	0	1	2026-04-06 08:54:33.89847+00	08:54:33.89847	2026-04-06 08:55:12.35691+00	08:55:12.35691	t	2026-04-06 08:54:33.89847+00	f	\N	\N
eb214f78-d9ce-43e3-9e0f-91035a594384	c689f4c1-9b6a-4caa-a510-1b9a5555c044	21166921-e0dc-4c5c-a902-384daa396a18	1	\N	\N	2026-04-06 13:36:53.845099+00	13:36:53.845099	2026-04-06 13:36:56.220731+00	13:36:56.220731	t	2026-04-06 13:36:53.845099+00	f	\N	\N
9fd9d4c5-476c-4a4d-a9f8-65e414c8e3bc	c25a449d-9904-4086-bd77-f82677d62de9	3966ce5d-bd22-421c-8079-c1306b87826d	\N	75264226	1	2026-04-06 17:59:23.918633+00	17:59:23.918633	2026-04-06 18:00:21.124334+00	18:00:21.124334	t	2026-04-06 17:59:23.918633+00	f	\N	\N
35b7247c-2d77-406d-bd17-e34d453a8909	c25a449d-9904-4086-bd77-f82677d62de9	21166921-e0dc-4c5c-a902-384daa396a18	2	\N	\N	2026-04-06 18:00:38.842241+00	18:00:38.842241	2026-04-06 18:01:30.913495+00	18:01:30.913495	t	2026-04-06 18:00:38.842241+00	f	\N	\N
a05a57da-b365-4a9d-a873-48e19f95022a	95e841a3-cd28-4bf6-9732-4c996403f455	3966ce5d-bd22-421c-8079-c1306b87826d	\N	3219839	1	2026-04-08 02:34:41.027139+00	02:34:41.027139	\N	\N	f	2026-04-08 02:34:41.027139+00	f	\N	\N
ed51e883-1f79-4d16-a07c-23a96c0580ed	71f68963-a1a0-42c9-9f5a-dce43467a80a	93665246-d770-4a1c-91f9-2de9cb74e567	\N	94392321	5	2026-04-08 02:34:48.733814+00	02:34:48.733814	\N	\N	f	2026-04-08 02:34:48.733814+00	f	\N	\N
24f097b1-f2ef-4dfb-b524-4152519b84d3	5216dcc3-c12c-4dad-9d3b-1deab77c4f11	21166921-e0dc-4c5c-a902-384daa396a18	1	\N	\N	2026-04-08 02:34:59.383343+00	02:34:59.383343	\N	\N	f	2026-04-08 02:34:59.383343+00	f	\N	\N
aeb796f4-0903-45c4-a6e9-a4d15c755a37	0af3e887-31ac-4a32-8cdd-2c9b50f14766	e95ab08c-7a0c-4d27-85a7-28a608a68053	\N	1234567	8	2026-04-08 02:36:20.390506+00	02:36:20.390506	\N	\N	f	2026-04-08 02:36:20.390506+00	f	\N	\N
63abba51-ded8-4342-bced-bda2dcdc2b87	76894b90-5a9b-4efb-a310-56143419dcfb	427627a9-f78b-4308-adbb-1817af2424a9	2	\N	\N	2026-04-08 02:36:48.124535+00	02:36:48.124535	\N	\N	f	2026-04-08 02:36:48.124535+00	f	\N	\N
\.


--
-- Data for Name: promociones_festivas; Type: TABLE DATA; Schema: public; Owner: novafit_user
--

COPY public.promociones_festivas ("Id", "Nombre", "Descripcion", "PorcentajeDescuento", "FechaInicio", "FechaFin", "Activa", "VecesAplicada", "FechaCreacion", "Eliminado", "FechaEliminacion") FROM stdin;
\.


--
-- Name: Suscripcions PK_Suscripcions; Type: CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public."Suscripcions"
    ADD CONSTRAINT "PK_Suscripcions" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: casilleros PK_casilleros; Type: CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public.casilleros
    ADD CONSTRAINT "PK_casilleros" PRIMARY KEY ("Id");


--
-- Name: clientes PK_clientes; Type: CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public.clientes
    ADD CONSTRAINT "PK_clientes" PRIMARY KEY ("Id");


--
-- Name: ingresos PK_ingresos; Type: CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public.ingresos
    ADD CONSTRAINT "PK_ingresos" PRIMARY KEY ("Id");


--
-- Name: prestamos_casilleros PK_prestamos_casilleros; Type: CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public.prestamos_casilleros
    ADD CONSTRAINT "PK_prestamos_casilleros" PRIMARY KEY ("Id");


--
-- Name: promociones_festivas PK_promociones_festivas; Type: CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public.promociones_festivas
    ADD CONSTRAINT "PK_promociones_festivas" PRIMARY KEY ("Id");


--
-- Name: IX_Suscripcions_CasilleroFijoId; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE INDEX "IX_Suscripcions_CasilleroFijoId" ON public."Suscripcions" USING btree ("CasilleroFijoId");


--
-- Name: IX_Suscripcions_ClienteId; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE INDEX "IX_Suscripcions_ClienteId" ON public."Suscripcions" USING btree ("ClienteId");


--
-- Name: IX_Suscripcions_ClienteId_Estado; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE INDEX "IX_Suscripcions_ClienteId_Estado" ON public."Suscripcions" USING btree ("ClienteId", "Estado");


--
-- Name: IX_Suscripcions_Estado; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE INDEX "IX_Suscripcions_Estado" ON public."Suscripcions" USING btree ("Estado");


--
-- Name: IX_Suscripcions_PromocionId; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE INDEX "IX_Suscripcions_PromocionId" ON public."Suscripcions" USING btree ("PromocionId");


--
-- Name: IX_Suscripcions_Tipo; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE INDEX "IX_Suscripcions_Tipo" ON public."Suscripcions" USING btree ("Tipo");


--
-- Name: IX_casilleros_AsignadoAClienteId; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE INDEX "IX_casilleros_AsignadoAClienteId" ON public.casilleros USING btree ("AsignadoAClienteId");


--
-- Name: IX_casilleros_Numero; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE UNIQUE INDEX "IX_casilleros_Numero" ON public.casilleros USING btree ("Numero");


--
-- Name: IX_clientes_Ci; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE UNIQUE INDEX "IX_clientes_Ci" ON public.clientes USING btree ("Ci");


--
-- Name: IX_ingresos_ClienteId_FechaIngreso; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE INDEX "IX_ingresos_ClienteId_FechaIngreso" ON public.ingresos USING btree ("ClienteId", "FechaIngreso");


--
-- Name: IX_ingresos_SuscripcionId; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE INDEX "IX_ingresos_SuscripcionId" ON public.ingresos USING btree ("SuscripcionId");


--
-- Name: IX_prestamos_casilleros_CasilleroId_FechaPrestamo; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE INDEX "IX_prestamos_casilleros_CasilleroId_FechaPrestamo" ON public.prestamos_casilleros USING btree ("CasilleroId", "FechaPrestamo");


--
-- Name: IX_prestamos_casilleros_ClienteId; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE INDEX "IX_prestamos_casilleros_ClienteId" ON public.prestamos_casilleros USING btree ("ClienteId");


--
-- Name: IX_prestamos_casilleros_IngresoId; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE INDEX "IX_prestamos_casilleros_IngresoId" ON public.prestamos_casilleros USING btree ("IngresoId");


--
-- Name: IX_promociones_festivas_FechaInicio_FechaFin; Type: INDEX; Schema: public; Owner: novafit_user
--

CREATE INDEX "IX_promociones_festivas_FechaInicio_FechaFin" ON public.promociones_festivas USING btree ("FechaInicio", "FechaFin");


--
-- Name: Suscripcions FK_Suscripcions_casilleros_CasilleroFijoId; Type: FK CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public."Suscripcions"
    ADD CONSTRAINT "FK_Suscripcions_casilleros_CasilleroFijoId" FOREIGN KEY ("CasilleroFijoId") REFERENCES public.casilleros("Id") ON DELETE SET NULL;


--
-- Name: Suscripcions FK_Suscripcions_clientes_ClienteId; Type: FK CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public."Suscripcions"
    ADD CONSTRAINT "FK_Suscripcions_clientes_ClienteId" FOREIGN KEY ("ClienteId") REFERENCES public.clientes("Id") ON DELETE RESTRICT;


--
-- Name: Suscripcions FK_Suscripcions_promociones_festivas_PromocionId; Type: FK CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public."Suscripcions"
    ADD CONSTRAINT "FK_Suscripcions_promociones_festivas_PromocionId" FOREIGN KEY ("PromocionId") REFERENCES public.promociones_festivas("Id") ON DELETE SET NULL;


--
-- Name: casilleros FK_casilleros_clientes_AsignadoAClienteId; Type: FK CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public.casilleros
    ADD CONSTRAINT "FK_casilleros_clientes_AsignadoAClienteId" FOREIGN KEY ("AsignadoAClienteId") REFERENCES public.clientes("Id");


--
-- Name: ingresos FK_ingresos_Suscripcions_SuscripcionId; Type: FK CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public.ingresos
    ADD CONSTRAINT "FK_ingresos_Suscripcions_SuscripcionId" FOREIGN KEY ("SuscripcionId") REFERENCES public."Suscripcions"("Id");


--
-- Name: ingresos FK_ingresos_clientes_ClienteId; Type: FK CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public.ingresos
    ADD CONSTRAINT "FK_ingresos_clientes_ClienteId" FOREIGN KEY ("ClienteId") REFERENCES public.clientes("Id") ON DELETE RESTRICT;


--
-- Name: prestamos_casilleros FK_prestamos_casilleros_casilleros_CasilleroId; Type: FK CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public.prestamos_casilleros
    ADD CONSTRAINT "FK_prestamos_casilleros_casilleros_CasilleroId" FOREIGN KEY ("CasilleroId") REFERENCES public.casilleros("Id") ON DELETE RESTRICT;


--
-- Name: prestamos_casilleros FK_prestamos_casilleros_clientes_ClienteId; Type: FK CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public.prestamos_casilleros
    ADD CONSTRAINT "FK_prestamos_casilleros_clientes_ClienteId" FOREIGN KEY ("ClienteId") REFERENCES public.clientes("Id");


--
-- Name: prestamos_casilleros FK_prestamos_casilleros_ingresos_IngresoId; Type: FK CONSTRAINT; Schema: public; Owner: novafit_user
--

ALTER TABLE ONLY public.prestamos_casilleros
    ADD CONSTRAINT "FK_prestamos_casilleros_ingresos_IngresoId" FOREIGN KEY ("IngresoId") REFERENCES public.ingresos("Id") ON DELETE CASCADE;


--
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: pg_database_owner
--

GRANT ALL ON SCHEMA public TO novafit_user;


--
-- Name: DEFAULT PRIVILEGES FOR SEQUENCES; Type: DEFAULT ACL; Schema: public; Owner: novafit_user
--

ALTER DEFAULT PRIVILEGES FOR ROLE novafit_user IN SCHEMA public GRANT ALL ON SEQUENCES TO novafit_user;


--
-- Name: DEFAULT PRIVILEGES FOR TABLES; Type: DEFAULT ACL; Schema: public; Owner: novafit_user
--

ALTER DEFAULT PRIVILEGES FOR ROLE novafit_user IN SCHEMA public GRANT ALL ON TABLES TO novafit_user;


--
-- PostgreSQL database dump complete
--

\unrestrict clxySOFSUaNwYaqxfvvKs0ha8FbSeXOgP2XzOoJqbJQp1MRnyYIDby3zt7KH1FN

