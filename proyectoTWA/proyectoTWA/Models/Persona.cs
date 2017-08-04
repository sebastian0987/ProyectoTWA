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
        public string Rut { get; set; }
        [Required(ErrorMessage = "Debe ingresar un Nombre para continuar")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe ingresar un apellido paterno para continuar")]
        public string ApellidoPaterno { get; set; }
        [Required(ErrorMessage = "Debe ingresar un apellido materno para continuar")]
        public string ApellidoMaterno { get; set; }
        [Required(ErrorMessage = "Debe ingresar una fecha de nacimiento para continuar")]
        public string FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Debe ingresar un correo para continuar")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe indicar si es un administrador")]
        public string Administrador { get; set; }
        [Required(ErrorMessage = "Debe ingresar una contraseña para continuar")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
		//[Compare("Password", ErrorMessage = "La contraseña ingresada no coincide")]
		//[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		//[DataType(DataType.Password)]
		//public string ConfirmarPassword { get; set; }
	}
}
