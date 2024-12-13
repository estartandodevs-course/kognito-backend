using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kognito.Usuarios.App.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    LoginHash = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Login_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login_DataDeCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Login_DataDeAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Neurodivergencia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ofensiva = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DataDeCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
