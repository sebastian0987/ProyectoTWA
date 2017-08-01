using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using proyectoTWA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace proyectoTWA.Controllers
{
	public class ProyectoController : Controller
	{
		private BaseDatos _baseDatos;
		public ProyectoController(BaseDatos baseDatos)
		{
			_baseDatos = baseDatos;
		}
		public IActionResult Index()
		{
			return View(_baseDatos.Proyecto.ToList());
		}
		public IActionResult NuevoProyecto()
		{
			return View();
		}


		[HttpPost]
		public IActionResult NuevoProyecto(Proyecto proyecto)
		{
			if (ModelState.IsValid)
			{
				var cuenta = _baseDatos.Proyecto.Where(u => u.NombreProyecto == proyecto.NombreProyecto).FirstOrDefault();
				if (cuenta != null)
				{
					ViewBag.Message = "Ya existe un proyecto con ese nombre";
					return View();
				}
				else
				{
					// Se obtiene la sesion de la persona que ingreso al sistema
					// Se carga en la tabla PersonaProyecto la relacion entre que persona
					// acaba de registrar el nuevo proyecto, dejandolo como responsable legal
					// y como director
					var sesion = HttpContext.Session.GetString("UserID");
					PersonaProyecto pp = new PersonaProyecto();
					pp.NombreProyecto = proyecto.NombreProyecto;
					pp.Rut = sesion;
					pp.DirectorS_N = "s";
					pp.ResponsableLegalS_N = "s";
					_baseDatos.PersonaProyecto.Add(pp);
					_baseDatos.Proyecto.Add(proyecto);
					_baseDatos.SaveChanges();
					ModelState.Clear();
					ViewBag.Message = "El proyecto '" + proyecto.NombreProyecto + "' se ha ingresado correctamente";
				}
			}
			return View();
		}
		public IActionResult ModificarProyecto(string nombre)
		{
			
			var proyecto = _baseDatos.Proyecto.Where(u => u.NombreProyecto == nombre).FirstOrDefault();
			ViewBag.Proyecto = proyecto;

			var colaboradores = _baseDatos.PersonaProyecto.Where(u => u.NombreProyecto == nombre).ToList();
			var personasAjenasAlProyecto = _baseDatos.PersonaProyecto.Where(u => u.NombreProyecto != nombre).ToList();
			var c = (from p in _baseDatos.Persona join col in colaboradores on p.Rut equals col.Rut select new { p.Rut , p.Nombre, p.ApellidoPaterno, p.ApellidoMaterno, col.ResponsableLegalS_N, col.DirectorS_N });
			var q = (from p in _baseDatos.Persona join pp in _baseDatos.PersonaProyecto on p.Rut equals pp.Rut into ps from pp in ps.DefaultIfEmpty() where pp.NombreProyecto != nombre select new { p.Nombre, Rut = pp == null ? "null" : p.Rut , p.ApellidoPaterno, p.ApellidoMaterno });
			var personasAjenas = new List<Persona>();
			var personasAfiliadas = new List<PersonaProyecto>();

			string query = "SELECT * FROM persona p LEFT JOIN personaproyecto pp ON p.rut = pp.rut WHERE pp.nombreProyecto IS NULL OR pp.nombreProyecto != 'proyecto1'";
			
			foreach (var t in q)
			{
				personasAjenas.Add(new Persona()
				{
					Rut = t.Rut,
					Nombre = t.Nombre,
					ApellidoPaterno = t.ApellidoPaterno,
					ApellidoMaterno = t.ApellidoMaterno
				});
			}
			foreach (var t in c)
			{
				personasAfiliadas.Add(new PersonaProyecto()
				{
					Rut = t.Rut,
					DirectorS_N = t.DirectorS_N,
					ResponsableLegalS_N = t.ResponsableLegalS_N
				});
			}
			ViewBag.Colaboradores = personasAfiliadas;
			ViewBag.PersonasAjenasAlProyecto = personasAjenas;

			//List<Persona> personasAjenas;
			//foreach(var persona in personasAjenasAlProyecto)
			//{
			//	Persona pers = _baseDatos.Persona.Where(u => u.Rut == persona.Rut).FirstOrDefault();
			//	personasAjenas.Insert(0,pers);
			//}

			//ViewBag.PersonasAjenasAlProyecto = personasAjenas;
			if (proyecto == null)
			{
				ViewBag.Message = "Hubo un error al intentar acceder al proyecto";
				return RedirectToAction("Index", "Home");
			}
			return View();
		}
		[HttpPost]
		public IActionResult ModificarProyecto(Proyecto ProyectoModificado)
		{
			var proyecto = _baseDatos.Proyecto.Where(u => u.NombreProyecto == ProyectoModificado.NombreProyecto).FirstOrDefault();
			var personasAjenasAlProyecto = _baseDatos.PersonaProyecto.Where(u => u.NombreProyecto != proyecto.NombreProyecto).ToList();
			ViewBag.Proyecto = proyecto;

			if (ModelState.IsValid)
			{
				if (proyecto != null)
				{
					//var sesion = HttpContext.Session.GetString("UserID");
			
					proyecto.FechaInicio = ProyectoModificado.FechaInicio;
					proyecto.FechaTermino = ProyectoModificado.FechaTermino;
					
					_baseDatos.SaveChanges();
					ViewBag.Message = "Proyecto modificado con éxito.";
					ModelState.Clear();
				}
				else
				{
					ViewBag.Message = "El proyecto solicitado no existe.";
					return View();
		
					
				}
			}
			return View(ProyectoModificado);
		}

		public IActionResult EliminarProyecto(string nombre)
		{

			return View();
		}

		public IActionResult Error()
		{
			return View();
		}


		public IActionResult Feed()
		{
			return View(_baseDatos.Proyecto.ToList());
		}


		public IActionResult AgregarPersonaAlProyecto(object nombre)
		{
			throw new NotImplementedException();
		}
	}
}
