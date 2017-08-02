using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using proyectoTWA.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace proyectoTWA.Controllers
{
    public class ArchivoController : Controller
    {
        private IHostingEnvironment hostingEnv;
        private BaseDatos _baseDatos;
        private string fecha()
        {
            DateTime date = new DateTime();
            
            return date.ToString();
        }

        public ArchivoController(IHostingEnvironment env, BaseDatos baseDatos)
        {
            this.hostingEnv = env;
            _baseDatos = baseDatos;
        }

        public IActionResult AddFiles()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddFiles(IList<IFormFile> files, Archivo archivo)
        {
           
            long size = 0;
            foreach (var file in files)
            {
                
                //Archivo archivo = new Archivo();
                //DateTime fecha = new DateTime();
                //var h = fecha.Year.ToString() + fecha.Second.ToString();
                //var nombreProyecto = DateTime.Now.ToString("MMddyyyyHmmssfff");
                //Se obtiene el nombre del archivo mas su extension
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                //Se obtiene la extension del archivo
                string [] extension = filename.Split('.');
        
                //Se guarda en la clase Archivo
                archivo.NombreArchivo = archivo.NombreArchivo+"." + extension.Last();

                //Se agrega el nombre del archivo a la ruta 'C:\Users\tatan\Source\Repos\ProyectoTWA\proyectoTWA\proyectoTWA\wwwroot'
                filename = hostingEnv.WebRootPath + $@"\{archivo.NombreArchivo}";
                archivo.Ubicacion = hostingEnv.WebRootPath;
                //archivo.NombreProyecto = nombreProyecto;
                size += file.Length;

                //Verificar que no existan dos archivos con el mismo nombre dentro de un proyecto
                var cuenta = _baseDatos.Archivo.Where(u => u.NombreArchivo == archivo.NombreArchivo && u.NombreProyecto == archivo.NombreProyecto).FirstOrDefault();
                if (cuenta != null)
                {
                    ViewBag.Message = "Ya existe un archivo dentro del proyecto con el mismo nombre";
                    return View();
                }

                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                archivo.Rut = HttpContext.Session.GetString("UserID");
                archivo.NombreProyecto = HttpContext.Session.GetString("ProyectoID");
                RegistrarArchivo(archivo.NombreArchivo,archivo.NombreProyecto);
                _baseDatos.Archivo.Add(archivo);
                _baseDatos.SaveChanges();

            }
            
            ViewBag.Message = "El archivo "+archivo.NombreArchivo+" se ha subido exitosamente";
            return View();
        }
        private void RegistrarArchivo(string nombreArchivo,string nombreProyecto)
        {
            Registro registro = new Registro();
            var rut = HttpContext.Session.GetString("UserID");
            var cuenta = _baseDatos.Persona.Where(u => u.Rut == rut).First();

            registro.NombreArchivo = nombreArchivo;
            //registro.NombreProyecto = nombreProyecto;
            registro.TipoModificacion = cuenta.Nombre + " agrego el archivo " + nombreArchivo + " al proyecto " + nombreProyecto;
            _baseDatos.Registro.Add(registro);
            _baseDatos.SaveChanges();
        }

        public IActionResult UpdateFile(string nombre)
        {
            var cuenta = _baseDatos.Archivo.Where(u => u.NombreArchivo == nombre).FirstOrDefault();
            if (cuenta == null)
            {
                ViewBag.Message = "Hubo un error al intentar acceder al archivo";
                return RedirectToAction("Index", "Home");
            }
            return View(cuenta);
        }
        [HttpPost]
        public IActionResult UpdateFile(Archivo archivo)
        {
            var cuenta = _baseDatos.Archivo.Where(u => u.NombreArchivo == archivo.NombreArchivo).First();
            if (cuenta == null)
            {
                ViewBag.Message = "Error";
                return View(archivo);
            }
            else
            {
                if(archivo.Estado != null)
                {
                    //Para modificar
                    cuenta.Estado = archivo.Estado;
                    ModificarRegistro(archivo.NombreArchivo,cuenta.Estado);
                    //_baseDatos.Update(cuenta);
                    _baseDatos.SaveChanges();
                    return RedirectToAction("ListaArchivo");
                }
                return RedirectToAction("ListaArchivo");

                //Para borrar
                //_baseDatos.Remove(cuenta);
                //_baseDatos.SaveChanges();
            }

            
        }


        private void ModificarRegistro(string nombreArchivo,string nuevoEstado)
        {
            Registro registro = new Registro();
            var rut = HttpContext.Session.GetString("UserID");
            var cuenta = _baseDatos.Persona.Where(u => u.Rut == rut).First();
            registro.NombreArchivo = nombreArchivo;
            registro.TipoModificacion = cuenta.Nombre + " cambio el estado del archivo " + nombreArchivo + " a " + nuevoEstado + " (Proyecto " + HttpContext.Session.GetString("ProyectoID") + ")"; 
            _baseDatos.Registro.Add(registro);
            _baseDatos.SaveChanges();
        }

        public IActionResult ListaArchivo()
        {
            var cuenta = _baseDatos.Archivo.Where(u => u.NombreProyecto == HttpContext.Session.GetString("ProyectoID")).ToList();
            //if (cuenta == null)
            //{
            //    ViewBag.Message = "Error";
            //    return View(archivo);
            //}
            //else
            //{
            //    if (archivo.Estado != null)
            //    {
            //        //Para modificar
            //        cuenta.Estado = archivo.Estado;
            //        //_baseDatos.Update(cuenta);
            //        _baseDatos.SaveChanges();
            //        return RedirectToAction("Index", "Home");
            //    }
            //    return RedirectToAction("Index", "Home");
            //}

            return View(cuenta);
        }
    }
}
