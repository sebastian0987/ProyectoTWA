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
    public class subirArchivoController : Controller
    {
        private IHostingEnvironment hostingEnv;
        private BaseDatos _baseDatos;
        private string fecha()
        {
            DateTime date = new DateTime();
            
            return date.ToString();
        }

        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public subirArchivoController(IHostingEnvironment env, BaseDatos baseDatos)
        {
            this.hostingEnv = env;
            _baseDatos = baseDatos;
        }

        public IActionResult UploadFiles()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadFiles(IList<IFormFile> files)
        {
           
            long size = 0;
            foreach (var file in files)
            {
                Archivo archivo = new Archivo();
                //DateTime fecha = new DateTime();
                //var h = fecha.Year.ToString() + fecha.Second.ToString();
                var nombreArchivo = DateTime.Now.ToString("MMddyyyyHmmssfff");
                //Se obtiene el nombre del archivo mas su extension
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                //Se obtiene la extension del archivo
                string [] extension = filename.Split('.');
                //Se agregar la extension al nuevo nombre del archivo
                nombreArchivo = nombreArchivo + "." + extension.Last();
                //Se guarda en la clase Archivo
                archivo.nombre = nombreArchivo;
                //Se agrega el nombre del archivo a la ruta 'C:\Users\tatan\Source\Repos\ProyectoTWA\proyectoTWA\proyectoTWA\wwwroot'
                filename = hostingEnv.WebRootPath + $@"\{nombreArchivo}";
                archivo.ubicacion = hostingEnv.WebRootPath;
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                _baseDatos.archivo.Add(archivo);
                _baseDatos.SaveChanges();

            }
            
            ViewBag.Message = "Error";
            return View();
        }
    }
}
