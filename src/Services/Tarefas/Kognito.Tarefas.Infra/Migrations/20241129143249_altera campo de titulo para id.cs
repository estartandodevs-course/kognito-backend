using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kognito.Tarefas.Infra.Migrations
{
    /// <inheritdoc />
    public partial class alteracampodetituloparaid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TituloTarefa",
                table: "Notas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TituloTarefa",
                table: "Notas",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
