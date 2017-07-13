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
                var cuenta = _baseDatos.persona.Where(u => u.rut == persona.rut).FirstOrDefault();
                if (cuenta != null)
                {
                    ViewBag.Message = "El RUT ingresado ya existe en el sistema";
                    return View();
                }
                _baseDatos.persona.Add(persona);
                _baseDatos.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = persona.nombre + " " + persona.apellidoPaterno + " " + persona.apellidoMaterno + " se ha ingresado correctamente";

            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Persona persona)
        {
            var cuenta = _baseDatos.persona.Where(u => u.rut == persona.rut && u.password == persona.password).FirstOrDefault();
            if (cuenta != null)
            {
                HttpContext.Session.SetString("UserID", cuenta.rut.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "rut o contraseña incorrecta");
            }
            return View();
        }

        public ActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
       
    }
}
