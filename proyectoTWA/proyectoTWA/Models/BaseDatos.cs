﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Archivo> Archivo { get; set; }
        public DbSet<Registro> Registro { get; set; }
    }
}
