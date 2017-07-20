using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proyectoTWA.Models
{
    public class Proyecto
    {
        [Key]
        [Required(ErrorMessage = "Debe ingresar un Nombre para continuar")]
        public string NombreProyecto { get; set; }
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
    }
}
