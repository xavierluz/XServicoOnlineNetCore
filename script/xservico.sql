--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.2
-- Dumped by pg_dump version 10.0

-- Started on 2019-01-07 16:44:24

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2271 (class 1262 OID 98837)
-- Name: OnlineETServico; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "OnlineETServico" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Portuguese_Brazil.1252' LC_CTYPE = 'Portuguese_Brazil.1252';


ALTER DATABASE "OnlineETServico" OWNER TO postgres;

\connect "OnlineETServico"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 7 (class 2615 OID 98964)
-- Name: dbo; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA dbo;


ALTER SCHEMA dbo OWNER TO postgres;

--
-- TOC entry 1 (class 3079 OID 12387)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2273 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = dbo, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 206 (class 1259 OID 115238)
-- Name: Almoxarifado; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE "Almoxarifado" (
    "Id" integer NOT NULL,
    "EmpresaId" uuid NOT NULL,
    "Descricao" character varying(500) NOT NULL,
    "Indentificacao" character varying(200) NOT NULL
);


ALTER TABLE "Almoxarifado" OWNER TO postgres;

--
-- TOC entry 205 (class 1259 OID 115236)
-- Name: Almoxarifado_Id_seq; Type: SEQUENCE; Schema: dbo; Owner: postgres
--

CREATE SEQUENCE "Almoxarifado_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "Almoxarifado_Id_seq" OWNER TO postgres;

--
-- TOC entry 2274 (class 0 OID 0)
-- Dependencies: 205
-- Name: Almoxarifado_Id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: postgres
--

ALTER SEQUENCE "Almoxarifado_Id_seq" OWNED BY "Almoxarifado"."Id";


--
-- TOC entry 199 (class 1259 OID 107179)
-- Name: Categoria; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE "Categoria" (
    "Id" integer NOT NULL,
    "Nome" character varying(50) NOT NULL,
    "Descricao" character varying(500),
    "Ativo" boolean NOT NULL
);


ALTER TABLE "Categoria" OWNER TO postgres;

--
-- TOC entry 198 (class 1259 OID 107177)
-- Name: Categoria_Id_seq; Type: SEQUENCE; Schema: dbo; Owner: postgres
--

CREATE SEQUENCE "Categoria_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "Categoria_Id_seq" OWNER TO postgres;

--
-- TOC entry 2275 (class 0 OID 0)
-- Dependencies: 198
-- Name: Categoria_Id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: postgres
--

ALTER SEQUENCE "Categoria_Id_seq" OWNED BY "Categoria"."Id";


--
-- TOC entry 201 (class 1259 OID 107190)
-- Name: Classificacao; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE "Classificacao" (
    "Id" integer NOT NULL,
    "Nome" character varying(50) NOT NULL,
    "Descricao" character varying(500),
    "Ativo" boolean NOT NULL
);


ALTER TABLE "Classificacao" OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 107188)
-- Name: Classificacao_Id_seq; Type: SEQUENCE; Schema: dbo; Owner: postgres
--

CREATE SEQUENCE "Classificacao_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "Classificacao_Id_seq" OWNER TO postgres;

--
-- TOC entry 2276 (class 0 OID 0)
-- Dependencies: 200
-- Name: Classificacao_Id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: postgres
--

ALTER SEQUENCE "Classificacao_Id_seq" OWNED BY "Classificacao"."Id";


--
-- TOC entry 204 (class 1259 OID 115228)
-- Name: Empresa; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE "Empresa" (
    "Id" uuid NOT NULL,
    "Chave" character varying(1000) NOT NULL,
    "CnpjCpf" character varying(15) NOT NULL,
    "RazaoSocial" character varying(200) NOT NULL,
    "NomeFantasia" character varying(200),
    "Logradouro" character varying(100) NOT NULL,
    "Cep" character varying(10) NOT NULL,
    "Bairro" character varying(50) NOT NULL,
    "Cidade" character varying(50) NOT NULL,
    "Site" character varying(100),
    "Telefone" character varying(12),
    "WhatsApp" character varying,
    "Ativo" boolean NOT NULL
);


ALTER TABLE "Empresa" OWNER TO postgres;

