using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proyectoTWA.Models
{
    public class PersonaProyecto
    {
        [Key]
        [Required(ErrorMessage = "Debe ingresar un Nombre para continuar")]
        public string Rut { get; set; }
        public string NombreProyecto { get; set; }
        public string DirectorS_N { get; set; }
        public string ResponsableLegalS_N { get; set; }
    }
}
