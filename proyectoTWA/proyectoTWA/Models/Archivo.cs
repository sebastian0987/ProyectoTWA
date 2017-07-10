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
        public string nombre { get; set; }
        public string ubicacion { get; set; }
    }
}