--
-- TOC entry 188 (class 1259 OID 99306)
-- Name: Funcao; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE "Funcao" (
    "Id" character varying(100) NOT NULL,
    "Nome" character varying(300),
    "NomeNormalizado" character varying(300),
    "CodigoConcorrencia" text
);


ALTER TABLE "Funcao" OWNER TO postgres;

--
-- TOC entry 191 (class 1259 OID 99324)
-- Name: FuncaoReivindicacao; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE "FuncaoReivindicacao" (
    "Id" integer NOT NULL,
    "FuncaoId" character varying(100) NOT NULL,
    "TipoReivindicacao" character varying(100),
    "ValorReivindicacao" character varying(200)
);


ALTER TABLE "FuncaoReivindicacao" OWNER TO postgres;

--
-- TOC entry 190 (class 1259 OID 99322)
-- Name: FuncaoReivindicacao_Id_seq; Type: SEQUENCE; Schema: dbo; Owner: postgres
--

CREATE SEQUENCE "FuncaoReivindicacao_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "FuncaoReivindicacao_Id_seq" OWNER TO postgres;

--
-- TOC entry 2277 (class 0 OID 0)
-- Dependencies: 190
-- Name: FuncaoReivindicacao_Id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: postgres
--

ALTER SEQUENCE "FuncaoReivindicacao_Id_seq" OWNED BY "FuncaoReivindicacao"."Id";


--
-- TOC entry 203 (class 1259 OID 107201)
-- Name: Materials; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE "Materials" (
    "Id" integer NOT NULL,
    "Nome" text,
    "Descricao" text,
    "categoriaId" integer NOT NULL,
    "classificacaoId" integer NOT NULL,
    "Ativo" boolean NOT NULL
);


ALTER TABLE "Materials" OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 107199)
-- Name: Materials_Id_seq; Type: SEQUENCE; Schema: dbo; Owner: postgres
--

CREATE SEQUENCE "Materials_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "Materials_Id_seq" OWNER TO postgres;

--
-- TOC entry 2278 (class 0 OID 0)
-- Dependencies: 202
-- Name: Materials_Id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: postgres
--

ALTER SEQUENCE "Materials_Id_seq" OWNED BY "Materials"."Id";


--
-- TOC entry 186 (class 1259 OID 98975)
-- Name: SEQ_GENERATED_FUNCAO_REIVINDICAO_ID; Type: SEQUENCE; Schema: dbo; Owner: postgres
--

CREATE SEQUENCE "SEQ_GENERATED_FUNCAO_REIVINDICAO_ID"
    START WITH 100
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "SEQ_GENERATED_FUNCAO_REIVINDICAO_ID" OWNER TO postgres;

--
-- TOC entry 187 (class 1259 OID 98977)
-- Name: SEQ_GENERATED_USUARIO_REIVINDICAO_ID; Type: SEQUENCE; Schema: dbo; Owner: postgres
--

CREATE SEQUENCE "SEQ_GENERATED_USUARIO_REIVINDICAO_ID"
    START WITH 100
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "SEQ_GENERATED_USUARIO_REIVINDICAO_ID" OWNER TO postgres;

--
-- TOC entry 189 (class 1259 OID 99314)
-- Name: Usuario; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE "Usuario" (
    "Id" character varying(100) NOT NULL,
    "Nome" character varying(50),
    "NomeNormalizado" character varying(256),
    "Email" character varying(100),
    "EmailNormalizado" character varying(256),
    "EmailConfirmed" boolean NOT NULL,
    "HashSenha" text,
    "CodigoSeguranca" text,
    "CodigoConcorrencia" text,
    "Telefone" text,
    "TelefoneConfirmado" boolean NOT NULL,
    "DoisTipoAcesso" boolean NOT NULL,
    "DataDesbloqueio" timestamp with time zone,
    "Bloqueado" boolean NOT NULL,
    "QuantidadeAcessoFalho" integer NOT NULL,
    "DataCadastro" date NOT NULL,
    "Usuario" character varying(50) NOT NULL,
    "EmpresaId" uuid
);


ALTER TABLE "Usuario" OWNER TO postgres;

--
-- TOC entry 192 (class 1259 OID 99335)
-- Name: UsuarioFuncao; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE "UsuarioFuncao" (
    "UsuarioId" character varying(100) NOT NULL,
    "FuncaoId" character varying(100) NOT NULL
);


