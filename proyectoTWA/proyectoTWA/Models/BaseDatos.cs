using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyectoTWA.Models
{
    public class BaseDatos : DbContext
    {
        public BaseDatos(DbContextOptions<BaseDatos> options) : base(options)
        {

        }
        public DbSet<Persona> persona { get; set; }
        public DbSet<Archivo> archivo { get; set; }
    }
}
