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
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Query.Expressions;

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
				var nomproyecto = _baseDatos.Proyecto.Where(u => u.NombreProyecto == proyecto.NombreProyecto).FirstOrDefault();
				if (nomproyecto != null)
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
            HttpContext.Session.SetString("ProyectoID", nombre);
            var colaboradores = _baseDatos.PersonaProyecto.Where(u => u.NombreProyecto == nombre).ToList();
			var personasAjenasAlProyecto = _baseDatos.PersonaProyecto.Where(u => u.NombreProyecto != nombre).ToList();
			var c = (from p in _baseDatos.Persona join col in colaboradores on p.Rut equals col.Rut select new { p.Rut , p.Nombre, p.ApellidoPaterno, p.ApellidoMaterno, col.ResponsableLegalS_N, col.DirectorS_N });
			//var q = (from p in _baseDatos.Persona join pp in _baseDatos.PersonaProyecto on p.Rut equals pp.Rut into ps from pp in ps.DefaultIfEmpty() where pp.NombreProyecto != nombre && pp.Rut == p.Rut select new { p.Nombre, Rut = pp == null ? "null" : p.Rut, p.ApellidoPaterno, p.ApellidoMaterno });
			var q = _baseDatos.Persona.Where(p => !_baseDatos.PersonaProyecto.Any(pp => pp.NombreProyecto == nombre && pp.Rut == p.Rut));
			var personasAjenas = new List<Persona>();
			var personasAfiliadas = new List<PersonaProyecto>();

			//string query = "SELECT * FROM persona p WHERE not exists(select* from personaproyecto pp  where pp.nombreProyecto = 'proyecto2' and p.rut = pp.rut)";
			
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
		public IActionResult ModificarProyecto(Proyecto proyectoModificado)
		{
			
			var proyecto = _baseDatos.Proyecto.Where(u => u.NombreProyecto == proyectoModificado.NombreProyecto).FirstOrDefault();
			ViewBag.Proyecto = proyecto;

			if (ModelState.IsValid)
			{
				if (proyecto != null)
				{
					//var sesion = HttpContext.Session.GetString("UserID");
			
					proyecto.FechaInicio = proyectoModificado.FechaInicio;
					proyecto.FechaTermino = proyectoModificado.FechaTermino;
					
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
			//return View(proyectoModificado);
			return RedirectToAction("ModificarProyecto", "Proyecto", new { nombre = proyecto.NombreProyecto });
		}

		public IActionResult EliminarProyecto(string nombre)
		{
			var proyecto = _baseDatos.Proyecto.Where(p => p.NombreProyecto == nombre).FirstOrDefault();
			var personaproyecto = _baseDatos.PersonaProyecto.Where(pp => pp.NombreProyecto == nombre);
			_baseDatos.Proyecto.Remove(proyecto);
			foreach(var pp in personaproyecto)
			{
				_baseDatos.PersonaProyecto.Remove(pp);
			}
			//_baseDatos.PersonaProyecto.RemoveRange(_baseDatos.PersonaProyecto.Where(pp => pp.NombreProyecto == nombre).ToList());
			_baseDatos.SaveChanges();
			return RedirectToAction("Feed", "Proyecto");
		}

		public IActionResult Error()
		{
			return View();
		}


		public IActionResult Feed()
		{
            //Obtener registro de los archivos en los proyectos del usuario
            ObtenerRegistros();
            var rut = HttpContext.Session.GetString("UserID");
            var personaProyectos = _baseDatos.PersonaProyecto.Where(u => u.Rut == rut).ToList();
            var proyectos = (from p in _baseDatos.Proyecto
                             join pp in personaProyectos
                             on p.NombreProyecto equals pp.NombreProyecto
                             select new
                             {
                                 p.NombreProyecto,
                                 p.FechaInicio,
                                 p.FechaTermino
                             });
            List<Proyecto> listaProyectos = new List<Proyecto>();
            foreach (var p in proyectos)
            {
                Proyecto proyecto = new Proyecto();
                proyecto.NombreProyecto = p.NombreProyecto;
                proyecto.FechaInicio = p.FechaInicio;
                proyecto.FechaTermino = p.FechaTermino;
                listaProyectos.Add(proyecto);

            }
            return View(listaProyectos);
		}

		
		public IActionResult AgregarPersonaAlProyecto(string rut, string nombreProyecto, string director, string responsable)
		{
			PersonaProyecto pp = new PersonaProyecto();
			pp.NombreProyecto = nombreProyecto;
			pp.Rut = rut;
			pp.DirectorS_N = director;
			pp.ResponsableLegalS_N = responsable;
			_baseDatos.PersonaProyecto.Add(pp);
			_baseDatos.SaveChanges();
			ModelState.Clear();
			return RedirectToAction("ModificarProyecto", "Proyecto", new {nombre = nombreProyecto});
		}

        public IActionResult Archivo(string nombreProyecto)
        {
            return RedirectToAction("ListaArchivo", "Archivo");
        }

        private void ObtenerRegistros()
        {
            //var c = (from p in _baseDatos.Persona join col in colaboradores on p.Rut equals col.Rut select new { p.Rut , p
            //_baseDatos.Proyecto.Where(u => u.NombreProyecto == proyectoModificado.NombreProyecto).FirstOrDefault();
            var rut = HttpContext.Session.GetString("UserID");
            var proyectos = _baseDatos.PersonaProyecto.Where(u => u.Rut == rut).ToList();
            var archivos = (from a in _baseDatos.Archivo join p in proyectos on a.NombreProyecto equals p.NombreProyecto join r in _baseDatos.Registro on a.NombreArchivo equals r.NombreArchivo select new {r.TipoModificacion }).Take(10);
            List<string> listaArchivos = new List<string>();
            foreach (var archivo in archivos)
            {
                listaArchivos.Add(archivo.TipoModificacion);
                
            }
            ViewBag.Feed = listaArchivos;
        }
    }
}