ALTER TABLE "UsuarioFuncao" OWNER TO postgres;

--
-- TOC entry 193 (class 1259 OID 99350)
-- Name: UsuarioLogin; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE "UsuarioLogin" (
    "ProvedorLogin" character varying(128) NOT NULL,
    "ChaveProvedor" character varying(128) NOT NULL,
    "NomeProvedor" character varying(100),
    "UsuarioId" character varying(100) NOT NULL
);


ALTER TABLE "UsuarioLogin" OWNER TO postgres;

--
-- TOC entry 195 (class 1259 OID 99362)
-- Name: UsuarioReivindicacao; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE "UsuarioReivindicacao" (
    "Id" integer NOT NULL,
    "UsuarioId" character varying(100) NOT NULL,
    "TipoReivindicacao" character varying(100),
    "ValorReivindicacao" character varying(200)
);


ALTER TABLE "UsuarioReivindicacao" OWNER TO postgres;

--
-- TOC entry 194 (class 1259 OID 99360)
-- Name: UsuarioReivindicacao_Id_seq; Type: SEQUENCE; Schema: dbo; Owner: postgres
--

CREATE SEQUENCE "UsuarioReivindicacao_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "UsuarioReivindicacao_Id_seq" OWNER TO postgres;

--
-- TOC entry 2279 (class 0 OID 0)
-- Dependencies: 194
-- Name: UsuarioReivindicacao_Id_seq; Type: SEQUENCE OWNED BY; Schema: dbo; Owner: postgres
--

ALTER SEQUENCE "UsuarioReivindicacao_Id_seq" OWNED BY "UsuarioReivindicacao"."Id";


--
-- TOC entry 196 (class 1259 OID 99373)
-- Name: UsuarioToken; Type: TABLE; Schema: dbo; Owner: postgres
--

CREATE TABLE "UsuarioToken" (
    "UsuarioId" character varying(100) NOT NULL,
    "ProvedorLogin" character varying(200) NOT NULL,
    "Nome" character varying(100) NOT NULL,
    "TokenValor" character varying(200)
);


ALTER TABLE "UsuarioToken" OWNER TO postgres;

SET search_path = public, pg_catalog;

--
-- TOC entry 197 (class 1259 OID 107029)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE "__EFMigrationsHistory" OWNER TO postgres;

SET search_path = dbo, pg_catalog;

--
-- TOC entry 2077 (class 2604 OID 115241)
-- Name: Almoxarifado Id; Type: DEFAULT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "Almoxarifado" ALTER COLUMN "Id" SET DEFAULT nextval('"Almoxarifado_Id_seq"'::regclass);


--
-- TOC entry 2074 (class 2604 OID 107182)
-- Name: Categoria Id; Type: DEFAULT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "Categoria" ALTER COLUMN "Id" SET DEFAULT nextval('"Categoria_Id_seq"'::regclass);


--
-- TOC entry 2075 (class 2604 OID 107193)
-- Name: Classificacao Id; Type: DEFAULT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "Classificacao" ALTER COLUMN "Id" SET DEFAULT nextval('"Classificacao_Id_seq"'::regclass);


--
-- TOC entry 2072 (class 2604 OID 99327)
-- Name: FuncaoReivindicacao Id; Type: DEFAULT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "FuncaoReivindicacao" ALTER COLUMN "Id" SET DEFAULT nextval('"FuncaoReivindicacao_Id_seq"'::regclass);


--
-- TOC entry 2076 (class 2604 OID 107204)
-- Name: Materials Id; Type: DEFAULT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "Materials" ALTER COLUMN "Id" SET DEFAULT nextval('"Materials_Id_seq"'::regclass);


--
-- TOC entry 2073 (class 2604 OID 99365)
-- Name: UsuarioReivindicacao Id; Type: DEFAULT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "UsuarioReivindicacao" ALTER COLUMN "Id" SET DEFAULT nextval('"UsuarioReivindicacao_Id_seq"'::regclass);


--
-- TOC entry 2266 (class 0 OID 115238)
-- Dependencies: 206
-- Data for Name: Almoxarifado; Type: TABLE DATA; Schema: dbo; Owner: postgres
--

