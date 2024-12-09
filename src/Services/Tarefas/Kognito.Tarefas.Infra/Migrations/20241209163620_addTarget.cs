using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kognito.Tarefas.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addTarget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NeurodivergenciasAlvo",
                table: "Tarefas",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeurodivergenciasAlvo",
                table: "Tarefas");
        }
    }
}
