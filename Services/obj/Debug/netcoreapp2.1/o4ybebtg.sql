﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE SCHEMA IF NOT EXISTS dbo;

CREATE TABLE dbo."Categoria" (
    "Id" serial NOT NULL,
    "Nome" character varying(50) NOT NULL,
    "Descricao" character varying(500) NULL,
    "Ativo" boolean NOT NULL,
    CONSTRAINT "PK_Categoria" PRIMARY KEY ("Id")
);

CREATE TABLE dbo."Classificacao" (
    "Id" serial NOT NULL,
    "Nome" character varying(50) NOT NULL,
    "Descricao" character varying(500) NULL,
    "Ativo" boolean NOT NULL,
    CONSTRAINT "PK_Classificacao" PRIMARY KEY ("Id")
);

CREATE TABLE dbo."Materials" (
    "Id" serial NOT NULL,
    "Nome" text NULL,
    "Descricao" text NULL,
    "categoriaId" integer NOT NULL,
    "classificacaoId" integer NOT NULL,
    "Ativo" boolean NOT NULL,
    CONSTRAINT "PK_Materials" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Materials_Categoria_categoriaId" FOREIGN KEY ("categoriaId") REFERENCES dbo."Categoria" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Materials_Classificacao_classificacaoId" FOREIGN KEY ("classificacaoId") REFERENCES dbo."Classificacao" ("Id") ON DELETE CASCADE
);

CREATE INDEX "INDX_CATEGORIA_NOME" ON dbo."Categoria" ("Nome");

CREATE INDEX "INDX_CLASSIFICAO_NOME" ON dbo."Classificacao" ("Nome");

CREATE INDEX "IX_Materials_categoriaId" ON dbo."Materials" ("categoriaId");

CREATE INDEX "IX_Materials_classificacaoId" ON dbo."Materials" ("classificacaoId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20181221174006_InitialService', '2.1.2-rtm-30932');

