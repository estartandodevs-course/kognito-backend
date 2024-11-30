using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kognito.Usuarios.App.Migrations
{
    /// <inheritdoc />
    public partial class incluimetaseemblemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emblemas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesbloqueadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emblemas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emblemas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Metas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Concluida = table.Column<bool>(type: "bit", nullable: false),
                    CriadaEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcluidaEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Metas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emblemas_UsuarioId",
                table: "Emblemas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Metas_UsuarioId",
                table: "Metas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emblemas");

            migrationBuilder.DropTable(
                name: "Metas");
        }
    }
}
