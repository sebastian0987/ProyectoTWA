using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyectoTWA.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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

        public IActionResult Archivo(string nombre)
        {
            var model = _baseDatos.Archivo.Where(u => u.NombreArchivo == nombre).FirstOrDefault();
            if (model == null)
            {
                ViewBag.Message = "Hubo un error al intentar acceder al archivo";
                return RedirectToAction("Index");
            }
           return RedirectToAction("UpdateFile", "Archivo", new { nombre = nombre });
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
            if (!ValidarRut(persona.Rut))
            {
                ViewBag.Message = "El RUT ingresado no es valido";
                return View();
            }
            if (ModelState.IsValid)
            {
                var cuenta = _baseDatos.Persona.Where(u => u.Rut == persona.Rut).FirstOrDefault();
                if (cuenta != null)
                {
                    ViewBag.Message = "El RUT ingresado ya existe en el sistema";
                    return View();
                }
                persona.Password = GetHash(persona.Password);
                _baseDatos.Persona.Add(persona);
                _baseDatos.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = persona.Nombre + " " + persona.ApellidoPaterno + " " + persona.ApellidoMaterno + " se ha ingresado correctamente";

            }
            return View();
        }

        private static string GetHash(string text)
        {
            text = "08rkeo87s0" + text;
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Persona persona)
        {
            if (ValidarRut(persona.Rut))
            {
                var cuenta = _baseDatos.Persona.Where(u => u.Rut == persona.Rut).FirstOrDefault();
                if (cuenta != null)
                {
                    if (cuenta.Password == GetHash(persona.Password))
                    {
                        HttpContext.Session.SetString("UserID", cuenta.Rut.ToString());
                        HttpContext.Session.SetString("Administrador", cuenta.Administrador.ToString());
                        return RedirectToAction("Feed", "Proyecto");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Rut y/o contraseña incorrecto");
                        return View();
                    }
                        
                }
                else
                {
                    ModelState.AddModelError("", "Rut incorrecto");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "RUT ingresado no valido");
                return View();
            }
        }

        private bool ValidarRut(string rut)
        {

            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }

        public ActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


    }
}
