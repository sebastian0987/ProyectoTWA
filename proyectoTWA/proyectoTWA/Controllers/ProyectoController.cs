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

        public ActionResult NuevoProyecto(Proyecto proyecto)
        {
			if (ModelState.IsValid)
			{
				var cuenta = _baseDatos.Proyecto.Where(u => u.NombreProyecto == proyecto.NombreProyecto).FirstOrDefault();
				if (cuenta != null)
				{
					ViewBag.Message = "Ya existe un proyecto con ese nombre";
					return View();
				}
				else { 
					_baseDatos.Proyecto.Add(proyecto);
					_baseDatos.SaveChanges();
					ModelState.Clear();
					ViewBag.Message = "El proyecto '" + proyecto.NombreProyecto + "' se ha ingresado correctamente";
				}
			}
			return View();
		}

        //public IActionResult Proyecto()
        //{
        //    return RedirectToAction("Feed", "Proyecto", new { nombre = nombre });
        //    //return RedirectToAction("UpdateFile", "Proyecto", new { nombre = nombre });
        //}


        public IActionResult Error()
        {
            return View();
        }


        public IActionResult Feed()
        {
            return View(_baseDatos.Proyecto.ToList());
        }
       
    }
}
