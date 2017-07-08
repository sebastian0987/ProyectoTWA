using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using proyectoTWA.Models;

namespace proyectoTWA.Migrations
{
    [DbContext(typeof(BaseDatos))]
    partial class BaseDatosModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("proyectoTWA.Models.Persona", b =>
                {
                    b.Property<string>("rut")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("administrador")
                        .IsRequired();

                    b.Property<string>("apellidoMaterno")
                        .IsRequired();

                    b.Property<string>("apellidoPaterno")
                        .IsRequired();

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<string>("fechaNacimiento")
                        .IsRequired();

                    b.Property<string>("nombre")
                        .IsRequired();

                    b.Property<string>("password")
                        .IsRequired();

                    b.HasKey("rut");

                    b.ToTable("persona");
                });
        }
    }
}
