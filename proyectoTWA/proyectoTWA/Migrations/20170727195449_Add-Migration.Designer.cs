using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using proyectoTWA.Models;

namespace proyectoTWA.Migrations
{
    [DbContext(typeof(BaseDatos))]
    [Migration("20170727195449_Add-Migration")]
    partial class AddMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("proyectoTWA.Models.Archivo", b =>
                {
                    b.Property<string>("NombreArchivo")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Estado");

                    b.Property<string>("NombreProyecto");

                    b.Property<string>("Rut");

                    b.Property<string>("Ubicacion");

                    b.HasKey("NombreArchivo");

                    b.ToTable("Archivo");
                });

            modelBuilder.Entity("proyectoTWA.Models.Persona", b =>
                {
                    b.Property<string>("Rut")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Administrador")
                        .IsRequired();

                    b.Property<string>("ApellidoMaterno")
                        .IsRequired();

                    b.Property<string>("ApellidoPaterno")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FechaNacimiento")
                        .IsRequired();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("Rut");

                    b.ToTable("Persona");
                });

            modelBuilder.Entity("proyectoTWA.Models.PersonaProyecto", b =>
                {
                    b.Property<string>("Rut")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DirectorS_N");

                    b.Property<string>("NombreProyecto");

                    b.Property<string>("ResponsableLegalS_N");

                    b.HasKey("Rut");

                    b.ToTable("PersonaProyecto");
                });

            modelBuilder.Entity("proyectoTWA.Models.Proyecto", b =>
                {
                    b.Property<string>("NombreProyecto")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FechaInicio")
                        .IsRequired();

                    b.Property<string>("FechaTermino")
                        .IsRequired();

                    b.HasKey("NombreProyecto");

                    b.ToTable("Proyecto");
                });

            modelBuilder.Entity("proyectoTWA.Models.Registro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NombreArchivo");

                    b.Property<string>("Rut");

                    b.Property<string>("TipoModificacion");

                    b.HasKey("Id");

                    b.ToTable("Registro");
                });
        }
    }
}