COPY "Almoxarifado" ("Id", "EmpresaId", "Descricao", "Indentificacao") FROM stdin;
\.


--
-- TOC entry 2259 (class 0 OID 107179)
-- Dependencies: 199
-- Data for Name: Categoria; Type: TABLE DATA; Schema: dbo; Owner: postgres
--

COPY "Categoria" ("Id", "Nome", "Descricao", "Ativo") FROM stdin;
1	Laticinios	Derivados do leite, origem animal ou vegetal	f
2	bebidas	Cervejas, Cachaça, água	f
3	bebidas	Cervejas, Cachaça, água	f
4	bebidas	Cervejas, águas e etc	f
5	bebidas	Cervejas, derivados da cachaça, água, refrigerantes  e etc	f
6	bebidas	Refrigerantes, Bebidas alcoólica , Água e etc	t
7	Carnes	Produto de origem aninal	t
8	Frutas	Produto de origem vegetal	t
\.


--
-- TOC entry 2261 (class 0 OID 107190)
-- Dependencies: 201
-- Data for Name: Classificacao; Type: TABLE DATA; Schema: dbo; Owner: postgres
--

COPY "Classificacao" ("Id", "Nome", "Descricao", "Ativo") FROM stdin;
4	Sucos	Bebidas sem teor alcoólico de origem natural ou artificial 	t
3	Alcoólica	Bebidas que contêm álcool em qualquer quantidade.	t
\.


--
-- TOC entry 2264 (class 0 OID 115228)
-- Dependencies: 204
-- Data for Name: Empresa; Type: TABLE DATA; Schema: dbo; Owner: postgres
--

COPY "Empresa" ("Id", "Chave", "CnpjCpf", "RazaoSocial", "NomeFantasia", "Logradouro", "Cep", "Bairro", "Cidade", "Site", "Telefone", "WhatsApp", "Ativo") FROM stdin;
5bf63fc8-0b6f-4f2c-8c56-f4e8e2aadb9b	11522312516520624288213451611723150275814018625136188	25946262874	Xavier	Gonçalves	Rua Alta Floresta	06814400	jd.São Francisco	Embu	\N	\N	\N	t
\.


--
-- TOC entry 2248 (class 0 OID 99306)
-- Dependencies: 188
-- Data for Name: Funcao; Type: TABLE DATA; Schema: dbo; Owner: postgres
--

COPY "Funcao" ("Id", "Nome", "NomeNormalizado", "CodigoConcorrencia") FROM stdin;
1245	Teste	Teste2	1233
d5037946-53d8-455c-b1d2-262ca12668f4	Admin	ADMIN	ce80bfcb-c067-4f9b-9638-1c6b5d060874
\.


--
-- TOC entry 2251 (class 0 OID 99324)
-- Dependencies: 191
-- Data for Name: FuncaoReivindicacao; Type: TABLE DATA; Schema: dbo; Owner: postgres
--

COPY "FuncaoReivindicacao" ("Id", "FuncaoId", "TipoReivindicacao", "ValorReivindicacao") FROM stdin;
1	1245	teste	teste
\.


--
-- TOC entry 2263 (class 0 OID 107201)
-- Dependencies: 203
-- Data for Name: Materials; Type: TABLE DATA; Schema: dbo; Owner: postgres
--

COPY "Materials" ("Id", "Nome", "Descricao", "categoriaId", "classificacaoId", "Ativo") FROM stdin;
2	Cerveja Skol	Bebida a base de cevada e malte	6	3	t
1	Cerveja Skol	Bebida a base de cevada e malte	6	3	t
\.


--
-- TOC entry 2249 (class 0 OID 99314)
-- Dependencies: 189
-- Data for Name: Usuario; Type: TABLE DATA; Schema: dbo; Owner: postgres
--

