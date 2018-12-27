CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE SCHEMA IF NOT EXISTS dbo;

CREATE SEQUENCE dbo."SEQ_GENERATED_FUNCAO_REIVINDICAO_ID" START WITH 1 INCREMENT BY 10 NO MINVALUE NO MAXVALUE NO CYCLE;

CREATE SEQUENCE dbo."SEQ_GENERATED_USUARIO_REIVINDICAO_ID" START WITH 1 INCREMENT BY 10 NO MINVALUE NO MAXVALUE NO CYCLE;

CREATE SEQUENCE dbo."SEQ_GENERATED_FUNCAO_REIVINDICAO_ID" AS integer START WITH 100 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;

CREATE SEQUENCE dbo."SEQ_GENERATED_USUARIO_REIVINDICAO_ID" AS integer START WITH 100 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;

CREATE TABLE dbo."Funcao" (
    "Id" character varying(100) NOT NULL,
    "Nome" character varying(300) NULL,
    "NomeNormalizado" character varying(300) NULL,
    "CodigoConcorrencia" text NULL,
    CONSTRAINT "PK_Funcao" PRIMARY KEY ("Id")
);

CREATE TABLE dbo."Usuario" (
    "Id" character varying(100) NOT NULL,
    "Nome" character varying(50) NULL,
    "NomeNormalizado" character varying(256) NULL,
    "Email" character varying(100) NULL,
    "EmailNormalizado" character varying(256) NULL,
    "EmailConfirmed" boolean NOT NULL,
    "HashSenha" text NULL,
    "CodigoSeguranca" text NULL,
    "CodigoConcorrencia" text NULL,
    "Telefone" text NULL,
    "TelefoneConfirmado" boolean NOT NULL,
    "DoisTipoAcesso" boolean NOT NULL,
    "DataDesbloqueio" timestamp with time zone NULL,
    "Bloqueado" boolean NOT NULL,
    "QuantidadeAcessoFalho" integer NOT NULL,
    CONSTRAINT "PK_Usuario" PRIMARY KEY ("Id")
);

CREATE TABLE dbo."FuncaoReivindicacao" (
    "Id" integer NOT NULL DEFAULT (NEXT VALUE FOR dbo.SEQ_GENERATED_FUNCAO_REIVINDICAO_ID),
    "FuncaoId" character varying(100) NOT NULL,
    "TipoReivindicacao" character varying(100) NULL,
    "ValorReivindicacao" character varying(200) NULL,
    CONSTRAINT "PK_FuncaoReivindicacao" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_FuncaoReivindicacao_Funcao_FuncaoId" FOREIGN KEY ("FuncaoId") REFERENCES dbo."Funcao" ("Id") ON DELETE CASCADE
);

CREATE TABLE dbo."UsuarioFuncao" (
    "UsuarioId" character varying(100) NOT NULL,
    "FuncaoId" character varying(100) NOT NULL,
    CONSTRAINT "PK_UsuarioFuncao" PRIMARY KEY ("UsuarioId", "FuncaoId"),
    CONSTRAINT "FK_UsuarioFuncao_Funcao_FuncaoId" FOREIGN KEY ("FuncaoId") REFERENCES dbo."Funcao" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_UsuarioFuncao_Usuario_UsuarioId" FOREIGN KEY ("UsuarioId") REFERENCES dbo."Usuario" ("Id") ON DELETE CASCADE
);

CREATE TABLE dbo."UsuarioLogin" (
    "ProvedorLogin" character varying(128) NOT NULL,
    "ChaveProvedor" character varying(128) NOT NULL,
    "NomeProvedor" character varying(100) NULL,
    "UsuarioId" character varying(100) NOT NULL,
    CONSTRAINT "PK_UsuarioLogin" PRIMARY KEY ("ProvedorLogin", "ChaveProvedor"),
    CONSTRAINT "FK_UsuarioLogin_Usuario_UsuarioId" FOREIGN KEY ("UsuarioId") REFERENCES dbo."Usuario" ("Id") ON DELETE CASCADE
);

CREATE TABLE dbo."UsuarioReivindicacao" (
    "Id" integer NOT NULL DEFAULT (NEXT VALUE FOR dbo.SEQ_GENERATED_USUARIO_REIVINDICAO_ID),
    "UsuarioId" character varying(100) NOT NULL,
    "TipoReivindicacao" character varying(100) NULL,
    "ValorReivindicacao" character varying(200) NULL,
    CONSTRAINT "PK_UsuarioReivindicacao" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_UsuarioReivindicacao_Usuario_UsuarioId" FOREIGN KEY ("UsuarioId") REFERENCES dbo."Usuario" ("Id") ON DELETE CASCADE
);

CREATE TABLE dbo."UsuarioToken" (
    "UsuarioId" character varying(100) NOT NULL,
    "ProvedorLogin" character varying(200) NOT NULL,
    "Nome" character varying(100) NOT NULL,
    "TokenValor" character varying(200) NULL,
    CONSTRAINT "PK_UsuarioToken" PRIMARY KEY ("UsuarioId", "ProvedorLogin", "Nome"),
    CONSTRAINT "FK_UsuarioToken_Usuario_UsuarioId" FOREIGN KEY ("UsuarioId") REFERENCES dbo."Usuario" ("Id") ON DELETE CASCADE
);

CREATE UNIQUE INDEX "RoleNameIndex" ON dbo."Funcao" ("NomeNormalizado");

CREATE INDEX "IX_FuncaoReivindicacao_FuncaoId" ON dbo."FuncaoReivindicacao" ("FuncaoId");

CREATE INDEX "IDX_USUARIO_EMAIL" ON dbo."Usuario" ("EmailNormalizado");

CREATE UNIQUE INDEX "IDX_USUARIO_NOME" ON dbo."Usuario" ("NomeNormalizado");

CREATE INDEX "IX_UsuarioFuncao_FuncaoId" ON dbo."UsuarioFuncao" ("FuncaoId");

CREATE INDEX "IX_UsuarioLogin_UsuarioId" ON dbo."UsuarioLogin" ("UsuarioId");

CREATE INDEX "IX_UsuarioReivindicacao_UsuarioId" ON dbo."UsuarioReivindicacao" ("UsuarioId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20181214112506_migrationUsuario', '2.1.2-rtm-30932');

