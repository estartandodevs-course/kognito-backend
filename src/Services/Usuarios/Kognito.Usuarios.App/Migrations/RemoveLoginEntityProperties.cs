using Microsoft.EntityFrameworkCore.Migrations;

namespace Kognito.Usuarios.App.Migrations
{
    public partial class RemoveLoginEntityProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login_Id",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Login_DataDeCadastro",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Login_DataDeAlteracao",
                table: "Usuarios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Login_Id",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid());

            migrationBuilder.AddColumn<DateTime>(
                name: "Login_DataDeCadastro",
                table: "Usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Login_DataDeAlteracao",
                table: "Usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}