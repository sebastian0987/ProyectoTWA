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

        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}
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
                var nombreProyecto = DateTime.Now.ToString("MMddyyyyHmmssfff");
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
                archivo.NombreProyecto = nombreProyecto;
                size += file.Length;

                //Verificar que no existan dos archivos con el mismo nombre dentro de un proyecto
                var cuenta = _baseDatos.archivo.Where(u => u.NombreArchivo == archivo.NombreArchivo && u.NombreProyecto == archivo.NombreProyecto).FirstOrDefault();
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
                RegistrarArchivo(archivo.NombreArchivo,archivo.NombreProyecto);
                _baseDatos.archivo.Add(archivo);
                _baseDatos.SaveChanges();

            }
            
            ViewBag.Message = "El archivo "+archivo.NombreArchivo+" se ha subido exitosamente";
            return View();
        }
        private void RegistrarArchivo(string nombreArchivo,string nombreProyecto)
        {
            Registro registro = new Registro();
            registro.Rut = HttpContext.Session.GetString("UserID");
            registro.NombreArchivo = nombreArchivo;
            registro.NombreProyecto = nombreProyecto;
            registro.TipoModificacion = "agregar";
            _baseDatos.registro.Add(registro);
            _baseDatos.SaveChanges();
        }

        public IActionResult UpdateFile()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdateFile(Archivo archivo)
        {
            return View();
        }
    }
}
