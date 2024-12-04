using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kognito.Usuarios.App.Migrations
{
    /// <inheritdoc />
    public partial class adicionaCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CodigoPai",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoRecuperacaoEmail",
                table: "Usuarios",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsavelEmail",
                table: "Usuarios",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoPai",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "CodigoRecuperacaoEmail",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ResponsavelEmail",
                table: "Usuarios");
        }
    }
}