COPY "Usuario" ("Id", "Nome", "NomeNormalizado", "Email", "EmailNormalizado", "EmailConfirmed", "HashSenha", "CodigoSeguranca", "CodigoConcorrencia", "Telefone", "TelefoneConfirmado", "DoisTipoAcesso", "DataDesbloqueio", "Bloqueado", "QuantidadeAcessoFalho", "DataCadastro", "Usuario", "EmpresaId") FROM stdin;
c072e50f-b154-4f0f-b6d1-bbbe9d27274c	Elenice de Brito Silva	XAVIERLUZ@GMAIL.COM	xavierluz@gmail.com	XAVIERLUZ@GMAIL.COM	f	AQAAAAEAACcQAAAAEJXWzRoBIg6TXLGM+qX4ZCU/Xt2YNCtjMKUz+31ls49UvGY/DyGO2b58NIjAGSy3dg==	A6NIE2N7PUYFPJD6SLWVGPQ6HOFLXNC2	06c0f3e0-e165-4476-b8d9-df84794df82d	(11) 95121-4906	f	f	\N	t	0	2018-12-19	xavierluz@gmail.com	2ddfa9b3-cef6-4992-8b1d-529c4de44aaf
2ddfa9b3-cef6-4992-8b1d-529c4de44acb	Elenice de Brito Silva	XAVIERLUZ100@GMAIL.COM	xavierluz100@gmail.com	XAVIERLUZ100@GMAIL.COM	f	AQAAAAEAACcQAAAAEOhmmVwwzCfjk53Z/dFT5zsp9rxPWMnLD2HL9Q4ykbmrqmr6MOqf9bNftpQCDh2L3A==	HQQBGGUEVR7UAYIGDO7BDWIWK7SV6XZ3	b9709f5b-ee20-444e-b00f-27eb4ef300c4	(11) 95121-4906	f	f	\N	t	0	2018-12-20	xavierluz100@gmail.com	2ddfa9b3-cef6-4992-8b1d-529c4de44aaf
\.


--
-- TOC entry 2252 (class 0 OID 99335)
-- Dependencies: 192
-- Data for Name: UsuarioFuncao; Type: TABLE DATA; Schema: dbo; Owner: postgres
--

COPY "UsuarioFuncao" ("UsuarioId", "FuncaoId") FROM stdin;
\.


--
-- TOC entry 2253 (class 0 OID 99350)
-- Dependencies: 193
-- Data for Name: UsuarioLogin; Type: TABLE DATA; Schema: dbo; Owner: postgres
--

COPY "UsuarioLogin" ("ProvedorLogin", "ChaveProvedor", "NomeProvedor", "UsuarioId") FROM stdin;
\.


--
-- TOC entry 2255 (class 0 OID 99362)
-- Dependencies: 195
-- Data for Name: UsuarioReivindicacao; Type: TABLE DATA; Schema: dbo; Owner: postgres
--

COPY "UsuarioReivindicacao" ("Id", "UsuarioId", "TipoReivindicacao", "ValorReivindicacao") FROM stdin;
\.


--
-- TOC entry 2256 (class 0 OID 99373)
-- Dependencies: 196
-- Data for Name: UsuarioToken; Type: TABLE DATA; Schema: dbo; Owner: postgres
--

COPY "UsuarioToken" ("UsuarioId", "ProvedorLogin", "Nome", "TokenValor") FROM stdin;
\.


SET search_path = public, pg_catalog;

--
-- TOC entry 2257 (class 0 OID 107029)
-- Dependencies: 197
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY "__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
\.


SET search_path = dbo, pg_catalog;

--
-- TOC entry 2280 (class 0 OID 0)
-- Dependencies: 205
-- Name: Almoxarifado_Id_seq; Type: SEQUENCE SET; Schema: dbo; Owner: postgres
--

SELECT pg_catalog.setval('"Almoxarifado_Id_seq"', 1, false);


--
-- TOC entry 2281 (class 0 OID 0)
-- Dependencies: 198
-- Name: Categoria_Id_seq; Type: SEQUENCE SET; Schema: dbo; Owner: postgres
--

SELECT pg_catalog.setval('"Categoria_Id_seq"', 8, true);


--
-- TOC entry 2282 (class 0 OID 0)
-- Dependencies: 200
-- Name: Classificacao_Id_seq; Type: SEQUENCE SET; Schema: dbo; Owner: postgres
--

SELECT pg_catalog.setval('"Classificacao_Id_seq"', 4, true);


--
-- TOC entry 2283 (class 0 OID 0)
-- Dependencies: 190
-- Name: FuncaoReivindicacao_Id_seq; Type: SEQUENCE SET; Schema: dbo; Owner: postgres
--

