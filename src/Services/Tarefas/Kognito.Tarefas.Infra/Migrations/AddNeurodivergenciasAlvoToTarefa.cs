using Microsoft.EntityFrameworkCore.Migrations;

namespace Kognito.Tarefas.Infra.Migrations;

public partial class AddNeurodivergenciasAlvoToTarefa : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TarefaNeurodivergencias");

        migrationBuilder.AddColumn<string>(
            name: "NeurodivergenciasAlvo",
            table: "Tarefas",
            type: "varchar(200)",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "NeurodivergenciasAlvo",
            table: "Tarefas");

        migrationBuilder.CreateTable(
            name: "TarefaNeurodivergencias",
            columns: table => new
            {
                TarefaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Neurodivergencia = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TarefaNeurodivergencias", x => new { x.TarefaId, x.Neurodivergencia });
                table.ForeignKey(
                    name: "FK_TarefaNeurodivergencias_Tarefas_TarefaId",
                    column: x => x.TarefaId,
                    principalTable: "Tarefas",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });
    }
}