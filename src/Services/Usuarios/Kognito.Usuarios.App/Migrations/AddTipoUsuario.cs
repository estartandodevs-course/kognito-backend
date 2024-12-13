using Microsoft.EntityFrameworkCore.Migrations;

namespace Kognito.Usuarios.App.Migrations;

public partial class AddTipoUsuario : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "TipoUsuario",
            table: "Usuarios",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "TipoUsuario",
            table: "Usuarios");
    }
}