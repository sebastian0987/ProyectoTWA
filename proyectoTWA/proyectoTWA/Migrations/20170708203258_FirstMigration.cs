using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace proyectoTWA.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "persona",
                columns: table => new
                {
                    rut = table.Column<string>(nullable: false),
                    administrador = table.Column<string>(nullable: false),
                    apellidoMaterno = table.Column<string>(nullable: false),
                    apellidoPaterno = table.Column<string>(nullable: false),
                    confirmarPassword = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: false),
                    fechaNacimiento = table.Column<string>(nullable: false),
                    nombre = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persona", x => x.rut);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "persona");
        }
    }
}
