﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proyectoTWA.Models
{
    public class Archivo
    {
        [Key]
        [Required(ErrorMessage = "Debe ingresar un Nombre para continuar")]
        public string NombreArchivo { get; set; }
        public string Ubicacion { get; set; }
        public string Estado { get; set; }
        public string NombreProyecto { get; set; }
        public string Rut { get; set; }
    }
}
