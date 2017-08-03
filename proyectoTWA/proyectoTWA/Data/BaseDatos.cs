using Microsoft.EntityFrameworkCore;

namespace proyectoTWA.Models
{
    public class BaseDatos : DbContext
    {
        public BaseDatos(DbContextOptions<BaseDatos> options) : base(options)
        {

        }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Archivo> Archivo { get; set; }
        public DbSet<Registro> Registro { get; set; }
        public DbSet<Proyecto> Proyecto { get; set; }
        public DbSet<PersonaProyecto> PersonaProyecto { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonaProyecto>().HasKey(u => new
            {
                u.Rut,
                u.NombreProyecto
            });
        }
    }
}