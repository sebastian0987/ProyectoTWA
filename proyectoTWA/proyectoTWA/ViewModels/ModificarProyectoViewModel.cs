using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using proyectoTWA.Models;

namespace proyectoTWA.ViewModels
{
	public class ModificarProyectoViewModel
	{
		public Proyecto Proyecto { get; set; }
		public List<Persona> Personas{ get; set; }
		public List<Archivo> Archivos { get; set; }
		public List<PersonaProyecto> PersonaProyectos { get; set; }
	}
}
