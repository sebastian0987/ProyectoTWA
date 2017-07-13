using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proyectoTWA.Models
{
    public class Persona
    {
        [Key]
        [Required(ErrorMessage = "Debe ingresar un RUT para continuar")]
        public string rut { get; set; }
        [Required(ErrorMessage = "Debe ingresar un Nombre para continuar")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Debe ingresar un apellido paterno para continuar")]
        public string apellidoPaterno { get; set; }
        [Required(ErrorMessage = "Debe ingresar un apellido materno para continuar")]
        public string apellidoMaterno { get; set; }
        [Required(ErrorMessage = "Debe ingresar una fecha de nacimiento para continuar")]
        public string fechaNacimiento { get; set; }
        [Required(ErrorMessage = "Debe ingresar un correo para continuar")]
        public string email { get; set; }
        [Required(ErrorMessage = "Debe indicar si es un administrador")]
        public string administrador { get; set; }
        [Required(ErrorMessage = "Debe ingresar una contraseña para continuar")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Compare("password",ErrorMessage = "La contraseña ingresada no coincide")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [DataType(DataType.Password)]
        public string confirmarPassword { get; set; }
    }
}
