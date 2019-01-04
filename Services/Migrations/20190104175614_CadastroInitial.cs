using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Services.Migrations
{
    public partial class CadastroInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Empresa",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Chave = table.Column<string>(maxLength: 1000, nullable: false),
                    CnpjCpf = table.Column<string>(maxLength: 15, nullable: false),
                    RazaoSocial = table.Column<string>(maxLength: 200, nullable: false),
                    NomeFantasia = table.Column<string>(maxLength: 200, nullable: true),
                    Logradouro = table.Column<string>(maxLength: 100, nullable: false),
                    Cep = table.Column<string>(maxLength: 10, nullable: false),
                    Bairro = table.Column<string>(maxLength: 50, nullable: false),
                    Cidade = table.Column<string>(maxLength: 50, nullable: false),
                    Site = table.Column<string>(maxLength: 100, nullable: true),
                    Telefone = table.Column<string>(maxLength: 12, nullable: true),
                    WhatsApp = table.Column<string>(maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    Usuario = table.Column<string>(maxLength: 50, nullable: false),
                    NomeNormalizado = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    EmailNormalizado = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    HashSenha = table.Column<string>(nullable: false),
                    CodigoSeguranca = table.Column<string>(nullable: false),
                    CodigoConcorrencia = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    TelefoneConfirmado = table.Column<bool>(nullable: false),
                    DoisTipoAcesso = table.Column<bool>(nullable: false),
                    DataDesbloqueio = table.Column<DateTimeOffset>(nullable: true),
                    Bloqueado = table.Column<bool>(nullable: false),
                    QuantidadeAcessoFalho = table.Column<int>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Almoxarifado",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmpresaId = table.Column<Guid>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(maxLength: 500, nullable: false),
                    Indentificacao = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Almoxarifado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Almoxarifado_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalSchema: "dbo",
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Almoxarifado_EmpresaId",
                schema: "dbo",
                table: "Almoxarifado",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IDX_ALMOXARIFADO_INDENTIFICACAO",
                schema: "dbo",
                table: "Almoxarifado",
                column: "Indentificacao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "INDX_EMPRESA_LOGRADOURO",
                schema: "dbo",
                table: "Empresa",
                column: "CnpjCpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IDX_USUARIO_NOME",
                schema: "dbo",
                table: "Usuario",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IDX_USUARIO_EMAIL",
                schema: "dbo",
                table: "Usuario",
                column: "EmailNormalizado");

            migrationBuilder.CreateIndex(
                name: "IDX_USUARIO_NOME_NORMALIZADO",
                schema: "dbo",
                table: "Usuario",
                column: "NomeNormalizado",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Almoxarifado",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Empresa",
                schema: "dbo");
        }
    }
}