SELECT pg_catalog.setval('"FuncaoReivindicacao_Id_seq"', 1, true);


--
-- TOC entry 2284 (class 0 OID 0)
-- Dependencies: 202
-- Name: Materials_Id_seq; Type: SEQUENCE SET; Schema: dbo; Owner: postgres
--

SELECT pg_catalog.setval('"Materials_Id_seq"', 2, true);


--
-- TOC entry 2285 (class 0 OID 0)
-- Dependencies: 186
-- Name: SEQ_GENERATED_FUNCAO_REIVINDICAO_ID; Type: SEQUENCE SET; Schema: dbo; Owner: postgres
--

SELECT pg_catalog.setval('"SEQ_GENERATED_FUNCAO_REIVINDICAO_ID"', 100, false);


--
-- TOC entry 2286 (class 0 OID 0)
-- Dependencies: 187
-- Name: SEQ_GENERATED_USUARIO_REIVINDICAO_ID; Type: SEQUENCE SET; Schema: dbo; Owner: postgres
--

SELECT pg_catalog.setval('"SEQ_GENERATED_USUARIO_REIVINDICAO_ID"', 100, false);


--
-- TOC entry 2287 (class 0 OID 0)
-- Dependencies: 194
-- Name: UsuarioReivindicacao_Id_seq; Type: SEQUENCE SET; Schema: dbo; Owner: postgres
--

SELECT pg_catalog.setval('"UsuarioReivindicacao_Id_seq"', 1, false);


--
-- TOC entry 2119 (class 2606 OID 115246)
-- Name: Almoxarifado PK_Almoxarifado; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "Almoxarifado"
    ADD CONSTRAINT "PK_Almoxarifado" PRIMARY KEY ("Id");


--
-- TOC entry 2105 (class 2606 OID 107187)
-- Name: Categoria PK_Categoria; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "Categoria"
    ADD CONSTRAINT "PK_Categoria" PRIMARY KEY ("Id");


--
-- TOC entry 2108 (class 2606 OID 107198)
-- Name: Classificacao PK_Classificacao; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "Classificacao"
    ADD CONSTRAINT "PK_Classificacao" PRIMARY KEY ("Id");


--
-- TOC entry 2115 (class 2606 OID 115235)
-- Name: Empresa PK_Empresa; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "Empresa"
    ADD CONSTRAINT "PK_Empresa" PRIMARY KEY ("Id");


--
-- TOC entry 2079 (class 2606 OID 99313)
-- Name: Funcao PK_Funcao; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "Funcao"
    ADD CONSTRAINT "PK_Funcao" PRIMARY KEY ("Id");


--
-- TOC entry 2089 (class 2606 OID 99329)
-- Name: FuncaoReivindicacao PK_FuncaoReivindicacao; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "FuncaoReivindicacao"
    ADD CONSTRAINT "PK_FuncaoReivindicacao" PRIMARY KEY ("Id");


--
-- TOC entry 2112 (class 2606 OID 107209)
-- Name: Materials PK_Materials; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "Materials"
    ADD CONSTRAINT "PK_Materials" PRIMARY KEY ("Id");


--
-- TOC entry 2085 (class 2606 OID 99321)
-- Name: Usuario PK_Usuario; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "Usuario"
    ADD CONSTRAINT "PK_Usuario" PRIMARY KEY ("Id");


--
-- TOC entry 2092 (class 2606 OID 99339)
-- Name: UsuarioFuncao PK_UsuarioFuncao; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "UsuarioFuncao"
    ADD CONSTRAINT "PK_UsuarioFuncao" PRIMARY KEY ("UsuarioId", "FuncaoId");


--
-- TOC entry 2095 (class 2606 OID 99354)
-- Name: UsuarioLogin PK_UsuarioLogin; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "UsuarioLogin"
    ADD CONSTRAINT "PK_UsuarioLogin" PRIMARY KEY ("ProvedorLogin", "ChaveProvedor");


--
-- TOC entry 2098 (class 2606 OID 99367)
-- Name: UsuarioReivindicacao PK_UsuarioReivindicacao; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "UsuarioReivindicacao"
    ADD CONSTRAINT "PK_UsuarioReivindicacao" PRIMARY KEY ("Id");


