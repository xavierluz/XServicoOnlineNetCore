CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE SCHEMA IF NOT EXISTS dbo;

CREATE TABLE dbo."Empresa" (
    "Id" uuid NOT NULL,
    "Chave" character varying(1000) NOT NULL,
    "CnpjCpf" character varying(15) NOT NULL,
    "RazaoSocial" character varying(200) NOT NULL,
    "NomeFantasia" character varying(200) NULL,
    "Logradouro" character varying(100) NOT NULL,
    "Cep" character varying(10) NOT NULL,
    "Bairro" character varying(50) NOT NULL,
    "Cidade" character varying(50) NOT NULL,
    "Site" character varying(100) NULL,
    "Telefone" character varying(12) NULL,
    "WhatsApp" character varying(12) NOT NULL,
    CONSTRAINT "PK_Empresa" PRIMARY KEY ("Id")
);

CREATE TABLE dbo."Usuario" (
    "Id" character varying(100) NOT NULL,
    "Usuario" character varying(50) NOT NULL,
    "NomeNormalizado" character varying(256) NULL,
    "Email" character varying(100) NOT NULL,
    "EmailNormalizado" character varying(256) NULL,
    "EmailConfirmed" boolean NOT NULL,
    "HashSenha" text NOT NULL,
    "CodigoSeguranca" text NOT NULL,
    "CodigoConcorrencia" text NULL,
    "Telefone" text NULL,
    "TelefoneConfirmado" boolean NOT NULL,
    "DoisTipoAcesso" boolean NOT NULL,
    "DataDesbloqueio" timestamp with time zone NULL,
    "Bloqueado" boolean NOT NULL,
    "QuantidadeAcessoFalho" integer NOT NULL,
    "DataCadastro" timestamp without time zone NOT NULL,
    "Nome" character varying(50) NOT NULL,
    CONSTRAINT "PK_Usuario" PRIMARY KEY ("Id")
);

CREATE TABLE dbo."Almoxarifado" (
    "Id" serial NOT NULL,
    "EmpresaId" uuid NOT NULL,
    "Descricao" character varying(500) NOT NULL,
    "Indentificacao" character varying(200) NOT NULL,
    CONSTRAINT "PK_Almoxarifado" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Almoxarifado_Empresa_EmpresaId" FOREIGN KEY ("EmpresaId") REFERENCES dbo."Empresa" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Almoxarifado_EmpresaId" ON dbo."Almoxarifado" ("EmpresaId");

CREATE UNIQUE INDEX "IDX_ALMOXARIFADO_INDENTIFICACAO" ON dbo."Almoxarifado" ("Indentificacao");

CREATE UNIQUE INDEX "INDX_EMPRESA_LOGRADOURO" ON dbo."Empresa" ("CnpjCpf");

CREATE INDEX "IDX_USUARIO_NOME" ON dbo."Usuario" ("Nome");

CREATE INDEX "IDX_USUARIO_EMAIL" ON dbo."Usuario" ("EmailNormalizado");

CREATE UNIQUE INDEX "IDX_USUARIO_NOME_NORMALIZADO" ON dbo."Usuario" ("NomeNormalizado");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190104175614_CadastroInitial', '2.1.2-rtm-30932');

