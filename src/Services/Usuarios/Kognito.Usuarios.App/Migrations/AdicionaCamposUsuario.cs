using Microsoft.EntityFrameworkCore.Migrations;

namespace Kognito.Usuarios.App.Migrations;

public partial class AdicionaCamposUsuario : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "ResponsavelEmail",
            table: "Usuarios",
            type: "nvarchar(256)",
            maxLength: 256,
            nullable: true);

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
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ResponsavelEmail",
            table: "Usuarios");

        migrationBuilder.DropColumn(
            name: "CodigoPai",
            table: "Usuarios");

        migrationBuilder.DropColumn(
            name: "CodigoRecuperacaoEmail",
            table: "Usuarios");
    }
}