--
-- TOC entry 2100 (class 2606 OID 99380)
-- Name: UsuarioToken PK_UsuarioToken; Type: CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "UsuarioToken"
    ADD CONSTRAINT "PK_UsuarioToken" PRIMARY KEY ("UsuarioId", "ProvedorLogin", "Nome");


SET search_path = public, pg_catalog;

--
-- TOC entry 2102 (class 2606 OID 107033)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


SET search_path = dbo, pg_catalog;

--
-- TOC entry 2116 (class 1259 OID 115253)
-- Name: IDX_ALMOXARIFADO_INDENTIFICACAO; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE UNIQUE INDEX "IDX_ALMOXARIFADO_INDENTIFICACAO" ON "Almoxarifado" USING btree ("Indentificacao");


--
-- TOC entry 2081 (class 1259 OID 99388)
-- Name: IDX_USUARIO_EMAIL; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE INDEX "IDX_USUARIO_EMAIL" ON "Usuario" USING btree ("EmailNormalizado");


--
-- TOC entry 2082 (class 1259 OID 107035)
-- Name: IDX_USUARIO_NOME; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE INDEX "IDX_USUARIO_NOME" ON "Usuario" USING btree ("Nome" DESC NULLS LAST);

ALTER TABLE "Usuario" CLUSTER ON "IDX_USUARIO_NOME";


--
-- TOC entry 2083 (class 1259 OID 107034)
-- Name: IDX_USUARIO_NOME_NORMALIZADO; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE INDEX "IDX_USUARIO_NOME_NORMALIZADO" ON "Usuario" USING btree ("NomeNormalizado");


--
-- TOC entry 2103 (class 1259 OID 107220)
-- Name: INDX_CATEGORIA_NOME; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE INDEX "INDX_CATEGORIA_NOME" ON "Categoria" USING btree ("Nome");


--
-- TOC entry 2106 (class 1259 OID 107221)
-- Name: INDX_CLASSIFICAO_NOME; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE INDEX "INDX_CLASSIFICAO_NOME" ON "Classificacao" USING btree ("Nome");


--
-- TOC entry 2113 (class 1259 OID 115254)
-- Name: INDX_EMPRESA_LOGRADOURO; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE UNIQUE INDEX "INDX_EMPRESA_LOGRADOURO" ON "Empresa" USING btree ("CnpjCpf");


--
-- TOC entry 2117 (class 1259 OID 115252)
-- Name: IX_Almoxarifado_EmpresaId; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE INDEX "IX_Almoxarifado_EmpresaId" ON "Almoxarifado" USING btree ("EmpresaId");


--
-- TOC entry 2087 (class 1259 OID 99387)
-- Name: IX_FuncaoReivindicacao_FuncaoId; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE INDEX "IX_FuncaoReivindicacao_FuncaoId" ON "FuncaoReivindicacao" USING btree ("FuncaoId");


--
-- TOC entry 2109 (class 1259 OID 107222)
-- Name: IX_Materials_categoriaId; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE INDEX "IX_Materials_categoriaId" ON "Materials" USING btree ("categoriaId");


--
-- TOC entry 2110 (class 1259 OID 107223)
-- Name: IX_Materials_classificacaoId; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE INDEX "IX_Materials_classificacaoId" ON "Materials" USING btree ("classificacaoId");


--
-- TOC entry 2090 (class 1259 OID 99390)
-- Name: IX_UsuarioFuncao_FuncaoId; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE INDEX "IX_UsuarioFuncao_FuncaoId" ON "UsuarioFuncao" USING btree ("FuncaoId");


--
-- TOC entry 2093 (class 1259 OID 99391)
-- Name: IX_UsuarioLogin_UsuarioId; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE INDEX "IX_UsuarioLogin_UsuarioId" ON "UsuarioLogin" USING btree ("UsuarioId");


--
-- TOC entry 2096 (class 1259 OID 99392)
-- Name: IX_UsuarioReivindicacao_UsuarioId; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE INDEX "IX_UsuarioReivindicacao_UsuarioId" ON "UsuarioReivindicacao" USING btree ("UsuarioId");


--
-- TOC entry 2080 (class 1259 OID 99386)
-- Name: RoleNameIndex; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE UNIQUE INDEX "RoleNameIndex" ON "Funcao" USING btree ("NomeNormalizado");


