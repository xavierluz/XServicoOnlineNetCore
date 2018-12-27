using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace XServicoOnline.Migrations
{
    public partial class migrationUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence(
                name: "SEQ_GENERATED_FUNCAO_REIVINDICAO_ID",
                schema: "dbo",
                startValue: 100L);

            migrationBuilder.CreateSequence(
                name: "SEQ_GENERATED_USUARIO_REIVINDICAO_ID",
                schema: "dbo",
                startValue: 100L);

            migrationBuilder.CreateTable(
                name: "Funcao",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    Nome = table.Column<string>(maxLength: 300, nullable: true),
                    NomeNormalizado = table.Column<string>(maxLength: 300, nullable: true),
                    CodigoConcorrencia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: true),
                    NomeNormalizado = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    EmailNormalizado = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    HashSenha = table.Column<string>(nullable: true),
                    CodigoSeguranca = table.Column<string>(nullable: true),
                    CodigoConcorrencia = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    TelefoneConfirmado = table.Column<bool>(nullable: false),
                    DoisTipoAcesso = table.Column<bool>(nullable: false),
                    DataDesbloqueio = table.Column<DateTimeOffset>(nullable: true),
                    Bloqueado = table.Column<bool>(nullable: false),
                    QuantidadeAcessoFalho = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuncaoReivindicacao",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FuncaoId = table.Column<string>(maxLength: 100, nullable: false),
                    TipoReivindicacao = table.Column<string>(maxLength: 100, nullable: true),
                    ValorReivindicacao = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncaoReivindicacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuncaoReivindicacao_Funcao_FuncaoId",
                        column: x => x.FuncaoId,
                        principalSchema: "dbo",
                        principalTable: "Funcao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioFuncao",
                schema: "dbo",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(maxLength: 100, nullable: false),
                    FuncaoId = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioFuncao", x => new { x.UsuarioId, x.FuncaoId });
                    table.ForeignKey(
                        name: "FK_UsuarioFuncao_Funcao_FuncaoId",
                        column: x => x.FuncaoId,
                        principalSchema: "dbo",
                        principalTable: "Funcao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioFuncao_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "dbo",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioLogin",
                schema: "dbo",
                columns: table => new
                {
                    ProvedorLogin = table.Column<string>(maxLength: 128, nullable: false),
                    ChaveProvedor = table.Column<string>(maxLength: 128, nullable: false),
                    NomeProvedor = table.Column<string>(maxLength: 100, nullable: true),
                    UsuarioId = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioLogin", x => new { x.ProvedorLogin, x.ChaveProvedor });
                    table.ForeignKey(
                        name: "FK_UsuarioLogin_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "dbo",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioReivindicacao",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UsuarioId = table.Column<string>(nullable: false),
                    TipoReivindicacao = table.Column<string>(maxLength: 100, nullable: true),
                    ValorReivindicacao = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioReivindicacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioReivindicacao_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "dbo",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioToken",
                schema: "dbo",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(maxLength: 100, nullable: false),
                    ProvedorLogin = table.Column<string>(maxLength: 200, nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    TokenValor = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioToken", x => new { x.UsuarioId, x.ProvedorLogin, x.Nome });
                    table.ForeignKey(
                        name: "FK_UsuarioToken_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "dbo",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "Funcao",
                column: "NomeNormalizado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FuncaoReivindicacao_FuncaoId",
                schema: "dbo",
                table: "FuncaoReivindicacao",
                column: "FuncaoId");

            migrationBuilder.CreateIndex(
                name: "IDX_USUARIO_EMAIL",
                schema: "dbo",
                table: "Usuario",
                column: "EmailNormalizado");

            migrationBuilder.CreateIndex(
                name: "IDX_USUARIO_NOME",
                schema: "dbo",
                table: "Usuario",
                column: "NomeNormalizado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioFuncao_FuncaoId",
                schema: "dbo",
                table: "UsuarioFuncao",
                column: "FuncaoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioLogin_UsuarioId",
                schema: "dbo",
                table: "UsuarioLogin",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioReivindicacao_UsuarioId",
                schema: "dbo",
                table: "UsuarioReivindicacao",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncaoReivindicacao",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UsuarioFuncao",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UsuarioLogin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UsuarioReivindicacao",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UsuarioToken",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Funcao",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "SEQ_GENERATED_FUNCAO_REIVINDICAO_ID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "SEQ_GENERATED_USUARIO_REIVINDICAO_ID",
                schema: "dbo");
        }
    }
}
