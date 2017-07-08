using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using proyectoTWA.Models;

namespace proyectoTWA.Controllers
{
    public class HomeController : Controller
    {
        private BaseDatos _baseDatos;
        public HomeController(BaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registrar(Persona persona)
        {
            if (ModelState.IsValid)
            {
                _baseDatos.persona.Add(persona);
                _baseDatos.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = persona.nombre + " " + persona.apellidoPaterno + " " + persona.apellidoMaterno + " se ha ingresado correctamente";

            }
            return View();
        }
    }
}