--
-- TOC entry 2086 (class 1259 OID 115260)
-- Name: fki_INDX_USUARIO_EMPRESA; Type: INDEX; Schema: dbo; Owner: postgres
--

CREATE INDEX "fki_INDX_USUARIO_EMPRESA" ON "Usuario" USING btree ("EmpresaId");


--
-- TOC entry 2128 (class 2606 OID 115247)
-- Name: Almoxarifado FK_Almoxarifado_Empresa_EmpresaId; Type: FK CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "Almoxarifado"
    ADD CONSTRAINT "FK_Almoxarifado_Empresa_EmpresaId" FOREIGN KEY ("EmpresaId") REFERENCES "Empresa"("Id") ON DELETE CASCADE;


--
-- TOC entry 2120 (class 2606 OID 99330)
-- Name: FuncaoReivindicacao FK_FuncaoReivindicacao_Funcao_FuncaoId; Type: FK CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "FuncaoReivindicacao"
    ADD CONSTRAINT "FK_FuncaoReivindicacao_Funcao_FuncaoId" FOREIGN KEY ("FuncaoId") REFERENCES "Funcao"("Id") ON DELETE CASCADE;


--
-- TOC entry 2126 (class 2606 OID 107210)
-- Name: Materials FK_Materials_Categoria_categoriaId; Type: FK CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "Materials"
    ADD CONSTRAINT "FK_Materials_Categoria_categoriaId" FOREIGN KEY ("categoriaId") REFERENCES "Categoria"("Id") ON DELETE CASCADE;


--
-- TOC entry 2127 (class 2606 OID 107215)
-- Name: Materials FK_Materials_Classificacao_classificacaoId; Type: FK CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "Materials"
    ADD CONSTRAINT "FK_Materials_Classificacao_classificacaoId" FOREIGN KEY ("classificacaoId") REFERENCES "Classificacao"("Id") ON DELETE CASCADE;


--
-- TOC entry 2121 (class 2606 OID 99340)
-- Name: UsuarioFuncao FK_UsuarioFuncao_Funcao_FuncaoId; Type: FK CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "UsuarioFuncao"
    ADD CONSTRAINT "FK_UsuarioFuncao_Funcao_FuncaoId" FOREIGN KEY ("FuncaoId") REFERENCES "Funcao"("Id") ON DELETE CASCADE;


--
-- TOC entry 2122 (class 2606 OID 99345)
-- Name: UsuarioFuncao FK_UsuarioFuncao_Usuario_UsuarioId; Type: FK CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "UsuarioFuncao"
    ADD CONSTRAINT "FK_UsuarioFuncao_Usuario_UsuarioId" FOREIGN KEY ("UsuarioId") REFERENCES "Usuario"("Id") ON DELETE CASCADE;


--
-- TOC entry 2123 (class 2606 OID 99355)
-- Name: UsuarioLogin FK_UsuarioLogin_Usuario_UsuarioId; Type: FK CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "UsuarioLogin"
    ADD CONSTRAINT "FK_UsuarioLogin_Usuario_UsuarioId" FOREIGN KEY ("UsuarioId") REFERENCES "Usuario"("Id") ON DELETE CASCADE;


--
-- TOC entry 2124 (class 2606 OID 99368)
-- Name: UsuarioReivindicacao FK_UsuarioReivindicacao_Usuario_UsuarioId; Type: FK CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "UsuarioReivindicacao"
    ADD CONSTRAINT "FK_UsuarioReivindicacao_Usuario_UsuarioId" FOREIGN KEY ("UsuarioId") REFERENCES "Usuario"("Id") ON DELETE CASCADE;


--
-- TOC entry 2125 (class 2606 OID 99381)
-- Name: UsuarioToken FK_UsuarioToken_Usuario_UsuarioId; Type: FK CONSTRAINT; Schema: dbo; Owner: postgres
--

ALTER TABLE ONLY "UsuarioToken"
    ADD CONSTRAINT "FK_UsuarioToken_Usuario_UsuarioId" FOREIGN KEY ("UsuarioId") REFERENCES "Usuario"("Id") ON DELETE CASCADE;


-- Completed on 2019-01-07 16:44:26

--
-- PostgreSQL database dump complete
--

