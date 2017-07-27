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
        [Required(ErrorMessage = "Debe ingresar un nombre para continuar")]
        public string NombreProyecto { get; set; }
		[Required]
		public string FechaInicio { get; set; }
		[Required]
        public string FechaTermino { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			DateTime StartDate = DateTime.Parse(FechaInicio);
			DateTime EndDate = DateTime.Parse(FechaTermino);
			if (EndDate < StartDate)
			{
				yield return
				  new ValidationResult(errorMessage: "La fecha de término debe ser posterior a la de inicio.",
									   memberNames: new[] { "EndDate" });
			}
		}
	} 
}
