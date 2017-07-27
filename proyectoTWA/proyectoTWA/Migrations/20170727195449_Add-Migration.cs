using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace proyectoTWA.Migrations
{
    public partial class AddMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_persona",
                table: "persona");

            migrationBuilder.RenameTable(
                name: "persona",
                newName: "Persona");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Persona",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "Persona",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "fechaNacimiento",
                table: "Persona",
                newName: "FechaNacimiento");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Persona",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "apellidoPaterno",
                table: "Persona",
                newName: "ApellidoPaterno");

            migrationBuilder.RenameColumn(
                name: "apellidoMaterno",
                table: "Persona",
                newName: "ApellidoMaterno");

            migrationBuilder.RenameColumn(
                name: "administrador",
                table: "Persona",
                newName: "Administrador");

            migrationBuilder.RenameColumn(
                name: "rut",
                table: "Persona",
                newName: "Rut");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persona",
                table: "Persona",
                column: "Rut");

            migrationBuilder.CreateTable(
                name: "Archivo",
                columns: table => new
                {
                    NombreArchivo = table.Column<string>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    NombreProyecto = table.Column<string>(nullable: true),
                    Rut = table.Column<string>(nullable: true),
                    Ubicacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archivo", x => x.NombreArchivo);
                });

            migrationBuilder.CreateTable(
                name: "PersonaProyecto",
                columns: table => new
                {
                    Rut = table.Column<string>(nullable: false),
                    DirectorS_N = table.Column<string>(nullable: true),
                    NombreProyecto = table.Column<string>(nullable: true),
                    ResponsableLegalS_N = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaProyecto", x => x.Rut);
                });

            migrationBuilder.CreateTable(
                name: "Proyecto",
                columns: table => new
                {
                    NombreProyecto = table.Column<string>(nullable: false),
                    FechaInicio = table.Column<string>(nullable: false),
                    FechaTermino = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyecto", x => x.NombreProyecto);
                });

            migrationBuilder.CreateTable(
                name: "Registro",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreArchivo = table.Column<string>(nullable: true),
                    Rut = table.Column<string>(nullable: true),
                    TipoModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registro", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archivo");

            migrationBuilder.DropTable(
                name: "PersonaProyecto");

            migrationBuilder.DropTable(
                name: "Proyecto");

            migrationBuilder.DropTable(
                name: "Registro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persona",
                table: "Persona");

            migrationBuilder.RenameTable(
                name: "Persona",
                newName: "persona");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "persona",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "persona",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "FechaNacimiento",
                table: "persona",
                newName: "fechaNacimiento");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "persona",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "ApellidoPaterno",
                table: "persona",
                newName: "apellidoPaterno");

            migrationBuilder.RenameColumn(
                name: "ApellidoMaterno",
                table: "persona",
                newName: "apellidoMaterno");

            migrationBuilder.RenameColumn(
                name: "Administrador",
                table: "persona",
                newName: "administrador");

            migrationBuilder.RenameColumn(
                name: "Rut",
                table: "persona",
                newName: "rut");

            migrationBuilder.AddPrimaryKey(
                name: "PK_persona",
                table: "persona",
                column: "rut");
        }
    }
}
