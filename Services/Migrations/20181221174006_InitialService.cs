using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Services.Migrations
{
    public partial class InitialService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Categoria",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(maxLength: 500, nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classificacao",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(maxLength: 500, nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classificacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    categoriaId = table.Column<int>(nullable: false),
                    classificacaoId = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_Categoria_categoriaId",
                        column: x => x.categoriaId,
                        principalSchema: "dbo",
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Materials_Classificacao_classificacaoId",
                        column: x => x.classificacaoId,
                        principalSchema: "dbo",
                        principalTable: "Classificacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "INDX_CATEGORIA_NOME",
                schema: "dbo",
                table: "Categoria",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "INDX_CLASSIFICAO_NOME",
                schema: "dbo",
                table: "Classificacao",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_categoriaId",
                schema: "dbo",
                table: "Materials",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_classificacaoId",
                schema: "dbo",
                table: "Materials",
                column: "classificacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materials",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Categoria",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Classificacao",
                schema: "dbo");
        }
    }
}
