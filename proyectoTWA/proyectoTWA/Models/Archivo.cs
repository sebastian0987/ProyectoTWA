using System;
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
        public string nombreArchivo { get; set; }
        public string ubicacion { get; set; }
        public string estado { get; set; }
        public string nombreProyecto { get; set; }
    }
}
